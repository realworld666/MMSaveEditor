// Decompiled with JetBrains decompiler
// Type: SetupKnowledgeFeedbackEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupKnowledgeFeedbackEntry : MonoBehaviour
{
  private string mFeedbackString = string.Empty;
  private Color mOpinionColor = UIConstants.whiteColor;
  public TextMeshProUGUI feedbackLabel;
  public Image backgroundImage;
  public Image smiley;
  private Sprite mSprite;

  public void SetOpinion(SessionSetup.SetupOpinion inSetupOpinion)
  {
    switch (inSetupOpinion)
    {
      case SessionSetup.SetupOpinion.None:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10009318", (GameObject) null);
        this.mOpinionColor = UIConstants.colorSetupOpinionNone;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-ThinkingSmileyLarge");
        break;
      case SessionSetup.SetupOpinion.VeryPoor:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10010206", (GameObject) null);
        this.mOpinionColor = UIConstants.colorSetupOpinionVeryPoor;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmileyLarge");
        break;
      case SessionSetup.SetupOpinion.Poor:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10010207", (GameObject) null);
        this.mOpinionColor = UIConstants.colorSetupOpinionPoor;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmileyLarge2");
        break;
      case SessionSetup.SetupOpinion.OK:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10010208", (GameObject) null);
        this.mOpinionColor = UIConstants.colorSetupOpinionOK;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmileyLarge");
        break;
      case SessionSetup.SetupOpinion.Good:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10010209", (GameObject) null);
        this.mOpinionColor = UIConstants.colorSetupOpinionGood;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge2");
        break;
      case SessionSetup.SetupOpinion.Great:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10010180", (GameObject) null);
        this.mOpinionColor = UIConstants.colorSetupOpinionGreat;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge");
        break;
      case SessionSetup.SetupOpinion.Excellent:
        this.mFeedbackString = Localisation.LocaliseID("PSG_10010210", (GameObject) null);
        this.mOpinionColor = UIConstants.sectorSessionFastestColor;
        this.mSprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge3");
        break;
    }
    if ((Object) this.feedbackLabel != (Object) null && this.mFeedbackString != string.Empty)
      this.feedbackLabel.text = this.mFeedbackString;
    if ((Object) this.backgroundImage != (Object) null && this.mOpinionColor != UIConstants.whiteColor)
      this.backgroundImage.color = this.mOpinionColor;
    if (!((Object) this.smiley != (Object) null) || !((Object) this.mSprite != (Object) null))
      return;
    this.smiley.sprite = this.mSprite;
  }
}
