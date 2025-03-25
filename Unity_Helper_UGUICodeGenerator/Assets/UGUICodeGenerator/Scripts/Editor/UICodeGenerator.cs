using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UGUICodeGenerator
{
    public class UICodeGenerator
    {
        /// <summary> 引用空间 </summary>
        private const string Str_Using = "using System.Collections;\nusing UnityEngine;\nusing UnityEngine.UI;\n\n";

        /// <summary> 类声明 </summary>
        private const string Str_Class = "public partial class #ScriptName# : MonoBehaviour";

        /// <summary> 要替换的类名 </summary>
        public const string ScriptName = "#ScriptName#";

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <returns>代码内容</returns>
        public static string StartScriptGenerate(Transform transform, string className)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"// 自动生成代码，勿手动修改！\n");
            sb.Append($"// 生成时间：{UICodeHelper.GetCurrentTime()}\n\n");
            sb.Append(Str_Using);
            sb.Append(Str_Class.Replace(ScriptName, className)); // 替换类名
            sb.Append("\n{\n");

            List<VariableNameData> listVariableName = new List<VariableNameData>(); // 变量名
            List<PathData> listVariablePath = new List<PathData>(); // 变量名路径
            List<VariableNameData> listProperty = new List<VariableNameData>(); // 属性
            List<VariableNameData> listEvent = new List<VariableNameData>(); // 事件

            // 拿到所有的附着组件
            foreach (var attacher in transform.GetComponentsInChildren<UICodeAttacher>())
            {
                var data = new VariableNameData();

                var type = UICodeHelper.GetTypeName(attacher.attachingUI);
                data.type = type;

                var name = attacher.name;
                if (!string.IsNullOrEmpty(attacher.overrideName))
                {
                    name = attacher.overrideName;
                }

                // 移除非法字符
                name = name.Replace(" ", "");
                name = name.Replace("(", "");
                name = name.Replace(")", "");
                name = name.Replace("/", "");
                name = name.Replace("\\", "");
                name = name.Replace("\"", "");
                name = name.Replace("\'", "");
                name = name.Replace("$", "");

                name = data.type.FirstCharacterToLower() + "_" + name;
                data.goName = name;

                // 添加变量
                listVariableName.Add(data);

                // 添加查找路径
                Transform parent = attacher.transform;
                List<string> parentPath = new() { parent.name };
                for(int i = 0; i < 100; i++)
                {
                    if (parent == transform)
                        break;
                    
                    parent = parent.parent;
                    parentPath.Add(parent.name);
                }

                parentPath.RemoveAt(parentPath.Count - 1);
                parentPath.Reverse();

                var pathData = new PathData();
                pathData.variablename = name;
                pathData.path = string.Join("/", parentPath);
                pathData.typename = type;
                listVariablePath.Add(pathData);
            }


            #region 添加变量
            foreach (var item in listVariableName)
            {
                sb.Append($"\t[SerializeField] {item.type} {item.goName};\r\n");
            }
            #endregion


            sb.Append("\r\n");


            #region 添加事件
            if (listEvent.Count > 0)
            {
                sb.Append("\r\n");
                sb.Append("\tprivate void Start()\n\t{\n"); //start 方法
                List<string> listMethodName = new List<string>();
                foreach (var item in listEvent)
                {
                    sb.Append($"\t\t{item.goName}.");
                    if (item.type == nameof(Button))
                    {
                        string methodName = $"{item.goName}_OnClick";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onClick.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "()");
                    }
                    else if (item.type == "ButtonPlus")
                    {
                        string methodName = $"{item.goName}_OnClick";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onClick.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "()");
                    }
                    else if (item.type == nameof(Toggle))
                    {
                        string methodName = $"{item.goName}_OnValueChanged";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onValueChanged.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "(bool isOn)");
                    }
                    else if (item.type == nameof(InputField))
                    {
                        string methodName = $"{item.goName}_OnValueChanged";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onValueChanged.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "(string arg)");
                    }
                    else if (item.type == nameof(Slider))
                    {
                        string methodName = $"{item.goName}_OnValueChanged";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onValueChanged.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "(float value)");
                    }
                    else if (item.type == nameof(Dropdown))
                    {
                        string methodName = $"{item.goName}_OnValueChanged";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onValueChanged.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "(int index)");
                    }
                    else if (item.type == nameof(Scrollbar))
                    {
                        string methodName = $"{item.goName}_OnValueChanged";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onValueChanged.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "(float value)");
                    }
                    else if (item.type == nameof(ScrollRect))
                    {
                        string methodName = $"{item.goName}_OnValueChanged";
                        methodName = methodName.Remove(0, 2); //移除m_
                        sb.Append($"onValueChanged.AddListener({methodName});\n");
                        listMethodName.Add(methodName + "(Vector2 detal)");
                    }
                }
                sb.Append("\t}\n");


                //添加方法
                foreach (var method in listMethodName)
                {
                    sb.Append($"\tprivate void {method}\n");
                    sb.Append("\t{\n\n\t}\n");
                }
            }
            #endregion



            #region 添加Reset方法 查询路径
            sb.Append("\r\n\r\n\r\n");
            sb.Append("\t#region 用于寻找控件,当控件丢失,点击脚本齿轮->Reset菜单可恢复,也可重新编写下面的路径代码\r\n");
            sb.Append("#if UNITY_EDITOR\n\tprivate void Reset()\n");
            sb.Append("\t{\n");
            //添加寻找代码
            for (int i = 0; i < listVariablePath.Count; i++)
            {
                string needFind = string.IsNullOrEmpty(listVariablePath[i].path) ? string.Empty : $"transform.Find(\"{listVariablePath[i].path}\").";
                sb.Append($"\t\t{listVariablePath[i].variablename} = {needFind}GetComponent<{listVariablePath[i].typename}>();\r\n");
            }

            sb.Append("\t}\n");
            sb.Append("#endif\n");
            sb.Append("\t#endregion\n");
            #endregion

            sb.Append("}");

            return sb.ToString();
        }

        /// <summary>
        /// 保存代码
        /// </summary>
        /// <param name="codeStr"></param>
        /// <param name="destfile"></param>
        /// <returns></returns>
        public static bool SaveScript(string codeStr, string destfile, string fileName)
        {
            if(!Directory.Exists(destfile))
            {
                Directory.CreateDirectory(destfile);
            }
            try
            {
                //替换脚本名
                //codeStr = codeStr.Replace(ScriptName, Path.GetFileNameWithoutExtension(destfile));
                File.WriteAllText(destfile + "/" + fileName, codeStr);
                AssetDatabase.Refresh();
                Debug.Log($"脚本生成成功，路径：{destfile + "/" + fileName}");
                return true;
            }
            catch (Exception e)
            {
                Debug.Log("脚本保存失败 " + e.ToString());
                return false;
            }
        }
    }

    #region Struct Data

    public struct VariableNameData
    {
        public string type;
        public string goName;
        public VariableNameData(string _type, string _goName)
        {
            this.type = _type;
            this.goName = _goName;
        }
    }

    public struct PathData
    {
        public string variablename;
        public string path;
        public string typename;
        public PathData(string _variablename, string _path, string _typename)
        {
            this.variablename = _variablename;
            this.path = _path;
            this.typename = _typename;
        }
    }

    #endregion Struct Data
}