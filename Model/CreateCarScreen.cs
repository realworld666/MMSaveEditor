// Decompiled with JetBrains decompiler
// Type: CreateCarScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class CreateCarScreen : MonoBehaviour
{
  [SerializeField]
  private Slider[] sliders = new Slider[5];
  [SerializeField]
  private Dropdown[] sponsorDropdowns = new Dropdown[6];
  private TeamColor.LiveryColour mColour = new TeamColor.LiveryColour();
  private ModManager modManager;
  private AssetManager assetManager;
  private Database mDatabase;
  private CarPartModelDatabase mCarPartModelDatabase;
  private LiveryManager mLiveryManager;
  private TeamColorManager mTeamColourManager;
  private CheapSponsorDatabase mSponsorDatabase;
  private FrontendCarManager mCarManager;
  private FrontendCar mCar;
  [SerializeField]
  private Dropdown chassisDropdown;
  [SerializeField]
  private Dropdown brakesDropdown;
  [SerializeField]
  private Dropdown wheelsDropdown;
  [SerializeField]
  private Dropdown rearWheelsDropdown;
  [SerializeField]
  private Dropdown frontWingDropdown;
  [SerializeField]
  private Dropdown rearWingDropdown;
  [SerializeField]
  private Button primaryColourPickColourButton;
  [SerializeField]
  private HSVPicker primaryColourPicker;
  [SerializeField]
  private Button secondaryColourPickColourButton;
  [SerializeField]
  private HSVPicker secondaryColourPicker;
  [SerializeField]
  private Button tertiaryColourPickColourButton;
  [SerializeField]
  private HSVPicker tertiaryColourPicker;
  [SerializeField]
  private Button trimColourPickColourButton;
  [SerializeField]
  private HSVPicker trimColourPicker;
  [SerializeField]
  private Dropdown liveryDropdown;
  [SerializeField]
  private Dropdown colourDropdown;
  [SerializeField]
  private Button saveButton;
  [SerializeField]
  private Button randomisePartsButton;
  [SerializeField]
  private Button randomiseAllButton;
  [SerializeField]
  private Button copyMaterialToLiveryDataButton;
  [SerializeField]
  private Button copyChassisLiveryToFrontWingButton;
  [SerializeField]
  private Button copyChassisLiveryToRearWingButton;

  private void Awake()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreateCarScreen.\u003CAwake\u003Ec__AnonStorey6C awakeCAnonStorey6C = new CreateCarScreen.\u003CAwake\u003Ec__AnonStorey6C();
    // ISSUE: reference to a compiler-generated field
    awakeCAnonStorey6C.\u003C\u003Ef__this = this;
    this.modManager = new ModManager();
    this.modManager.Start();
    this.assetManager = new AssetManager(this.modManager);
    this.mDatabase = new Database(this.assetManager, this.modManager);
    this.mDatabase.LoadGameDatabase();
    this.mCarPartModelDatabase = new CarPartModelDatabase(this.mDatabase, true);
    this.mLiveryManager = new LiveryManager();
    this.mLiveryManager.LoadFromDatabase(this.mDatabase);
    this.mTeamColourManager = new TeamColorManager();
    this.mSponsorDatabase = new CheapSponsorDatabase(this.mDatabase);
    this.mCarManager = new FrontendCarManager();
    TeamColor defaultColour = TeamColor.defaultColour;
    this.UpdateColoursFromColourSet(defaultColour);
    this.UpdatePickersAndButtonsToCurrentColour();
    // ISSUE: reference to a compiler-generated method
    this.primaryColourPickColourButton.onClick.AddListener(new UnityAction(awakeCAnonStorey6C.\u003C\u003Em__D2));
    // ISSUE: reference to a compiler-generated method
    this.secondaryColourPickColourButton.onClick.AddListener(new UnityAction(awakeCAnonStorey6C.\u003C\u003Em__D3));
    // ISSUE: reference to a compiler-generated method
    this.tertiaryColourPickColourButton.onClick.AddListener(new UnityAction(awakeCAnonStorey6C.\u003C\u003Em__D4));
    // ISSUE: reference to a compiler-generated method
    this.trimColourPickColourButton.onClick.AddListener(new UnityAction(awakeCAnonStorey6C.\u003C\u003Em__D5));
    // ISSUE: reference to a compiler-generated method
    this.primaryColourPicker.onValueChanged.AddListener(new UnityAction<Color>(awakeCAnonStorey6C.\u003C\u003Em__D6));
    // ISSUE: reference to a compiler-generated method
    this.secondaryColourPicker.onValueChanged.AddListener(new UnityAction<Color>(awakeCAnonStorey6C.\u003C\u003Em__D7));
    // ISSUE: reference to a compiler-generated method
    this.tertiaryColourPicker.onValueChanged.AddListener(new UnityAction<Color>(awakeCAnonStorey6C.\u003C\u003Em__D8));
    // ISSUE: reference to a compiler-generated method
    this.trimColourPicker.onValueChanged.AddListener(new UnityAction<Color>(awakeCAnonStorey6C.\u003C\u003Em__D9));
    List<Dropdown.OptionData> optionDataList1 = new List<Dropdown.OptionData>(this.mLiveryManager.liveries.Length);
    for (int index = 0; index < this.mLiveryManager.liveries.Length; ++index)
      optionDataList1.Add(new Dropdown.OptionData("Livery " + this.mLiveryManager.liveries[index].id.ToString()));
    this.liveryDropdown.set_options(optionDataList1);
    this.liveryDropdown.onValueChanged.AddListener(new UnityAction<int>(this.LiveryDropdownChanged));
    List<Dropdown.OptionData> optionDataList2 = new List<Dropdown.OptionData>(this.mTeamColourManager.colours.Length);
    for (int index = 0; index < this.mTeamColourManager.colours.Length; ++index)
      optionDataList2.Add(new Dropdown.OptionData("Colour " + this.mTeamColourManager.colours[index].colorID.ToString()));
    this.colourDropdown.set_options(optionDataList2);
    this.colourDropdown.onValueChanged.AddListener(new UnityAction<int>(this.ColourDropdownChanged));
    this.randomisePartsButton.onClick.AddListener(new UnityAction(this.RandomiseParts));
    this.randomiseAllButton.onClick.AddListener(new UnityAction(this.RandomiseAll));
    List<Dropdown.OptionData> optionDataList3 = new List<Dropdown.OptionData>(this.mCarPartModelDatabase.NumOptionsForPartType(CarPartModelDatabase.CarPartModelType.Chassis));
    // ISSUE: reference to a compiler-generated field
    awakeCAnonStorey6C.chassisOptions = this.mCarPartModelDatabase.ModelsForPartType(CarPartModelDatabase.CarPartModelType.Chassis);
    // ISSUE: reference to a compiler-generated field
    for (int index = 0; index < awakeCAnonStorey6C.chassisOptions.Count; ++index)
    {
      // ISSUE: reference to a compiler-generated field
      optionDataList3.Add(new Dropdown.OptionData(awakeCAnonStorey6C.chassisOptions[index].assetPath));
    }
    this.chassisDropdown.set_options(optionDataList3);
    // ISSUE: reference to a compiler-generated method
    this.chassisDropdown.onValueChanged.AddListener(new UnityAction<int>(awakeCAnonStorey6C.\u003C\u003Em__DA));
    List<Dropdown.OptionData> optionDataList4 = new List<Dropdown.OptionData>(this.mCarPartModelDatabase.NumOptionsForPartType(CarPartModelDatabase.CarPartModelType.Brake));
    // ISSUE: reference to a compiler-generated field
    awakeCAnonStorey6C.brakesOptions = this.mCarPartModelDatabase.ModelsForPartType(CarPartModelDatabase.CarPartModelType.Brake);
    // ISSUE: reference to a compiler-generated field
    for (int index = 0; index < awakeCAnonStorey6C.brakesOptions.Count; ++index)
    {
      // ISSUE: reference to a compiler-generated field
      optionDataList4.Add(new Dropdown.OptionData(awakeCAnonStorey6C.brakesOptions[index].assetPath));
    }
    this.brakesDropdown.set_options(optionDataList4);
    // ISSUE: reference to a compiler-generated method
    this.brakesDropdown.onValueChanged.AddListener(new UnityAction<int>(awakeCAnonStorey6C.\u003C\u003Em__DB));
    List<Dropdown.OptionData> optionDataList5 = new List<Dropdown.OptionData>(this.mCarPartModelDatabase.NumOptionsForPartType(CarPartModelDatabase.CarPartModelType.Wheel));
    // ISSUE: reference to a compiler-generated field
    awakeCAnonStorey6C.wheelsOptions = this.mCarPartModelDatabase.ModelsForPartType(CarPartModelDatabase.CarPartModelType.Wheel);
    // ISSUE: reference to a compiler-generated field
    for (int index = 0; index < awakeCAnonStorey6C.wheelsOptions.Count; ++index)
    {
      // ISSUE: reference to a compiler-generated field
      optionDataList5.Add(new Dropdown.OptionData(awakeCAnonStorey6C.wheelsOptions[index].assetPath));
    }
    this.wheelsDropdown.set_options(optionDataList5);
    // ISSUE: reference to a compiler-generated method
    this.wheelsDropdown.onValueChanged.AddListener(new UnityAction<int>(awakeCAnonStorey6C.\u003C\u003Em__DC));
    this.rearWheelsDropdown.onValueChanged.AddListener((UnityAction<int>) (value => Debug.Log((object) "Rear wheels not supported yet", (Object) null)));
    List<Dropdown.OptionData> optionDataList6 = new List<Dropdown.OptionData>(this.mCarPartModelDatabase.NumOptionsForPartType(CarPartModelDatabase.CarPartModelType.FrontWing));
    // ISSUE: reference to a compiler-generated field
    awakeCAnonStorey6C.frontWingOptions = this.mCarPartModelDatabase.ModelsForPartType(CarPartModelDatabase.CarPartModelType.FrontWing);
    // ISSUE: reference to a compiler-generated field
    for (int index = 0; index < awakeCAnonStorey6C.frontWingOptions.Count; ++index)
    {
      // ISSUE: reference to a compiler-generated field
      optionDataList6.Add(new Dropdown.OptionData(awakeCAnonStorey6C.frontWingOptions[index].assetPath));
    }
    this.frontWingDropdown.set_options(optionDataList6);
    // ISSUE: reference to a compiler-generated method
    this.frontWingDropdown.onValueChanged.AddListener(new UnityAction<int>(awakeCAnonStorey6C.\u003C\u003Em__DE));
    List<Dropdown.OptionData> optionDataList7 = new List<Dropdown.OptionData>(this.mCarPartModelDatabase.NumOptionsForPartType(CarPartModelDatabase.CarPartModelType.RearWing));
    // ISSUE: reference to a compiler-generated field
    awakeCAnonStorey6C.rearWingOptions = this.mCarPartModelDatabase.ModelsForPartType(CarPartModelDatabase.CarPartModelType.RearWing);
    // ISSUE: reference to a compiler-generated field
    for (int index = 0; index < awakeCAnonStorey6C.rearWingOptions.Count; ++index)
    {
      // ISSUE: reference to a compiler-generated field
      optionDataList7.Add(new Dropdown.OptionData(awakeCAnonStorey6C.rearWingOptions[index].assetPath));
    }
    this.rearWingDropdown.set_options(optionDataList7);
    // ISSUE: reference to a compiler-generated method
    this.rearWingDropdown.onValueChanged.AddListener(new UnityAction<int>(awakeCAnonStorey6C.\u003C\u003Em__DF));
    for (int index = 0; index < 5; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      this.sliders[index].onValueChanged.AddListener(new UnityAction<float>(new CreateCarScreen.\u003CAwake\u003Ec__AnonStorey6D()
      {
        \u003C\u003Ef__this = this,
        sliderIndexCopy = index
      }.\u003C\u003Em__E0));
    }
    for (int index1 = 0; index1 < this.sponsorDropdowns.Length; ++index1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CreateCarScreen.\u003CAwake\u003Ec__AnonStorey6E awakeCAnonStorey6E = new CreateCarScreen.\u003CAwake\u003Ec__AnonStorey6E();
      // ISSUE: reference to a compiler-generated field
      awakeCAnonStorey6E.\u003C\u003Ef__this = this;
      List<Dropdown.OptionData> optionDataList8 = new List<Dropdown.OptionData>();
      for (int index2 = 0; index2 < this.mSponsorDatabase.sponsors.Count; ++index2)
      {
        CheapSponsorDatabase.CheapSponsorData sponsor = this.mSponsorDatabase.sponsors[index2];
        optionDataList8.Add(new Dropdown.OptionData(sponsor.name));
      }
      this.sponsorDropdowns[index1].set_options(optionDataList8);
      // ISSUE: reference to a compiler-generated field
      awakeCAnonStorey6E.sponsorSlotIndexCopy = index1;
      // ISSUE: reference to a compiler-generated method
      this.sponsorDropdowns[index1].onValueChanged.AddListener(new UnityAction<int>(awakeCAnonStorey6E.\u003C\u003Em__E1));
    }
    this.copyMaterialToLiveryDataButton.onClick.AddListener(new UnityAction(this.CopyMaterialToLiveryData));
    this.copyChassisLiveryToFrontWingButton.onClick.AddListener(new UnityAction(this.CopyChassisLiveryToFrontWing));
    this.copyChassisLiveryToRearWingButton.onClick.AddListener(new UnityAction(this.CopyChassisLiveryToRearWing));
    this.mCar = new FrontendCar();
    this.mCar.Start(-1, -1, this.mCarManager);
    this.mCar.gameObject.SetActive(true);
    this.mCar.Setup(defaultColour, LiveryData.defaultLivery, FrontendCarData.ModelData.defaultModelData, FrontendCarData.BlendShapeData.defaultBlendShapeData, FrontendCarData.SponsorData.defaultSponsorData);
  }

  private void BlendShapeSliderChanged(int blendShapeIndex, float value)
  {
    this.mCar.SetBlendShapeWeight(blendShapeIndex, value);
  }

  private void Update()
  {
    this.mCar.Update();
  }

  private void LiveryDropdownChanged(int index)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCar.SetLiveryData(this.mLiveryManager.liveries[index]);
  }

  private void ColourDropdownChanged(int index)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.UpdateColoursFromColourSet(this.mTeamColourManager.colours[index]);
    this.UpdatePickersAndButtonsToCurrentColour();
    this.SetCarColours();
  }

  private void UpdateColoursFromColourSet(TeamColor colour)
  {
    this.mColour = colour.livery;
  }

  private void UpdatePickersAndButtonsToCurrentColour()
  {
    this.primaryColourPickColourButton.image.color = this.mColour.primary;
    this.secondaryColourPickColourButton.image.color = this.mColour.secondary;
    this.tertiaryColourPickColourButton.image.color = this.mColour.tertiary;
    this.trimColourPickColourButton.image.color = this.mColour.trim;
    this.primaryColourPicker.currentColor = this.mColour.primary;
    this.secondaryColourPicker.currentColor = this.mColour.secondary;
    this.tertiaryColourPicker.currentColor = this.mColour.tertiary;
    this.trimColourPicker.currentColor = this.mColour.trim;
  }

  private void SetSponsor(int sponsorSlotIndex, int dropdownIndex)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    int sponsorLogoId = dropdownIndex + 1;
    this.mCar.SetSponsorTexture(sponsorSlotIndex, sponsorLogoId);
  }

  private void CopyMaterialToLiveryData()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void CopyChassisLiveryToFrontWing()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    LiveryData liveryData = this.mCar.data.liveryData;
    liveryData.frontWing = liveryData.chassis.Clone();
    this.mCar.SetLiveryData(liveryData);
  }

  private void CopyChassisLiveryToRearWing()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    LiveryData liveryData = this.mCar.data.liveryData;
    liveryData.rearWing = liveryData.chassis.Clone();
    this.mCar.SetLiveryData(liveryData);
  }

  private void RandomiseParts()
  {
    this.mCar.RandomiseParts(this.mCarPartModelDatabase);
  }

  private void RandomiseAll()
  {
    this.mCar.Randomise(this.mCarPartModelDatabase);
    this.mColour = this.mCar.data.colourData;
    this.UpdatePickersAndButtonsToCurrentColour();
  }

  private void SetCarColours()
  {
    this.mCar.SetColours(this.mColour);
  }
}
