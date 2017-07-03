// Decompiled with JetBrains decompiler
// Type: ContractReactionRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContractReactionRollover : UIDialogBox
{
  private RectTransform mTransform;
  [SerializeField]
  private TextMeshProUGUI title;
  [SerializeField]
  private TextMeshProUGUI description;
  [SerializeField]
  private Image smileyFace;

  public void ShowRollover(ContractEvaluationPerson.ReactionType inReactionType)
  {
    this.mTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
    this.SetupTitle(inReactionType);
    this.SetupDescription(inReactionType);
    this.SetupReaction(inReactionType);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  private void SetupTitle(ContractEvaluationPerson.ReactionType inReactionType)
  {
    this.title.text = Localisation.LocaliseEnum((Enum) inReactionType);
  }

  private void SetupDescription(ContractEvaluationPerson.ReactionType inReactionType)
  {
    switch (inReactionType)
    {
      case ContractEvaluationPerson.ReactionType.Insulted:
        this.description.text = Localisation.LocaliseID("PSG_10010147", (GameObject) null);
        break;
      case ContractEvaluationPerson.ReactionType.UnHappy:
        this.description.text = Localisation.LocaliseID("PSG_10010148", (GameObject) null);
        break;
      case ContractEvaluationPerson.ReactionType.Neutral:
        this.description.text = Localisation.LocaliseID("PSG_10010149", (GameObject) null);
        break;
      case ContractEvaluationPerson.ReactionType.Delighted:
        this.description.text = Localisation.LocaliseID("PSG_10010150", (GameObject) null);
        break;
    }
  }

  private void SetupReaction(ContractEvaluationPerson.ReactionType inValue)
  {
    this.smileyFace.sprite = inValue != ContractEvaluationPerson.ReactionType.Delighted ? (inValue != ContractEvaluationPerson.ReactionType.Neutral ? (inValue != ContractEvaluationPerson.ReactionType.UnHappy ? App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AngrySmileyLarge") : App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmileyLarge")) : App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmileyLarge")) : App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge");
  }
}
