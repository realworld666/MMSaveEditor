// Decompiled with JetBrains decompiler
// Type: CreateTeamOptionUniform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateTeamOptionUniform : CreateTeamOption
{
  public UICharacterPortrait driver;
  public UICharacterPortrait engineer;
  public UICharacterPortrait mechanic;
  public UIDriverHelmet primaryHelmet;
  public UIDriverHelmet secondaryHelmet;
  public UIDriverHelmet tertiaryHelmet;
  public Image primaryColor;
  public Image secondaryColor;
  public Toggle primaryColorToggle;
  public Toggle secondaryColorToggle;
  public RectTransform primaryColorRect;
  public RectTransform secondaryColorRect;
  public TextMeshProUGUI hatStyleLabel;
  public Button leftButtonHatStyle;
  public Button rightButtonHatStyle;
  public TextMeshProUGUI shirtStyleLabel;
  public Button leftButtonShirtStyle;
  public Button rightButtonShirtStyle;
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
    this.leftButtonHatStyle.onClick.AddListener(new UnityAction(this.OnLeftButtonHatStyle));
    this.rightButtonHatStyle.onClick.AddListener(new UnityAction(this.OnRightButtonHatStyle));
    this.leftButtonShirtStyle.onClick.AddListener(new UnityAction(this.OnLeftButtonShirtStyle));
    this.rightButtonShirtStyle.onClick.AddListener(new UnityAction(this.OnRightButtonShirtStyle));
    this.primaryColorToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPrimaryColorToggle));
    this.secondaryColorToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnSecondaryColorToggle));
  }

  public override void Setup()
  {
    this.UpdateButtonHatStyleState();
    this.UpdateButtonShirtStyleState();
    this.UpdateTeamPortraits();
    this.UpdateColors();
  }

  public void UpdateTeamPortraits()
  {
    Person defaultPerson1 = CreateTeamManager.defaultPersons[0];
    Person defaultPerson2 = CreateTeamManager.defaultPersons[1];
    Person defaultPerson3 = CreateTeamManager.defaultPersons[2];
    this.driver.SetPortrait(defaultPerson1.portrait, defaultPerson1.gender, 25, -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Driver, CreateTeamManager.newTeam.driversHatStyle, CreateTeamManager.newTeam.driversBodyStyle);
    this.engineer.SetPortrait(defaultPerson2.portrait, defaultPerson2.gender, 25, -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Engineer, -1, CreateTeamManager.newTeam.driversBodyStyle);
    this.mechanic.SetPortrait(defaultPerson3.portrait, defaultPerson3.gender, 25, -1, CreateTeamManager.newTeamColor, UICharacterPortraitBody.BodyType.Mechanic, -1, CreateTeamManager.newTeam.driversBodyStyle);
    this.primaryHelmet.SetHelmet(CreateTeamManager.newTeamColor.helmetColor, UIDriverHelmet.HelmetType.FirstDriver);
    this.secondaryHelmet.SetHelmet(CreateTeamManager.newTeamColor.helmetColor, UIDriverHelmet.HelmetType.SecondDriver);
    this.tertiaryHelmet.SetHelmet(CreateTeamManager.newTeamColor.helmetColor, UIDriverHelmet.HelmetType.ReserveDriver);
  }

  private void UpdateColors()
  {
    this.primaryColor.color = CreateTeamManager.newTeamColor.staffColor.primary;
    this.secondaryColor.color = CreateTeamManager.newTeamColor.staffColor.secondary;
  }

  private void OnLeftButtonHatStyle()
  {
    --CreateTeamManager.newTeam.driversHatStyle;
    this.UpdateButtonHatStyleState();
    this.UpdateTeamPortraits();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnRightButtonHatStyle()
  {
    ++CreateTeamManager.newTeam.driversHatStyle;
    this.UpdateButtonHatStyleState();
    this.UpdateTeamPortraits();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void UpdateButtonHatStyleState()
  {
    this.leftButtonHatStyle.interactable = CreateTeamManager.newTeam.driversHatStyle > 0;
    this.rightButtonHatStyle.interactable = CreateTeamManager.newTeam.driversHatStyle < Portrait.hatStyles.Length - 1;
    this.UpdateHatStyleText();
  }

  private void UpdateHatStyleText()
  {
    StringVariableParser.intValue1 = CreateTeamManager.newTeam.driversHatStyle + 1;
    this.hatStyleLabel.text = Localisation.LocaliseID("PSG_10012027", (GameObject) null);
  }

  private void OnLeftButtonShirtStyle()
  {
    --CreateTeamManager.newTeam.driversBodyStyle;
    this.UpdateButtonShirtStyleState();
    this.UpdateTeamPortraits();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnRightButtonShirtStyle()
  {
    ++CreateTeamManager.newTeam.driversBodyStyle;
    this.UpdateButtonShirtStyleState();
    this.UpdateTeamPortraits();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void UpdateButtonShirtStyleState()
  {
    this.leftButtonShirtStyle.interactable = CreateTeamManager.newTeam.driversBodyStyle > 0;
    this.rightButtonShirtStyle.interactable = CreateTeamManager.newTeam.driversBodyStyle < Portrait.shirtStyles.Length - 1;
    this.UpdateShirtStyleText();
  }

  private void UpdateShirtStyleText()
  {
    StringVariableParser.intValue1 = CreateTeamManager.newTeam.driversBodyStyle + 1;
    this.shirtStyleLabel.text = Localisation.LocaliseID("PSG_10012026", (GameObject) null);
  }

  private void OnPrimaryColorToggle(bool inValue)
  {
    if (inValue)
    {
      ColorPickerDialogBox.Open(this.primaryColorRect, CreateTeamManager.newTeamColor.staffColor.primary, (Color[]) null, false);
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
      ColorPickerDialogBox.Open(this.secondaryColorRect, CreateTeamManager.newTeamColor.staffColor.secondary, (Color[]) null, false);
      ColorPickerDialogBox.OnColorPicked += new Action<Color>(this.OnSecondaryColorPicked);
      ColorPickerDialogBox.OnClose += new Action(this.OnSecondaryColorClose);
    }
    else
      ColorPickerDialogBox.Close();
  }

  private void OnPrimaryColorPicked(Color inColor)
  {
    CreateTeamManager.SetStaffColors(inColor, CreateTeamManager.newTeamColor.staffColor.secondary);
    this.UpdateColors();
    this.UpdateTeamPortraits();
    this.screen.overviewWidget.RefreshLogo();
  }

  private void OnPrimaryColorClose()
  {
    this.primaryColorToggle.isOn = false;
  }

  private void OnSecondaryColorPicked(Color inColor)
  {
    CreateTeamManager.SetStaffColors(CreateTeamManager.newTeamColor.staffColor.primary, inColor);
    this.UpdateColors();
    this.UpdateTeamPortraits();
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
