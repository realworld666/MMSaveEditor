// Decompiled with JetBrains decompiler
// Type: UIChooseSeriesEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChooseSeriesEntry : MonoBehaviour
{
  public Toggle toggle;
  public UIChampionshipLogo logo;
  public TextMeshProUGUI tierLabel;
  public TextMeshProUGUI nameLabel;
  public GameObject locked;
  public GameObject[] csIcons;
  public Image stripBacking;
  public ChooseSeriesScreen screen;
  private Championship mChampionship;
  private bool mChoosable;

  public bool isChoosable
  {
    get
    {
      return this.mChoosable;
    }
  }

  public void Setup(Championship inChampionship)
  {
    scSoundManager.BlockSoundEvents = true;
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.RefreshSeriesLock);
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.RefreshSeriesLock);
    this.mChampionship = inChampionship;
    this.mChoosable = !CreateTeamManager.isCreatingTeam || Game.instance.challengeManager.IsAttemptingChallenge() ? this.mChampionship.isChoosable && !this.mChampionship.isBlockedByChallenge : this.mChampionship.isChoosableCreateTeam;
    this.mChoosable = this.mChoosable && App.instance.dlcManager.IsDlcWithIdInstalled(this.mChampionship.DlcID);
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    this.toggle.interactable = this.mChoosable;
    GameUtility.SetActive(this.locked, !this.mChoosable);
    this.logo.SetChampionship(this.mChampionship);
    switch (this.mChampionship.championshipOrder + 1)
    {
      case 1:
        this.tierLabel.text = Localisation.LocaliseID("PSG_10006870", (GameObject) null);
        break;
      case 2:
        this.tierLabel.text = Localisation.LocaliseID("PSG_10006871", (GameObject) null);
        break;
      case 3:
        this.tierLabel.text = Localisation.LocaliseID("PSG_10006872", (GameObject) null);
        break;
      default:
        this.tierLabel.text = "Tier" + (this.mChampionship.championshipOrder + 1).ToString();
        break;
    }
    this.nameLabel.text = this.mChampionship.GetChampionshipName(false);
    this.stripBacking.color = this.mChampionship.uiColor;
    this.SetIcon();
    scSoundManager.BlockSoundEvents = false;
  }

  private void OnDestroy()
  {
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.RefreshSeriesLock);
  }

  private void RefreshSeriesLock()
  {
    if (this.mChampionship == null || !this.gameObject.activeSelf)
      return;
    this.Setup(this.mChampionship);
  }

  private void SetIcon()
  {
    for (int index = 0; index < this.csIcons.Length; ++index)
      GameUtility.SetActive(this.csIcons[index], this.mChampionship.championshipOrder == index);
  }

  private void OnToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue || this.mChampionship == null)
      return;
    this.screen.SelectChampionship(this.mChampionship);
  }
}
