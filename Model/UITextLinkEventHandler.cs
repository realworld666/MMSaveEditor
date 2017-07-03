// Decompiled with JetBrains decompiler
// Type: UITextLinkEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UITextLinkEventHandler : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
{
  private UITextLinkHandler mHandler;

  public void Setup(UITextLinkHandler inHandler)
  {
    this.mHandler = inHandler;
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if ((Object) this.mHandler != (Object) null)
      this.mHandler.FindLinkData();
    else
      Debug.LogWarning((object) "UITextLinkEventHandler has not been setup, but has been added to this GameObject. This can occur if an object is duplicated after the script has been added to it.", (Object) this.gameObject);
  }

  public void OnPointerDown(PointerEventData eventData)
  {
  }
}
