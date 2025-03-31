using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using UGUICodeGenerator;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.iOS;


namespace UGUICodeGenerator
{
    public class UICodeEditor
    {
        /// <summary>
        /// 获取生成的类/文件名
        /// </summary>
        /// <returns></returns>
        public static string GetGenClassName(GameObject gameObject)
        {
            return $"UICode_{gameObject.name}";
        }

        [MenuItem("GameObject/UI Code Gen 生成UI代码", priority = 1001)]
        public static void UICodeGen()
        {
            GameObject selectedObj = Selection.activeGameObject;
            var className = GetGenClassName(selectedObj);

            var codeStr = UICodeGenerator.StartScriptGenerate(selectedObj.transform, className);
            Debug.Log(codeStr);

            UICodeGenerator.SaveScript(codeStr, UICodeGeneratorParam.SavePath, $"{className}.cs");
        }

        [MenuItem("GameObject/UI Code Attach 挂载UI代码", priority = 1002)]
        public static void UICodeAttach()
        {
            GameObject selectedObj = Selection.activeGameObject;
            var className = GetGenClassName(selectedObj);

            var fileFullPath = UICodeGeneratorParam.SavePath + $"/{className}.cs";
            System.Type scriptType = UICodeHelper.GetAssembly().GetType(Path.GetFileNameWithoutExtension(fileFullPath));
            if (scriptType == null)
            {
                Debug.LogError("UICodeGen:没有找到代码文件: " + fileFullPath);
                return;
            }

            var target = Selection.activeGameObject.GetComponent(scriptType);
            if (target != null)
            {
                Debug.LogWarning("UICodeGen:已有绑定脚本: " + className);

                GameObject.DestroyImmediate(target);
            }

            Selection.activeGameObject.AddComponent(scriptType);

            // Mark Dirty
            var prefabStage = PrefabStageUtility.GetPrefabStage(Selection.activeGameObject);
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }

            Debug.Log("UICodeGen:脚本绑定成功: " + className);
        }
    }
}
