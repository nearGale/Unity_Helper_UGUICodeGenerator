using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace UGUICodeGenerator
{
    public static class UICodeHelper
    {
        public static string GetTypeName(Component comp)
        {
            if (comp == null)
                return "Transform"; // 默认是Transform
            else if (comp is Canvas)
                return "Canvas";
            else if (comp is Button)
                return "Button";
            else if (comp is InputField)
                return "InputField";
            else if (comp is Toggle)
                return "Toggle";
            else if (comp is Slider)
                return "Slider";
            else if (comp is Slider)
                return "Slider";
            else if (comp is Dropdown)
                return "Dropdown";
            else if (comp is Scrollbar)
                return "Scrollbar";
            else if (comp is ScrollRect)
                return "ScrollRect";
            else if (comp is Image)
                return "Image";
            else if (comp is RawImage)
                return "RawImage";
            else if (comp is Text)
                return "Text";
            else if (comp is RectTransform)
                return "RectTransform";
            else if (comp is CanvasRenderer)
                return "CanvasRenderer";

            return "Transform";
        }

        /// <summary>
        /// 获取程序集 (用于加载脚本)
        /// </summary>
        /// <returns>Assembly-CSharp</returns>
        public static Assembly GetAssembly()
        {
            Assembly[] AssbyCustmList = System.AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < AssbyCustmList.Length; i++)
            {
                string assbyName = AssbyCustmList[i].GetName().Name;
                if (assbyName == "Assembly-CSharp")
                {
                    return AssbyCustmList[i];
                }
            }
            return null;
        }

        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString();
        }

        public static async void AwaitToDo(int millis, System.Action callback)
        {
            await Task.Delay(millis);
            callback?.Invoke();
        }

    }
}