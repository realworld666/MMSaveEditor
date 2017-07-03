// Decompiled with JetBrains decompiler
// Type: TitleLoadingScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class TitleLoadingScreen : UIScreen
{
  public GameObject baseGameLoading;
  public GameObject gtSeriesDLCLoading;
  [SerializeField]
  private TextMeshProUGUI[] tipText;

  public override void OnEnter()
  {
    base.OnEnter();
    GameUtility.SetActive(this.gtSeriesDLCLoading, App.instance.dlcManager.IsDlcWithIdInstalled(2));
    GameUtility.SetActive(this.baseGameLoading, !this.gtSeriesDLCLoading.activeSelf);
    this.showNavigationBars = false;
    for (int index = 0; index < this.tipText.Length; ++index)
      GameUtility.SetLoadingTip(this.tipText[index]);
    scSoundManager.LoadSoundBank_Frontend();
  }

  public override void OnExit()
  {
    base.OnExit();
    UIManager.instance.ClearNavigationStacks();
  }
}
