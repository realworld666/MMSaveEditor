// Decompiled with JetBrains decompiler
// Type: UIPlayerTrophyEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerTrophyEntry : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI championshipNameLabel;
  public TextMeshProUGUI teamLabel;
  public TextMeshProUGUI championshipYearLabel;
  public GameObject[] trophyIcons;
  private Trophy mTrophy;

  public void Setup(Trophy inTrophy)
  {
    this.mTrophy = inTrophy;
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
    this.championshipNameLabel.text = this.mTrophy.championship.GetChampionshipName(false);
    this.teamLabel.text = this.mTrophy.team.name;
    this.championshipYearLabel.text = this.mTrophy.yearWon.ToString();
    this.SetIcon();
  }

  private void SetIcon()
  {
    for (int index = 0; index < this.trophyIcons.Length; ++index)
      GameUtility.SetActive(this.trophyIcons[index], this.mTrophy.championship.championshipID == index);
  }

  private void OnButton()
  {
    UIManager.instance.ChangeScreen("SeriesResultsTeamScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    UIManager.instance.GetScreen<SeriesResultsTeamScreen>().Setup(this.mTrophy);
  }
}
