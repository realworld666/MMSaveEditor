// Decompiled with JetBrains decompiler
// Type: UIPanelCarInfoStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPanelCarInfoStatEntry : MonoBehaviour
{
  public Color rankingOnGridDefaultColor = new Color(0.0f, 0.0f, 0.0f);
  private CarStats.StatType mStat = CarStats.StatType.Count;
  private Color mDefaultColor = new Color(0.0f, 0.0f, 0.0f);
  public TextMeshProUGUI statName;
  public TextMeshProUGUI statValue;
  public TextMeshProUGUI gridRanking;
  public UICarStatIcon icon;
  public GameObject greenArrow;
  public GameObject redArrow;
  private float mCarValue;
  private Car mCar;

  private void Awake()
  {
    this.mDefaultColor = this.statValue.color;
  }

  public void Setup(CarStats.StatType inStat, float inValue, Car inCar)
  {
    this.mCar = inCar;
    this.mStat = inStat;
    this.mCarValue = (float) Mathf.RoundToInt(inValue);
    this.icon.SetIcon(this.mStat, inCar.carManager.team.championship.series);
    this.statName.text = ((CarStats.CarStatShortName) this.mStat).ToString();
    this.statValue.text = Mathf.RoundToInt(this.mCarValue).ToString();
    this.statValue.color = this.mDefaultColor;
    this.statName.color = this.mDefaultColor;
    this.greenArrow.SetActive(false);
    this.redArrow.SetActive(false);
    this.UpdateRanking();
  }

  private void UpdateRanking()
  {
    int inPosition = CarManager.GetCarStandingsOnStat(this.mStat, Game.instance.player.team.championship).IndexOf(this.mCar) + 1;
    this.gridRanking.text = GameUtility.FormatForPosition(inPosition, (string) null);
    if (inPosition == 1)
      this.gridRanking.color = UIConstants.sectorSessionFastestColor;
    else
      this.gridRanking.color = this.rankingOnGridDefaultColor;
  }

  public void ShowComparison(float inValue)
  {
    if (this.statValue.text == Mathf.RoundToInt(inValue).ToString())
      return;
    this.statValue.text = Mathf.RoundToInt(inValue).ToString();
    if ((double) this.mCarValue > (double) Mathf.RoundToInt(inValue))
    {
      this.statName.color = UIConstants.negativeColor;
      this.statValue.color = UIConstants.negativeColor;
      this.greenArrow.SetActive(false);
      this.redArrow.SetActive(true);
    }
    else if ((double) this.mCarValue < (double) Mathf.RoundToInt(inValue))
    {
      this.statName.color = UIConstants.positiveColor;
      this.statValue.color = UIConstants.positiveColor;
      this.greenArrow.SetActive(true);
      this.redArrow.SetActive(false);
    }
    else
    {
      this.statName.color = this.mDefaultColor;
      this.statValue.color = this.mDefaultColor;
      this.greenArrow.SetActive(false);
      this.redArrow.SetActive(false);
    }
  }
}
