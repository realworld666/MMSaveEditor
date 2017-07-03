// Decompiled with JetBrains decompiler
// Type: UICompareStaffStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICompareStaffStatEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public Button button;
  public TextMeshProUGUI label;
  public TextMeshProUGUI leftValue;
  public TextMeshProUGUI rightValue;
  public Image leftBar;
  public Image rightBar;
  public UICompareStaffStatsWidget widget;
  private GameObject mCircle;
  private Button mCircleButton;
  private UIGraphRadarCircle mCircleRadar;
  private MouseEvents mCircleMouseEvent;
  private int mIndex;

  public void Setup(int inIndex, string inLabel, float inLeftValue, float inRightValue, int inMaxValue)
  {
    this.mIndex = inIndex;
    this.label.text = inLabel;
    this.mCircle = this.widget.graph.GetHighlightObject(this.mIndex);
    this.mCircleButton = this.mCircle.GetComponent<Button>();
    this.mCircleRadar = this.mCircle.GetComponent<UIGraphRadarCircle>();
    this.mCircleButton.onClick.RemoveAllListeners();
    this.mCircleButton.onClick.AddListener(new UnityAction(this.SelectEntry));
    this.AddCircleMouseEventComponent();
    if ((double) inLeftValue >= 0.0)
    {
      GameUtility.SetActive(this.leftValue.gameObject, true);
      GameUtility.SetActive(this.leftBar.gameObject, true);
      this.leftValue.text = ((int) inLeftValue).ToString();
      GameUtility.SetImageFillAmountIfDifferent(this.leftBar, inLeftValue / (float) inMaxValue, 1f / 512f);
      this.leftBar.color = this.widget.graphColor1;
    }
    else
    {
      GameUtility.SetActive(this.leftValue.gameObject, false);
      GameUtility.SetActive(this.leftBar.gameObject, false);
    }
    if ((double) inRightValue >= 0.0)
    {
      GameUtility.SetActive(this.rightValue.gameObject, true);
      GameUtility.SetActive(this.rightBar.gameObject, true);
      this.rightValue.text = ((int) inRightValue).ToString();
      GameUtility.SetImageFillAmountIfDifferent(this.rightBar, inRightValue / (float) inMaxValue, 1f / 512f);
      this.rightBar.color = this.widget.graphColor2;
    }
    else
    {
      GameUtility.SetActive(this.rightValue.gameObject, false);
      GameUtility.SetActive(this.rightBar.gameObject, false);
    }
  }

  private void AddCircleMouseEventComponent()
  {
    this.mCircleMouseEvent = this.mCircleButton.gameObject.GetComponent<MouseEvents>();
    if ((Object) this.mCircleMouseEvent == (Object) null)
      this.mCircleMouseEvent = this.mCircle.gameObject.AddComponent<MouseEvents>();
    this.mCircleMouseEvent.targetObject = this.gameObject;
  }

  public void SelectEntry()
  {
    this.button.Select();
  }

  public void OnMouseEnter()
  {
    this.SelectEntry();
  }

  public void OnMouseExit()
  {
    EventSystem.current.SetSelectedGameObject((GameObject) null);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (!((Object) this.mCircle != (Object) null))
      return;
    this.mCircleButton.Select();
    this.mCircleRadar.OnPointerEnter((PointerEventData) null);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (!((Object) this.mCircle != (Object) null))
      return;
    EventSystem.current.SetSelectedGameObject((GameObject) null);
    this.mCircleRadar.OnPointerExit((PointerEventData) null);
  }
}
