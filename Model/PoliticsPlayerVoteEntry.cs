// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVoteEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PoliticsPlayerVoteEntry : MonoBehaviour
{
  public Toggle toggle;
  public TextMeshProUGUI ruleName;
  public TextMeshProUGUI ruleDescription;
  public GameObject tick;
  public PoliticsPlayerVotePopup widget;
  private PoliticalVote mVote;

  public void Setup(PoliticalVote inVote, bool inSelected = false)
  {
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.isOn = inSelected;
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    this.mVote = inVote;
    if (this.mVote.HasImpactOfType<PoliticalImpactChangeTrack>())
    {
      this.SetTrackImpactLabelsGeneric(this.mVote.GetImpactOfType<PoliticalImpactChangeTrack>());
    }
    else
    {
      this.ruleName.text = this.mVote.GetName();
      this.ruleDescription.text = this.mVote.GetDescription();
    }
    this.SetGraphic();
  }

  private void SetTrackImpactLabelsGeneric(PoliticalImpactChangeTrack inImpact)
  {
    string str1 = string.Empty;
    string str2 = string.Empty;
    switch (inImpact.impactType)
    {
      case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
        str1 = Localisation.LocaliseID("PSG_10011868", (GameObject) null);
        str2 = Localisation.LocaliseID("PSG_10011870", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
        str1 = Localisation.LocaliseID("PSG_10011869", (GameObject) null);
        str2 = Localisation.LocaliseID("PSG_10011871", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrack:
        str1 = Localisation.LocaliseID("PSG_10011864", (GameObject) null);
        str2 = Localisation.LocaliseID("PSG_10011873", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
        str1 = Localisation.LocaliseID("PSG_10011865", (GameObject) null);
        str2 = Localisation.LocaliseID("PSG_10011872", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
        str1 = Localisation.LocaliseID("PSG_10011866", (GameObject) null);
        str2 = Localisation.LocaliseID("PSG_10011874", (GameObject) null);
        break;
    }
    this.ruleName.text = str1;
    this.ruleDescription.text = str2;
  }

  private void OnToggle(bool inValue)
  {
    if (inValue)
      this.widget.SelectVote(this.mVote);
    this.SetGraphic();
  }

  private void SetGraphic()
  {
    GameUtility.SetActive(this.tick, this.toggle.isOn);
  }
}
