// Decompiled with JetBrains decompiler
// Type: UIDriverInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDriverInfoWidget : MonoBehaviour
{
  public Flag flag;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI name;
  public TextMeshProUGUI status;
  public TextMeshProUGUI mindSet;
  public Image mindSetIcon;
  public UIGauge obedience;
  public UIGauge morale;
  public TextMeshProUGUI expectedPositionsQualifying;
  public TextMeshProUGUI expectedPositionsRace;

  public void Setup(Driver inDriver)
  {
    this.flag.SetNationality(inDriver.nationality);
    this.portrait.SetPortrait((Person) inDriver);
    this.name.text = inDriver.name;
    this.status.text = Localisation.LocaliseEnum((Enum) inDriver.contract.proposedStatus);
    this.mindSet.text = inDriver.mentalState.emotion.ToString();
    this.mindSetIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-" + inDriver.mentalState.smileyType.ToString() + "Smiley");
    this.obedience.SetValueRange(0.0f, 1f);
    this.obedience.SetValue(inDriver.obedience, UIGauge.AnimationSetting.Animate);
    this.morale.SetValueRange(0.0f, 1f);
    this.morale.SetValue(inDriver.GetMorale(), UIGauge.AnimationSetting.Animate);
  }
}
