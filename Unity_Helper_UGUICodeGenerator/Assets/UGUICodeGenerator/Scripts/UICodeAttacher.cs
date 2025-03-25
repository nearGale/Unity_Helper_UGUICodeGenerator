using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UGUICodeGenerator
{
    /// <summary>
    /// 附加在UGUI组件上
    /// 生成代码时，会检查含有此脚本的组件，将其生成对应的代码
    /// </summary>
    public class UICodeAttacher : MonoBehaviour
    {
        /// <summary> 绑定的组件，拖上来就可以了 </summary>
        public Component attachingUI;

        /// <summary> 重命名的变量名 </summary>
        public string overrideName;
    }
}