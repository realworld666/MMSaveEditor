// Decompiled with JetBrains decompiler
// Type: UITooltipsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITooltipsManager : MonoBehaviour
{
  private bool mTooltipIconsOn = true;
  public GameObject canvas;
  private UITooltip[] tooltips;
  private bool mFlagForLateUpdate;

  public void Awake()
  {
    this.tooltips = this.transform.GetComponentsInChildren<UITooltip>(true);
    for (int index = 0; index < this.tooltips.Length; ++index)
      this.tooltips[index].OnStart();
    this.mTooltipIconsOn = true;
  }

  private void OnEnable()
  {
    if ((Object) this.canvas != (Object) null)
      this.canvas.SetActive(true);
    this.mFlagForLateUpdate = true;
  }

  private void LateUpdate()
  {
    if (!this.mFlagForLateUpdate)
      return;
    bool settingBool = App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Tooltips, false);
    if (this.mTooltipIconsOn == settingBool)
      return;
    for (int index = 0; index < this.tooltips.Length; ++index)
    {
      this.tooltips[index].gameObject.SetActive(false);
      if ((Object) this.tooltips[index].target != (Object) null)
        this.tooltips[index].SetTooltipEnabled(settingBool);
    }
    this.mFlagForLateUpdate = false;
    this.mTooltipIconsOn = settingBool;
  }
}
