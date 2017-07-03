// Decompiled with JetBrains decompiler
// Type: UIStaffDetailsScreenDriverDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStaffDetailsScreenDriverDetailsWidget : MonoBehaviour
{
  public TextMeshProUGUI jobTitleFor;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI jobTitle;
  public TextMeshProUGUI name;
  public TextMeshProUGUI relationshipStatus;
  public Image relationshipEmoji;
  public Button swap;
  private Mechanic mMechanic;

  private void Awake()
  {
    this.swap.onClick.AddListener(new UnityAction(this.SwapDrivers));
  }

  public void Setup(Person inPerson)
  {
    this.mMechanic = (Mechanic) inPerson;
    Person inPerson1 = (Person) this.mMechanic.contract.GetTeam().GetDriver(this.mMechanic.driver) ?? (Person) this.mMechanic.contract.GetTeam().GetDriver(0);
    this.jobTitleFor.text = Localisation.LocaliseEnum((Enum) this.mMechanic.contract.job) + " For:";
    this.portrait.SetPortrait(inPerson1);
    this.name.text = inPerson1.shortName;
    this.SetRelationshipWithDriver(UnityEngine.Random.Range(0, 3));
    this.jobTitle.text = Localisation.LocaliseEnum((Enum) inPerson1.contract.proposedStatus);
  }

  private void SetRelationshipWithDriver(int relationshipQuality)
  {
    Sprite sprite;
    Color color;
    string str;
    if (relationshipQuality >= 2)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmiley");
      color = UIConstants.colorBandGreen;
      str = "Friendly";
    }
    else if (relationshipQuality >= 1)
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmiley");
      color = UIConstants.colorBandYellow;
      str = "Neutral";
    }
    else
    {
      sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmiley");
      color = UIConstants.colorBandRed;
      str = "Unfriendly";
    }
    this.relationshipStatus.text = str;
    this.relationshipStatus.color = color;
    this.relationshipEmoji.sprite = sprite;
    this.relationshipEmoji.color = color;
  }

  private void SwapDrivers()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    SwapStaffPopup dialog = UIManager.instance.dialogBoxManager.GetDialog<SwapStaffPopup>();
    dialog.SetupManager(this.mMechanic);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }
}
