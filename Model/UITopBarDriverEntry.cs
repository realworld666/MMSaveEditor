// Decompiled with JetBrains decompiler
// Type: UITopBarDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITopBarDriverEntry : MonoBehaviour
{
  public Image colorStripe;
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI pointsLabel;
  public UIGridList eventPointsList;
  private Team mTeam;
  private Driver mDriver;

  public void SetChampionshipEntry(ChampionshipEntry_v1 inEntry)
  {
    if (inEntry == null)
      return;
    this.mDriver = inEntry.GetEntity<Driver>();
    this.mTeam = this.mDriver.contract.GetTeam();
    int championshipPosition = inEntry.GetCurrentChampionshipPosition();
    this.colorStripe.color = this.mTeam.GetTeamColor().primaryUIColour.normal;
    this.positionLabel.text = championshipPosition.ToString();
    this.nameLabel.text = this.mDriver.lastName;
    this.pointsLabel.text = inEntry.GetCurrentPoints().ToString();
    int count = inEntry.championship.calendar.Count;
    int itemCount1 = this.eventPointsList.itemCount;
    int num = count - itemCount1;
    for (int index = 0; index < num; ++index)
      this.eventPointsList.CreateListItem<UIDriverEventData>();
    int itemCount2 = this.eventPointsList.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UIDriverEventData uiDriverEventData = this.eventPointsList.GetItem<UIDriverEventData>(inIndex);
      if (inIndex < count)
        uiDriverEventData.SetPointsForEvent(this.mDriver, inEntry.championship.calendar[inIndex]);
      GameUtility.SetActive(uiDriverEventData.gameObject, inIndex < count);
    }
  }
}
