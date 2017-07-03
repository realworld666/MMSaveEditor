// Decompiled with JetBrains decompiler
// Type: UITravelTyreSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITravelTyreSelection : UITravelStepOption
{
  private int[] mRemainingTyreCount = new int[Team.mainDriverCount];
  private int[] mFirstTyreCount = new int[Team.mainDriverCount];
  private int[] mSecondTyreCount = new int[Team.mainDriverCount];
  private int[] mThirdTyreCount = new int[Team.mainDriverCount];
  private int mTotalTyreCount = 15;
  private bool[] mHasBeenSetup = new bool[Team.mainDriverCount];
  public UICharacterPortrait[] driverPortrait;
  public Flag[] flag;
  public TextMeshProUGUI[] driverNameLabel;
  public TextMeshProUGUI[] championshipPositionLabel;
  public TextMeshProUGUI[] firstOptionTyreNameLabel;
  public TextMeshProUGUI[] firstOptionCountLabel;
  public Image[] firstTyreIcon;
  public Button[] addFirstOptionButton;
  public Button[] removeFirstOptionButton;
  public TextMeshProUGUI[] secondOptionTyreNameLabel;
  public TextMeshProUGUI[] secondOptionCountLabel;
  public Image[] secondTyreIcon;
  public Button[] addSecondOptionButton;
  public Button[] removeSecondOptionButton;
  public TextMeshProUGUI[] thirdOptionTyreNameLabel;
  public TextMeshProUGUI[] thirdOptionCountLabel;
  public Image[] thirdTyreIcon;
  public Button[] addThirdOptionButton;
  public Button[] removeThirdOptionButton;
  public Button[] clearSelectionButton;
  public Button[] autoPickButton;
  public GameObject[] tyreSelectionComplete;
  public GameObject[] tyreSelectionIncomplete;
  public TextMeshProUGUI[] tyreIncompleteLabel;
  public Transform[] pipGrid;
  public Animator[] highlightAnimators;
  private Image[,] mPips;
  private TyreSet mFirstOptionTyreSet;
  private TyreSet mSecondOptionTyreSet;
  private TyreSet mThirdOptionTyreSet;

  public override void OnStart()
  {
    base.OnStart();
    for (int index = 0; index < Team.mainDriverCount; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UITravelTyreSelection.\u003COnStart\u003Ec__AnonStoreyA1 startCAnonStoreyA1 = new UITravelTyreSelection.\u003COnStart\u003Ec__AnonStoreyA1();
      // ISSUE: reference to a compiler-generated field
      startCAnonStoreyA1.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      startCAnonStoreyA1.driverID = index;
      // ISSUE: reference to a compiler-generated method
      this.addFirstOptionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1E7));
      // ISSUE: reference to a compiler-generated method
      this.removeFirstOptionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1E8));
      // ISSUE: reference to a compiler-generated method
      this.addSecondOptionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1E9));
      // ISSUE: reference to a compiler-generated method
      this.removeSecondOptionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1EA));
      // ISSUE: reference to a compiler-generated method
      this.addThirdOptionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1EB));
      // ISSUE: reference to a compiler-generated method
      this.removeThirdOptionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1EC));
      // ISSUE: reference to a compiler-generated method
      this.clearSelectionButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1ED));
      // ISSUE: reference to a compiler-generated method
      this.autoPickButton[index].onClick.AddListener(new UnityAction(startCAnonStoreyA1.\u003C\u003Em__1EE));
    }
  }

  public override void Setup()
  {
    base.Setup();
    Team team = Game.instance.player.team;
    Circuit circuit = team.championship.GetCurrentEventDetails().circuit;
    this.mFirstOptionTyreSet = TyreSet.CreateTyreSet(circuit.firstTyreOption);
    this.mSecondOptionTyreSet = TyreSet.CreateTyreSet(circuit.secondTyreOption);
    this.mThirdOptionTyreSet = TyreSet.CreateTyreSet(circuit.thirdTyreOption);
    this.mTotalTyreCount = team.championship.rules.maxSlickTyresPerEvent;
    this.mPips = new Image[Team.mainDriverCount, this.pipGrid[0].childCount];
    for (int index1 = 0; index1 < Team.mainDriverCount; ++index1)
    {
      Driver driver = team.GetDriver(index1);
      for (int index2 = 0; index2 < this.pipGrid[index1].childCount; ++index2)
        this.mPips[index1, index2] = this.pipGrid[index1].GetChild(index2).GetComponent<Image>();
      this.driverPortrait[index1].SetPortrait((Person) driver);
      this.flag[index1].SetNationality(driver.nationality);
      this.driverNameLabel[index1].text = driver.shortName;
      ChampionshipEntry_v1 championshipEntry = driver.GetChampionshipEntry();
      this.championshipPositionLabel[index1].text = championshipEntry.championship.GetAcronym(false) + ": " + GameUtility.FormatForPosition(championshipEntry.GetCurrentChampionshipPosition(), (string) null);
      this.SetTyreIconAndName(this.firstTyreIcon[index1], this.firstOptionTyreNameLabel[index1], circuit.firstTyreOption);
      this.SetTyreIconAndName(this.secondTyreIcon[index1], this.secondOptionTyreNameLabel[index1], circuit.secondTyreOption);
      this.SetTyreIconAndName(this.thirdTyreIcon[index1], this.thirdOptionTyreNameLabel[index1], circuit.thirdTyreOption);
      if (!this.mHasBeenSetup[index1])
      {
        this.ClearSelection(index1);
        this.mHasBeenSetup[index1] = true;
      }
    }
    ChampionshipRules rules = Game.instance.player.team.championship.rules;
    for (int index = 0; index < Team.mainDriverCount; ++index)
      this.addThirdOptionButton[index].transform.parent.gameObject.SetActive(rules.compoundsAvailable > 2);
  }

  public override void RefreshText()
  {
    Circuit circuit = Game.instance.player.team.championship.GetCurrentEventDetails().circuit;
    Team team = Game.instance.player.team;
    for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
    {
      Driver driver = team.GetDriver(inIndex);
      this.driverPortrait[inIndex].SetPortrait((Person) driver);
      this.flag[inIndex].SetNationality(driver.nationality);
      this.driverNameLabel[inIndex].text = driver.shortName;
      ChampionshipEntry_v1 championshipEntry = driver.GetChampionshipEntry();
      this.championshipPositionLabel[inIndex].text = championshipEntry.championship.GetAcronym(false) + ": " + GameUtility.FormatForPosition(championshipEntry.GetCurrentChampionshipPosition(), (string) null);
    }
    for (int index = 0; index < Team.mainDriverCount; ++index)
    {
      this.firstOptionTyreNameLabel[index].text = Localisation.LocaliseEnum((Enum) circuit.firstTyreOption);
      this.secondOptionTyreNameLabel[index].text = Localisation.LocaliseEnum((Enum) circuit.secondTyreOption);
      this.thirdOptionTyreNameLabel[index].text = Localisation.LocaliseEnum((Enum) circuit.thirdTyreOption);
    }
  }

  private void SetTyreIconAndName(Image inImage, TextMeshProUGUI inLabel, TyreSet.Compound inCompound)
  {
    string str = "TyresNew-Small-Tyre";
    inLabel.text = Localisation.LocaliseEnum((Enum) inCompound);
    switch (inCompound)
    {
      case TyreSet.Compound.SuperSoft:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Soft");
        break;
      case TyreSet.Compound.Soft:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "SuperSoft");
        break;
      case TyreSet.Compound.Medium:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Medium");
        break;
      case TyreSet.Compound.Hard:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Hard");
        break;
      case TyreSet.Compound.Intermediate:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Inter");
        break;
      case TyreSet.Compound.Wet:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Wet");
        break;
      case TyreSet.Compound.UltraSoft:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-UltraSoft");
        break;
    }
  }

  private void AddFirstOptionTyre(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRemainingTyreCount[inDriverID] > 0)
    {
      ++this.mFirstTyreCount[inDriverID];
      --this.mRemainingTyreCount[inDriverID];
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void RemoveFirstOptionTyre(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mFirstTyreCount[inDriverID] > 1)
    {
      --this.mFirstTyreCount[inDriverID];
      ++this.mRemainingTyreCount[inDriverID];
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void AddSecondOptionTyre(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRemainingTyreCount[inDriverID] > 0)
    {
      ++this.mSecondTyreCount[inDriverID];
      --this.mRemainingTyreCount[inDriverID];
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void RemoveSecondOptionTyre(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mSecondTyreCount[inDriverID] > 1)
    {
      --this.mSecondTyreCount[inDriverID];
      ++this.mRemainingTyreCount[inDriverID];
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void AddThirdOptionTyre(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRemainingTyreCount[inDriverID] > 0)
    {
      ++this.mThirdTyreCount[inDriverID];
      --this.mRemainingTyreCount[inDriverID];
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void RemoveThirdOptionTyre(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mThirdTyreCount[inDriverID] > 1)
    {
      --this.mThirdTyreCount[inDriverID];
      ++this.mRemainingTyreCount[inDriverID];
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void SetDefaultAllocation(int inDriverID)
  {
    ChampionshipRules rules = Game.instance.player.team.championship.rules;
    this.mRemainingTyreCount[inDriverID] = 0;
    if (rules.compoundsAvailable > 2)
    {
      int num = this.mTotalTyreCount / 3;
      this.mFirstTyreCount[inDriverID] = num;
      this.mSecondTyreCount[inDriverID] = num;
      this.mThirdTyreCount[inDriverID] = this.mTotalTyreCount - (this.mFirstTyreCount[inDriverID] + this.mSecondTyreCount[inDriverID]);
    }
    else
    {
      int num = this.mTotalTyreCount / 2;
      this.mFirstTyreCount[inDriverID] = num;
      this.mSecondTyreCount[inDriverID] = this.mTotalTyreCount - this.mFirstTyreCount[inDriverID];
      this.mThirdTyreCount[inDriverID] = 0;
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void AutoPick(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRemainingTyreCount[inDriverID] == 0)
      return;
    ChampionshipRules rules = Game.instance.player.team.championship.rules;
    this.mRemainingTyreCount[inDriverID] = 0;
    if (rules.compoundsAvailable > 2)
    {
      int inMax = (this.mTotalTyreCount - 1) / 2;
      int inMin = inMax / 2;
      this.mFirstTyreCount[inDriverID] = RandomUtility.GetRandomInc(inMin, inMax);
      this.mSecondTyreCount[inDriverID] = RandomUtility.GetRandomInc(inMin, inMax);
      this.mThirdTyreCount[inDriverID] = this.mTotalTyreCount - (this.mFirstTyreCount[inDriverID] + this.mSecondTyreCount[inDriverID]);
    }
    else
    {
      int inMax = (this.mTotalTyreCount - 1) / 2;
      int inMin = inMax / 2;
      this.mFirstTyreCount[inDriverID] = RandomUtility.GetRandomInc(inMin, inMax);
      this.mSecondTyreCount[inDriverID] = this.mTotalTyreCount - this.mFirstTyreCount[inDriverID];
      this.mThirdTyreCount[inDriverID] = 0;
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void ClearSelection(int inDriverID)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (Game.instance.player.team.championship.rules.compoundsAvailable > 2)
    {
      this.mRemainingTyreCount[inDriverID] = this.mTotalTyreCount - 3;
      this.mFirstTyreCount[inDriverID] = 1;
      this.mSecondTyreCount[inDriverID] = 1;
      this.mThirdTyreCount[inDriverID] = 1;
    }
    else
    {
      this.mRemainingTyreCount[inDriverID] = this.mTotalTyreCount - 2;
      this.mFirstTyreCount[inDriverID] = 1;
      this.mSecondTyreCount[inDriverID] = 1;
      this.mThirdTyreCount[inDriverID] = 0;
    }
    this.UpdateSelectionData(inDriverID);
  }

  private void UpdateSelectionData(int inDriverID)
  {
    bool inIsActive = this.mRemainingTyreCount[inDriverID] == 0;
    GameUtility.SetActive(this.tyreSelectionComplete[inDriverID], inIsActive);
    GameUtility.SetActive(this.tyreSelectionIncomplete[inDriverID], !inIsActive);
    if (this.mRemainingTyreCount[inDriverID] > 1)
    {
      StringVariableParser.intValue1 = this.mRemainingTyreCount[inDriverID];
      this.tyreIncompleteLabel[inDriverID].text = Localisation.LocaliseID("PSG_10010615", (GameObject) null);
    }
    else if (this.mRemainingTyreCount[inDriverID] == 1)
      this.tyreIncompleteLabel[inDriverID].text = Localisation.LocaliseID("PSG_10010663", (GameObject) null);
    this.firstOptionCountLabel[inDriverID].text = this.mFirstTyreCount[inDriverID].ToString();
    this.secondOptionCountLabel[inDriverID].text = this.mSecondTyreCount[inDriverID].ToString();
    this.thirdOptionCountLabel[inDriverID].text = this.mThirdTyreCount[inDriverID].ToString();
    for (int index = 0; index < 15; ++index)
    {
      Color color = this.mPips[inDriverID, index].color;
      color = Color.white;
      if (index >= this.mTotalTyreCount)
        color.a = 0.0f;
      else if (index < this.mTotalTyreCount - this.mRemainingTyreCount[inDriverID])
      {
        color = index >= this.mFirstTyreCount[inDriverID] ? (index >= this.mFirstTyreCount[inDriverID] + this.mSecondTyreCount[inDriverID] ? this.mThirdOptionTyreSet.GetColor() : this.mSecondOptionTyreSet.GetColor()) : this.mFirstOptionTyreSet.GetColor();
        color.a = 0.75f;
      }
      else
        color.a = 0.1f;
      this.mPips[inDriverID, index].color = color;
    }
    this.addFirstOptionButton[inDriverID].interactable = this.mRemainingTyreCount[inDriverID] > 0;
    this.removeFirstOptionButton[inDriverID].interactable = this.mFirstTyreCount[inDriverID] > 1;
    this.addSecondOptionButton[inDriverID].interactable = this.mRemainingTyreCount[inDriverID] > 0;
    this.removeSecondOptionButton[inDriverID].interactable = this.mSecondTyreCount[inDriverID] > 1;
    this.addThirdOptionButton[inDriverID].interactable = this.mRemainingTyreCount[inDriverID] > 0;
    this.removeThirdOptionButton[inDriverID].interactable = this.mThirdTyreCount[inDriverID] > 1;
    this.clearSelectionButton[inDriverID].interactable = this.mFirstTyreCount[inDriverID] > 1 || this.mSecondTyreCount[inDriverID] > 1 || this.mThirdTyreCount[inDriverID] > 1;
    this.autoPickButton[inDriverID].interactable = true;
    Game.instance.persistentEventData.SetSelectedTyreCount(inDriverID, this.mFirstTyreCount[inDriverID], this.mSecondTyreCount[inDriverID], this.mThirdTyreCount[inDriverID]);
    this.autoPickButton[inDriverID].interactable = !inIsActive;
  }

  public override bool IsReady()
  {
    if (this.mRemainingTyreCount[0] == 0)
      return this.mRemainingTyreCount[1] == 0;
    return false;
  }
}
