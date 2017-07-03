// Decompiled with JetBrains decompiler
// Type: UIDlcButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDlcButton : MonoBehaviour
{
  public uint DlcAppId;
  public Button button;
  public GameObject comingSoon;
  public GameObject owned;
  public GameObject avalible;
  public GameObject noSteam;

  public void Setup()
  {
    this.button.onClick.RemoveAllListeners();
    bool flag1 = SteamManager.IsSteamOnline();
    bool flag2 = App.instance.dlcManager.IsDlcKnown(this.DlcAppId);
    bool inIsActive1 = flag2 && App.instance.dlcManager.IsDlcInstalled(this.DlcAppId);
    bool inIsActive2 = flag1 && flag2 && !inIsActive1;
    GameUtility.SetActive(this.noSteam, !flag1 && !inIsActive1);
    GameUtility.SetActive(this.comingSoon, !flag2);
    GameUtility.SetActive(this.owned, inIsActive1);
    GameUtility.SetActive(this.avalible, inIsActive2);
    this.button.interactable = inIsActive2;
    if (!inIsActive2)
      return;
    this.button.onClick.AddListener(new UnityAction(this.HandleDlcButtonPress));
  }

  private void HandleDlcButtonPress()
  {
    DLCManager.HandleGetDlcButton(this.DlcAppId);
  }
}
