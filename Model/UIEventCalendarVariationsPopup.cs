// Decompiled with JetBrains decompiler
// Type: UIEventCalendarVariationsPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventCalendarVariationsPopup : UIDialogBox
{
  public Button closeButton;
  public Button backButton;
  public TextMeshProUGUI ruleDescription;
  public TextMeshProUGUI header;
  public GameObject arrow;
  public UIGridList layoutGrid;
  private Circuit mCircuit;
  private PoliticalVote mPoliticalVote;
  private PoliticalImpactChangeTrack mPoliticalImpact;
  private Action mCloseAction;

  public void Start()
  {
    this.closeButton.onClick.AddListener(new UnityAction(this.Close));
    this.backButton.onClick.AddListener(new UnityAction(this.Close));
  }

  public static void ShowAllTrackLayouts(Circuit inCircuit, Action inAction = null)
  {
    UIEventCalendarVariationsPopup dialog = UIManager.instance.dialogBoxManager.GetDialog<UIEventCalendarVariationsPopup>();
    dialog.SetCircuit(inCircuit, inAction);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void ShowTrackLayoutPolitics(PoliticalVote inPoliticalVote, Action inAction = null)
  {
    UIEventCalendarVariationsPopup dialog = UIManager.instance.dialogBoxManager.GetDialog<UIEventCalendarVariationsPopup>();
    dialog.SetPoliticalVote(inPoliticalVote, inAction);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void HidePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIEventCalendarVariationsPopup>().Hide();
  }

  public void SetPoliticalVote(PoliticalVote inPoliticalVote, Action inAction = null)
  {
    if (inPoliticalVote == null)
      return;
    this.mPoliticalVote = inPoliticalVote;
    this.mPoliticalImpact = this.mPoliticalVote.GetImpactOfType<PoliticalImpactChangeTrack>();
    this.mCloseAction = inAction;
    this.SetRuleTrackLayouts();
  }

  public void SetCircuit(Circuit inCircuit, Action inAction = null)
  {
    if (inCircuit == null)
      return;
    this.mCircuit = inCircuit;
    this.mCloseAction = inAction;
    this.SetAllTrackLayouts();
  }

  private void SetRuleTrackLayouts()
  {
    this.header.text = this.mPoliticalVote.GetName();
    GameUtility.SetActive(this.ruleDescription.gameObject, true);
    this.ruleDescription.text = this.mPoliticalVote.GetDescription();
    int num1 = 0;
    Circuit circuit1 = (Circuit) null;
    Circuit circuit2 = (Circuit) null;
    string str1 = string.Empty;
    string str2 = string.Empty;
    switch (this.mPoliticalImpact.impactType)
    {
      case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
      case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
        GameUtility.SetActive(this.arrow, true);
        num1 = 2;
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrack:
      case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
      case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
        GameUtility.SetActive(this.arrow, false);
        num1 = 1;
        break;
    }
    switch (this.mPoliticalImpact.impactType)
    {
      case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
        circuit1 = this.mPoliticalImpact.trackAffected;
        str1 = Localisation.LocaliseID("PSG_10009985", (GameObject) null);
        circuit2 = this.mPoliticalImpact.trackLayout;
        str2 = Localisation.LocaliseID("PSG_10009987", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
        circuit1 = this.mPoliticalImpact.trackAffected;
        str1 = Localisation.LocaliseID("PSG_10009984", (GameObject) null);
        circuit2 = this.mPoliticalImpact.newTrack;
        str2 = Localisation.LocaliseID("PSG_10009988", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrack:
        circuit1 = this.mPoliticalImpact.newTrack;
        str1 = Localisation.LocaliseID("PSG_10009986", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
        circuit1 = this.mPoliticalImpact.newTrack;
        str1 = Localisation.LocaliseID("PSG_10009987", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
        circuit1 = this.mPoliticalImpact.trackAffected;
        str1 = Localisation.LocaliseID("PSG_10009984", (GameObject) null);
        break;
    }
    int itemCount1 = this.layoutGrid.itemCount;
    int num2 = num1 - itemCount1;
    GameUtility.SetActive(this.layoutGrid.itemPrefab, true);
    for (int index = 0; index < num2; ++index)
      this.layoutGrid.CreateListItem<UIEventCalendarVariationEntry>();
    GameUtility.SetActive(this.layoutGrid.itemPrefab, false);
    int itemCount2 = this.layoutGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UIEventCalendarVariationEntry calendarVariationEntry = this.layoutGrid.GetItem<UIEventCalendarVariationEntry>(inIndex);
      GameUtility.SetActive(calendarVariationEntry.gameObject, inIndex < num1);
      if (calendarVariationEntry.gameObject.activeSelf)
        calendarVariationEntry.SetupVote(inIndex != 0 ? circuit2 : circuit1, inIndex != 0 ? str2 : str1);
    }
    if (!this.arrow.activeSelf)
      return;
    GameUtility.SetSiblingIndex(this.arrow, 2);
  }

  private void SetAllTrackLayouts()
  {
    GameUtility.SetActive(this.ruleDescription.gameObject, false);
    GameUtility.SetActive(this.arrow, false);
    List<Circuit> circuitsByLocation = Game.instance.circuitManager.GetCircuitsByLocation(this.mCircuit.locationName);
    int count = circuitsByLocation.Count;
    int itemCount1 = this.layoutGrid.itemCount;
    int num = count - itemCount1;
    this.header.text = Localisation.LocaliseID(this.mCircuit.locationNameID, (GameObject) null) + " " + (count <= 0 ? Localisation.LocaliseID("PSG_10009983", (GameObject) null) : Localisation.LocaliseID("PSG_10009982", (GameObject) null));
    GameUtility.SetActive(this.layoutGrid.itemPrefab, true);
    for (int index = 0; index < num; ++index)
      this.layoutGrid.CreateListItem<UIEventCalendarVariationEntry>();
    GameUtility.SetActive(this.layoutGrid.itemPrefab, false);
    int itemCount2 = this.layoutGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UIEventCalendarVariationEntry calendarVariationEntry = this.layoutGrid.GetItem<UIEventCalendarVariationEntry>(inIndex);
      GameUtility.SetActive(calendarVariationEntry.gameObject, inIndex < count);
      if (calendarVariationEntry.gameObject.activeSelf)
        calendarVariationEntry.Setup(circuitsByLocation[inIndex]);
    }
  }

  public void Close()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Hide();
    if (this.mCloseAction == null)
      return;
    this.mCloseAction();
    this.mCloseAction = (Action) null;
  }
}
