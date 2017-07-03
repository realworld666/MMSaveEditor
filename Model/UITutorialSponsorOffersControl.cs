// Decompiled with JetBrains decompiler
// Type: UITutorialSponsorOffersControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class UITutorialSponsorOffersControl : UITutorialBespokeScript
{
  protected override void Activate()
  {
    Team team = Game.instance.player.team;
    team.sponsorController.ClearAllSponsorOffers();
    if (team.sponsorController.GetTotalOffersCount() > 0)
      return;
    this.FindSponsorToAddToTeamPlayersOffers().CreateSponsorOffer(team);
  }

  private Sponsor FindSponsorToAddToTeamPlayersOffers()
  {
    Team team = Game.instance.player.team;
    List<Sponsor> entityList = Game.instance.sponsorManager.GetEntityList();
    Sponsor sponsor = (Sponsor) null;
    for (int index = 0; index < entityList.Count; ++index)
    {
      Sponsor inSponsor = entityList[index];
      if (!inSponsor.IsTeamIgnored(team) && team.sponsorAppeal >= inSponsor.prestigeLevel && (team.championship != null && !team.championship.HasSeasonEnded()) && (!team.sponsorController.OfferExistForSponsorInSlot(inSponsor.slotSponsoring, inSponsor) && !team.sponsorController.IsSlotAlreadySponsored(inSponsor.slotSponsoring) && team.sponsorController.sponsorOffers.GetMap(inSponsor.slotSponsoring).Count < 3))
      {
        sponsor = inSponsor;
        break;
      }
    }
    return sponsor;
  }
}
