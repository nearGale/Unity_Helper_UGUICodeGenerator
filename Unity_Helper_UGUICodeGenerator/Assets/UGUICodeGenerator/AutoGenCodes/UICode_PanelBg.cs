// 自动生成代码，勿手动修改！
// 生成时间：2025/3/31 20:58:40

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class UICode_PanelBg : MonoBehaviour
{
	[SerializeField] RectTransform rectTransform_BtnCenter;
	[SerializeField] Button button_BtnCenter;
	[SerializeField] Image image_BtnCenter;
	[SerializeField] Text text_TextBtnCenter;
	[SerializeField] Transform transform_ImageTick;
	[SerializeField] Transform transform_TextTryOverrideNme;




	#region 用于寻找控件,当控件丢失,点击脚本齿轮->Reset菜单可恢复,也可重新编写下面的路径代码
#if UNITY_EDITOR
	private void Reset()
	{
		rectTransform_BtnCenter = transform.Find("BtnCenter").GetComponent<RectTransform>();
		button_BtnCenter = transform.Find("BtnCenter").GetComponent<Button>();
		image_BtnCenter = transform.Find("BtnCenter").GetComponent<Image>();
		text_TextBtnCenter = transform.Find("BtnCenter/TextBtnCenter").GetComponent<Text>();
		transform_ImageTick = transform.Find("ImageTick").GetComponent<Transform>();
		transform_TextTryOverrideNme = transform.Find("TextName").GetComponent<Transform>();
	}
#endif
	#endregion
}