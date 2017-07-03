// Decompiled with JetBrains decompiler
// Type: UIMediaReport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIMediaReport : MonoBehaviour
{
  public TextMeshProUGUI date;
  public TextMeshProUGUI championshipLabel;
  public TextMeshProUGUI sessionReactionLabel;
  public TextMeshProUGUI title;
  public TextMeshProUGUI body;
  public TextMeshProUGUI likesNumber;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI writersText;
  public UIPressLogo logo;
  public UICircuitImage trackImage;
  private MediaStory mCurrentStory;

  public void SetStory(MediaStory inMediaStory)
  {
    this.logo.SetMediaOutlet(inMediaStory.journalist.contract.GetMediaOutlet());
    this.trackImage.SetCircuitIcon(Game.instance.sessionManager.eventDetails.circuit);
    this.mCurrentStory = inMediaStory;
    this.mCurrentStory.journalist.contract.GetMediaOutlet().ApplyColours(this.gameObject);
    this.portrait.SetPortrait(this.mCurrentStory.journalist);
    this.writersText.text = this.mCurrentStory.journalist.contract.employeerName + "\n" + GameUtility.FormatDateTimeToShortDateString(Game.instance.time.now) + "\n" + this.mCurrentStory.journalist.name;
    this.body.text = this.mCurrentStory.GetText();
    this.title.text = this.mCurrentStory.localisedTitle;
    this.date.text = GameUtility.FormatDateTimeToLongDateString(Game.instance.time.now, string.Empty);
    StringVariableParser.sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    this.sessionReactionLabel.text = Localisation.LocaliseID("PSG_10010992", (GameObject) null);
    this.championshipLabel.text = Game.instance.player.team.championship.GetChampionshipName(false);
    this.likesNumber.text = RandomUtility.GetRandom(100, 10000).ToString();
  }
}
