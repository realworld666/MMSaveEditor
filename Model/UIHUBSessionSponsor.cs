// Decompiled with JetBrains decompiler
// Type: UIHUBSessionSponsor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUBSessionSponsor : MonoBehaviour
{
  public TextMeshProUGUI sponsorPosition;
  public TextMeshProUGUI sponsorBonus;
  public GameObject orAboveLabel;
  public Image rewardBacking;
  public GameObject onTarget;
  public GameObject offTarget;
  private SponsorshipDeal mSponsorshipDeal;
  private SessionObjective mSponsorObjective;

  public void Setup()
  {
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.sessionType;
    this.mSponsorshipDeal = Game.instance.player.team.sponsorController.weekendSponsorshipDeal;
    GameUtility.SetActive(this.gameObject, this.mSponsorshipDeal != null && sessionType != SessionDetails.SessionType.Practice);
    if (!this.gameObject.activeSelf)
      return;
    switch (sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.mSponsorObjective = this.mSponsorshipDeal.qualifyingObjective;
        break;
      case SessionDetails.SessionType.Qualifying:
        this.mSponsorObjective = this.mSponsorshipDeal.qualifyingObjective;
        break;
      case SessionDetails.SessionType.Race:
        this.mSponsorObjective = this.mSponsorshipDeal.raceObjective;
        break;
    }
    this.SetDetails();
  }

  private void SetDetails()
  {
    if (this.mSponsorObjective == null)
      return;
    this.sponsorPosition.text = GameUtility.FormatForPosition(this.mSponsorObjective.targetResult, (string) null);
    this.sponsorBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorObjective.financialReward, 0);
    GameUtility.SetActive(this.orAboveLabel, this.mSponsorObjective.targetResult != 1);
    bool flag = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle().standingsPosition <= this.mSponsorObjective.targetResult;
    GameState.Type type = App.instance.gameStateManager.currentState.type;
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Qualifying && (type == GameState.Type.PreSessionHUB || type == GameState.Type.QualifyingPreSession))
      flag = true;
    if (flag)
    {
      this.sponsorPosition.color = UIConstants.sponsorPositiveColor;
      if ((Object) this.rewardBacking != (Object) null)
        this.rewardBacking.color = UIConstants.sponsorPositiveColor;
      if (!((Object) this.onTarget != (Object) null) || !((Object) this.offTarget != (Object) null))
        return;
      GameUtility.SetActive(this.onTarget, true);
      GameUtility.SetActive(this.offTarget, false);
    }
    else
    {
      this.sponsorPosition.color = UIConstants.sponsorNegativeColor;
      if ((Object) this.rewardBacking != (Object) null)
        this.rewardBacking.color = UIConstants.sponsorNegativeColor;
      if (!((Object) this.onTarget != (Object) null) || !((Object) this.offTarget != (Object) null))
        return;
      GameUtility.SetActive(this.onTarget, false);
      GameUtility.SetActive(this.offTarget, true);
    }
  }
}
