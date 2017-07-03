// Decompiled with JetBrains decompiler
// Type: UIApplyColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIApplyColor : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler
{
  [SerializeField]
  private ColorBlock colorBlock = new ColorBlock();
  [SerializeField]
  private GameObject[] targetObjects = new GameObject[0];

  private void Start()
  {
    this.Normal();
  }

  private void Update()
  {
    Toggle component1 = this.gameObject.GetComponent<Toggle>();
    Button component2 = this.gameObject.GetComponent<Button>();
    if ((Object) component1 != (Object) null)
    {
      if (!component1.enabled)
        this.Disabled();
      if (component1.isOn)
        this.Pressed();
      else
        this.Normal();
    }
    if (!((Object) component2 != (Object) null) || component2.enabled)
      return;
    this.Disabled();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.Highlight();
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    this.Normal();
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    this.Pressed();
  }

  private void Highlight()
  {
    this.SetColor(this.colorBlock.highlightedColor);
  }

  private void Normal()
  {
    this.SetColor(this.colorBlock.normalColor);
  }

  private void Pressed()
  {
    this.SetColor(this.colorBlock.pressedColor);
  }

  private void Disabled()
  {
    this.SetColor(this.colorBlock.disabledColor);
  }

  private void SetColor(Color inColor)
  {
    for (int index = 0; index < this.targetObjects.Length; ++index)
    {
      TextMeshProUGUI component1 = this.targetObjects[index].GetComponent<TextMeshProUGUI>();
      Image component2 = this.targetObjects[index].GetComponent<Image>();
      if ((Object) component1 != (Object) null)
        component1.color = inColor;
      if ((Object) component2 != (Object) null)
        component2.color = inColor;
    }
  }
}
