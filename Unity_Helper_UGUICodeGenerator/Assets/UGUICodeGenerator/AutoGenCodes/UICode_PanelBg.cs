// 自动生成代码，勿手动修改！
// 生成时间：2025/3/28 19:51:21

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class UICode_PanelBg : MonoBehaviour
{
	[SerializeField] RectTransform rectTransform_ButtonLegacy;
	[SerializeField] Button button_BtnTest;
	[SerializeField] Image image_ButtonLegacy;
	[SerializeField] Text text_TextLegacy;
	[SerializeField] Transform transform_Image;




	#region 用于寻找控件,当控件丢失,点击脚本齿轮->Reset菜单可恢复,也可重新编写下面的路径代码
#if UNITY_EDITOR
	private void Reset()
	{
		rectTransform_ButtonLegacy = transform.Find("Button (Legacy)").GetComponent<RectTransform>();
		button_BtnTest = transform.Find("Button (Legacy)").GetComponent<Button>();
		image_ButtonLegacy = transform.Find("Button (Legacy)").GetComponent<Image>();
		text_TextLegacy = transform.Find("Button (Legacy)/Text (Legacy)").GetComponent<Text>();
		transform_Image = transform.Find("Image").GetComponent<Transform>();
	}
#endif
	#endregion
}