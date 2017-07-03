// Decompiled with JetBrains decompiler
// Type: UIMailToggleEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMailToggleEntry : MonoBehaviour
{
  private Message.Group mGroup = Message.Group.Other;
  public TextMeshProUGUI heading;
  public Image backing;
  public Toggle toggle;

  private void Awake()
  {
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
  }

  private void OnValueChanged(bool inValue)
  {
    if (!inValue)
      return;
    UIManager.instance.GetScreen<MailScreen>().SetMediaGroup(this.mGroup);
  }

  public void Setup(Message.Group inGroup)
  {
    this.mGroup = inGroup;
    this.heading.text = this.name;
    this.backing.color = MailScreen.GetBackingColor(this.mGroup);
  }

  public void UpdateText()
  {
    string str = Localisation.LocaliseEnum((Enum) this.mGroup, Localisation.currentLanguage, "Male");
    int countPerGroupType = Game.instance.messageManager.GetUnreadMessagesCountPerGroupType(this.mGroup);
    if (countPerGroupType != 0)
      str = str + " (" + countPerGroupType.ToString() + ")";
    this.heading.text = str;
  }
}
