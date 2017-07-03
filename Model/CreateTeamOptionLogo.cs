// Decompiled with JetBrains decompiler
// Type: CreateTeamOptionLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateTeamOptionLogo : CreateTeamOption
{
  public UITeamCreateLogo teamCreateLogo;
  public TextMeshProUGUI teamLogoLabel;
  public Button leftButtonTeamLogo;
  public Button rightButtonTeamLogo;
  public Image primaryColor;
  public Image secondaryColor;
  public Toggle primaryColorToggle;
  public Toggle secondaryColorToggle;
  public RectTransform primaryColorRect;
  public RectTransform secondaryColorRect;
  public CreateTeamScreen screen;

  public override bool isReady
  {
    get
    {
      return true;
    }
  }

  public override void OnStart()
  {
    this.leftButtonTeamLogo.onClick.AddListener(new UnityAction(this.OnLeftButtonTeamLogo));
    this.rightButtonTeamLogo.onClick.AddListener(new UnityAction(this.OnRightButtonTeamLogo));
    this.primaryColorToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPrimaryColorToggle));
    this.secondaryColorToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnSecondaryColorToggle));
  }

  public override void Setup()
  {
    this.UpdateColors();
    this.UpdateButtonTeamLogoState();
  }

  public override void OnEnter()
  {
    this.UpdateColors();
    this.UpdateButtonTeamLogoState();
  }

  private void UpdateColors()
  {
    this.primaryColor.color = CreateTeamManager.newTeamColor.customLogoColor.primary;
    this.secondaryColor.color = CreateTeamManager.newTeamColor.customLogoColor.secondary;
    this.teamCreateLogo.Refresh();
  }

  private void OnLeftButtonTeamLogo()
  {
    --CreateTeamManager.newTeam.customLogo.styleID;
    this.UpdateButtonTeamLogoState();
    this.teamCreateLogo.Refresh();
    this.screen.overviewWidget.RefreshLogo();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnRightButtonTeamLogo()
  {
    ++CreateTeamManager.newTeam.customLogo.styleID;
    this.UpdateButtonTeamLogoState();
    this.teamCreateLogo.Refresh();
    this.screen.overviewWidget.RefreshLogo();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void UpdateButtonTeamLogoState()
  {
    this.leftButtonTeamLogo.interactable = CreateTeamManager.newTeam.customLogo.styleID > 0;
    this.rightButtonTeamLogo.interactable = CreateTeamManager.newTeam.customLogo.styleID < UITeamCreateLogo.logos.Length - 1;
    this.UpdateTeamLogoText();
  }

  private void UpdateTeamLogoText()
  {
    StringVariableParser.intValue1 = CreateTeamManager.newTeam.customLogo.styleID + 1;
    this.teamLogoLabel.text = Localisation.LocaliseID("PSG_10012025", (GameObject) null);
  }

  private void OnPrimaryColorToggle(bool inValue)
  {
    if (inValue)
    {
      ColorPickerDialogBox.Open(this.primaryColorRect, CreateTeamManager.newTeamColor.customLogoColor.primary, (Color[]) null, false);
      ColorPickerDialogBox.OnColorPicked += new Action<Color>(this.OnPrimaryColorPicked);
      ColorPickerDialogBox.OnClose += new Action(this.OnPrimaryColorClose);
    }
    else
      ColorPickerDialogBox.Close();
  }

  private void OnSecondaryColorToggle(bool inValue)
  {
    if (inValue)
    {
      ColorPickerDialogBox.Open(this.secondaryColorRect, CreateTeamManager.newTeamColor.customLogoColor.secondary, (Color[]) null, false);
      ColorPickerDialogBox.OnColorPicked += new Action<Color>(this.OnSecondaryColorPicked);
      ColorPickerDialogBox.OnClose += new Action(this.OnSecondaryColorClose);
    }
    else
      ColorPickerDialogBox.Close();
  }

  private void OnPrimaryColorPicked(Color inColor)
  {
    CreateTeamManager.newTeamColor.customLogoColor.primary = inColor;
    this.UpdateColors();
    this.screen.overviewWidget.RefreshLogo();
  }

  private void OnPrimaryColorClose()
  {
    this.primaryColorToggle.isOn = false;
  }

  private void OnSecondaryColorPicked(Color inColor)
  {
    CreateTeamManager.newTeamColor.customLogoColor.secondary = inColor;
    this.UpdateColors();
    this.screen.overviewWidget.RefreshLogo();
  }

  private void OnSecondaryColorClose()
  {
    this.secondaryColorToggle.isOn = false;
  }

  private void OnDisable()
  {
    ColorPickerDialogBox.Close();
  }
}
