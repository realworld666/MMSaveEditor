// Decompiled with JetBrains decompiler
// Type: UIConstructorsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConstructorsEntry : MonoBehaviour
{
  public UIConstructorsEntry.BarType barType = UIConstructorsEntry.BarType.Darker;
  public Image[] backing = new Image[0];
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI teamNameLabel;
  public TextMeshProUGUI pointsLabel;
  public TextMeshProUGUI gapLabel;
  public Image teamColorStrip;
  public Image greenArrow;
  public Image redArrow;
  public UICar uiCar;

  private void Start()
  {
    this.greenArrow.color = UIConstants.positiveColor;
    this.redArrow.color = UIConstants.negativeColor;
  }

  public void SetInfo(Team inTeam, ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inFirstPlaceEntry)
  {
    this.teamColorStrip.color = inTeam.GetTeamColor().primaryUIColour.normal;
    this.uiCar.SetTeamColor(inTeam.GetTeamColor().carColor);
    this.positionLabel.text = inEntry.GetCurrentChampionshipPosition().ToString();
    this.teamNameLabel.text = inTeam.name;
    this.pointsLabel.text = inEntry.GetCurrentPoints().ToString();
    int num = inFirstPlaceEntry.GetCurrentPoints() - inEntry.GetCurrentPoints();
    if (num == 0)
      this.gapLabel.text = "-";
    else
      this.gapLabel.text = "-" + num.ToString();
    if (inTeam == Game.instance.player.team)
      this.barType = UIConstructorsEntry.BarType.PlayerOwned;
    this.SetArrows(inEntry.GetPreviousChampionshipPosition(), inEntry.GetCurrentChampionshipPosition());
    this.SetBarType();
  }

  private void SetArrows(int inPreviousPosition, int inPosition)
  {
    if (inPreviousPosition > inPosition)
    {
      this.greenArrow.gameObject.SetActive(true);
      this.redArrow.gameObject.SetActive(false);
    }
    else if (inPreviousPosition < inPosition)
    {
      this.redArrow.gameObject.SetActive(true);
      this.greenArrow.gameObject.SetActive(false);
    }
    else
    {
      this.redArrow.gameObject.SetActive(false);
      this.greenArrow.gameObject.SetActive(false);
    }
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
    {
      if ((UIConstructorsEntry.BarType) index == this.barType)
        this.backing[index].gameObject.SetActive(true);
      else
        this.backing[index].gameObject.SetActive(false);
    }
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
