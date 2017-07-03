// Decompiled with JetBrains decompiler
// Type: UIPersonContractDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIPersonContractDetailsWidget : MonoBehaviour
{
  public TextMeshProUGUI expires;
  public TextMeshProUGUI breakContract;
  public TextMeshProUGUI perRaceCost;
  public TextMeshProUGUI status;
  public TextMeshProUGUI qualifyingBonusPosition;
  public TextMeshProUGUI qualifyingBonus;
  public TextMeshProUGUI raceBonusPosition;
  public TextMeshProUGUI raceBonus;

  public void Setup(Person mPerson)
  {
    if (!mPerson.IsFreeAgent() && !(mPerson.contract.GetTeam() is NullTeam) && mPerson.CanShowStats())
    {
      if (mPerson.contract.IsContractForReplacementPerson())
      {
        this.expires.text = Localisation.LocaliseID("PSG_10010607", (GameObject) null);
      }
      else
      {
        int monthsRemaining = mPerson.contract.GetMonthsRemaining();
        StringVariableParser.intValue1 = monthsRemaining;
        this.expires.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
      }
      int contractTerminationCost = mPerson.contract.GetContractTerminationCost();
      this.breakContract.text = GameUtility.GetCurrencyString((long) -contractTerminationCost, 0);
      this.breakContract.color = GameUtility.GetCurrencyColor(-contractTerminationCost);
      long perRaceCost = mPerson.contract.perRaceCost;
      this.perRaceCost.text = GameUtility.GetCurrencyString(-perRaceCost, 0);
      this.perRaceCost.color = GameUtility.GetCurrencyColor(-perRaceCost);
      if ((Object) this.status != (Object) null)
        this.status.text = mPerson.contract.GetProposedStatusText();
      if (mPerson.contract.qualifyingBonusTargetPosition == 0 || mPerson.contract.qualifyingBonus == 0)
      {
        this.qualifyingBonus.text = "-";
        this.qualifyingBonus.color = Color.white;
        this.qualifyingBonusPosition.text = "-";
      }
      else
      {
        this.qualifyingBonusPosition.text = GameUtility.FormatForPositionOrAbove(mPerson.contract.qualifyingBonusTargetPosition, (string) null);
        this.qualifyingBonus.text = GameUtility.GetCurrencyString((long) -mPerson.contract.qualifyingBonus, 0);
        this.qualifyingBonus.color = GameUtility.GetCurrencyColor(-mPerson.contract.qualifyingBonus);
      }
      if (mPerson.contract.raceBonusTargetPosition == 0 || mPerson.contract.raceBonus == 0)
      {
        this.raceBonusPosition.text = "-";
        this.raceBonus.text = "-";
        this.raceBonus.color = Color.white;
      }
      else
      {
        this.raceBonusPosition.text = GameUtility.FormatForPositionOrAbove(mPerson.contract.raceBonusTargetPosition, (string) null);
        this.raceBonus.text = GameUtility.GetCurrencyString((long) -mPerson.contract.raceBonus, 0);
        this.raceBonus.color = GameUtility.GetCurrencyColor(-mPerson.contract.raceBonus);
      }
    }
    else
    {
      if ((Object) this.status != (Object) null)
        this.status.text = "-";
      this.expires.color = Color.white;
      this.expires.text = "-";
      this.breakContract.color = Color.white;
      this.breakContract.text = "-";
      this.perRaceCost.color = Color.white;
      this.perRaceCost.text = "-";
      this.qualifyingBonus.color = Color.white;
      this.qualifyingBonus.text = "-";
      this.raceBonus.color = Color.white;
      this.raceBonus.text = "-";
      this.qualifyingBonusPosition.color = Color.white;
      this.qualifyingBonusPosition.text = "-";
      this.raceBonusPosition.color = Color.white;
      this.raceBonusPosition.text = "-";
    }
  }
}
