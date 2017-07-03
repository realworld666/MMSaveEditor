// Decompiled with JetBrains decompiler
// Type: UITooltipIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITooltipIcon : MonoBehaviour
{
  private bool mCachedEnabled;

  private void Awake()
  {
    this.mCachedEnabled = App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Tooltips, false);
    this.SetImage(this.mCachedEnabled);
  }

  private void OnEnable()
  {
    bool settingBool = App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Tooltips, false);
    if (this.mCachedEnabled == settingBool)
      return;
    this.SetImage(settingBool);
    this.mCachedEnabled = settingBool;
  }

  private void SetImage(bool inIsOn)
  {
    if ((Object) this.GetComponent<Button>() != (Object) null)
      this.GetComponent<Button>().enabled = inIsOn;
    if (!((Object) this.GetComponent<Image>() != (Object) null))
      return;
    this.GetComponent<Image>().enabled = inIsOn;
  }
}
