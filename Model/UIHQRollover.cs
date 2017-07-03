// Decompiled with JetBrains decompiler
// Type: UIHQRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHQRollover : UIDialogBox
{
  private UIHQRollover.Mode mMode = UIHQRollover.Mode.Building;
  public RectTransform rectTransform;
  public UIHQGroupIcon groupIcon;
  public TextMeshProUGUI currentTitle;
  public TextMeshProUGUI requireTitle;
  public TextMeshProUGUI description;
  public UIGridList gridCurrent;
  public UIGridList gridUpgrade;
  public UIGridList gridKnowledge;
  public UIGridList gridLocked;
  public GameObject knowledgePrefab;
  public GameObject unlockPrefab;
  public GameObject requirementPrefab;
  public GameObject knowledgeRequires;
  public GameObject upgradeHeaderBuild;
  public GameObject upgradeHeaderUpgrade;
  public GameObject infoParent;
  public GameObject currentParent;
  public GameObject upgradeParent;
  public GameObject knowledgeParent;
  public GameObject lockedParent;
  private HQsBuilding_v1 mBuilding;

  public static void Setup(UIScreen.ScreenMode inScreenMode, HQsBuilding_v1 inBuilding, UIHQRollover.Mode inMode = UIHQRollover.Mode.AutoSelect, bool inShowInfo = true)
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIHQRollover>().SetupRollover(inScreenMode, inBuilding, inMode, inShowInfo);
  }

  public static void Close()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIHQRollover>().CloseRollover();
  }

  public void SetupRollover(UIScreen.ScreenMode inScreenMode, HQsBuilding_v1 inBuilding, UIHQRollover.Mode inMode = UIHQRollover.Mode.AutoSelect, bool inShowInfo = true)
  {
    this.mBuilding = inBuilding;
    this.mMode = inMode;
    if (this.mMode == UIHQRollover.Mode.AutoSelect)
      this.mMode = !this.mBuilding.isLocked ? UIHQRollover.Mode.Building : UIHQRollover.Mode.Locked;
    GameUtility.SetActive(this.infoParent, inShowInfo);
    GameUtility.SetActive(this.currentParent, inMode != UIHQRollover.Mode.Knowledge && inScreenMode == UIScreen.ScreenMode.Mode2D);
    GameUtility.SetActive(this.upgradeParent, this.mMode == UIHQRollover.Mode.Building);
    GameUtility.SetActive(this.knowledgeParent, this.mMode == UIHQRollover.Mode.Knowledge);
    GameUtility.SetActive(this.lockedParent, this.mMode == UIHQRollover.Mode.Locked);
    this.SetInfo();
    this.SetCurrent();
    this.SetUpgrade();
    this.SetUpgradeKnowledge();
    this.SetLocks();
    this.UpdatePosition();
    this.Show();
  }

  public void Show()
  {
    GameUtility.SetActive(this.gameObject, true);
  }

  public void CloseRollover()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    this.UpdatePosition();
  }

  private void UpdatePosition()
  {
    if (this.mMode != UIHQRollover.Mode.Knowledge)
      GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, Vector3.zero, true, (RectTransform) null);
    else
      GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  private void SetInfo()
  {
    if (!this.infoParent.activeSelf)
      return;
    this.currentTitle.text = this.mBuilding.buildingName;
    this.requireTitle.text = this.mBuilding.buildingName;
    this.description.text = this.mBuilding.info.GetDescription();
    this.groupIcon.SetIcon(this.mBuilding);
  }

  private void SetCurrent()
  {
    if (!this.currentParent.activeSelf)
      return;
    int num = 0;
    this.gridCurrent.DestroyListItems();
    List<PartTypeSlotSettings> settings = this.GetSettings(true);
    this.gridCurrent.itemPrefab = this.knowledgePrefab;
    GameUtility.SetActive(this.gridCurrent.itemPrefab, true);
    int count = settings.Count;
    for (int index = 0; index < count; ++index)
    {
      this.gridCurrent.CreateListItem<UIHQKnowledgeEntry>().SetupKnowledge(settings[index], false, this.mBuilding);
      ++num;
    }
    if (this.mBuilding.info.effects != null && this.mBuilding.isBuilt)
    {
      for (int inEffectIndex = 0; inEffectIndex < this.mBuilding.info.effects.Length; ++inEffectIndex)
      {
        this.gridCurrent.CreateListItem<UIHQKnowledgeEntry>().SetupEffect(this.mBuilding, inEffectIndex, false);
        ++num;
      }
    }
    GameUtility.SetActive(this.gridCurrent.itemPrefab, false);
    GameUtility.SetActive(this.currentParent, num > 0);
  }

  private void SetUpgrade()
  {
    if (!this.upgradeParent.activeSelf)
      return;
    int num = 0;
    this.gridUpgrade.DestroyListItems();
    if (!this.mBuilding.isMaxLevel)
    {
      List<PartTypeSlotSettings> settings = this.GetSettings(false);
      this.gridUpgrade.itemPrefab = this.knowledgePrefab;
      GameUtility.SetActive(this.gridUpgrade.itemPrefab, true);
      int count = settings.Count;
      for (int index = 0; index < count; ++index)
      {
        this.gridUpgrade.CreateListItem<UIHQKnowledgeEntry>().SetupKnowledge(settings[index], true, this.mBuilding);
        ++num;
      }
      if (this.mBuilding.info.effects != null && !this.mBuilding.isMaxLevel)
      {
        for (int inEffectIndex = 0; inEffectIndex < this.mBuilding.info.effects.Length; ++inEffectIndex)
        {
          this.gridUpgrade.CreateListItem<UIHQKnowledgeEntry>().SetupEffect(this.mBuilding, inEffectIndex, true);
          ++num;
        }
      }
      GameUtility.SetActive(this.gridUpgrade.itemPrefab, false);
      this.gridUpgrade.itemPrefab = this.unlockPrefab;
      GameUtility.SetActive(this.gridUpgrade.itemPrefab, true);
      HQsBuilding_v1[] buildingsCanUnlock = this.mBuilding.team.headquarters.GetBuildingsCanUnlock(this.mBuilding.info.type, this.mBuilding.nextLevel);
      int length = buildingsCanUnlock.Length;
      for (int index = 0; index < length; ++index)
      {
        this.gridUpgrade.CreateListItem<UIHQUnlockEntry>().Setup(buildingsCanUnlock[index], this.mBuilding.info.type, this.mBuilding.nextLevel);
        ++num;
      }
      GameUtility.SetActive(this.gridUpgrade.itemPrefab, false);
    }
    GameUtility.SetActive(this.upgradeHeaderBuild, !this.mBuilding.isBuilt);
    GameUtility.SetActive(this.upgradeHeaderUpgrade, this.mBuilding.isBuilt);
    GameUtility.SetActive(this.upgradeParent, num > 0);
  }

  private void SetUpgradeKnowledge()
  {
    if (!this.knowledgeParent.activeSelf)
      return;
    int num1 = 0;
    int num2 = 0;
    this.gridKnowledge.DestroyListItems();
    List<PartTypeSlotSettings> settings = this.GetSettings(false);
    int count = settings.Count;
    if (!this.mBuilding.isMaxLevel && count > 0)
    {
      this.gridKnowledge.itemPrefab = this.knowledgePrefab;
      GameUtility.SetActive(this.gridKnowledge.itemPrefab, true);
      for (int index = 0; index < count; ++index)
      {
        this.gridKnowledge.CreateListItem<UIHQKnowledgeEntry>().SetupKnowledge(settings[index], true, this.mBuilding);
        ++num1;
      }
      GameUtility.SetActive(this.gridKnowledge.itemPrefab, false);
    }
    if (!this.mBuilding.isMaxLevel)
    {
      this.knowledgeRequires.transform.SetAsLastSibling();
      this.gridKnowledge.itemPrefab = this.requirementPrefab;
      GameUtility.SetActive(this.gridKnowledge.itemPrefab, true);
      for (int index1 = 0; index1 < count; ++index1)
      {
        UnlockRequirementHQBuildingLevel[] buildingRequirements = settings[index1].GetBuildingRequirements();
        int maxLevel = settings[index1].GetMaxLevel(Game.instance.player.team);
        for (int index2 = 0; index2 < buildingRequirements.Length; ++index2)
        {
          if (buildingRequirements[index2].setLevel == maxLevel + 1)
          {
            this.gridKnowledge.CreateListItem<UIHQUnlockTickEntry>().Setup(buildingRequirements[index2]);
            ++num1;
            ++num2;
          }
        }
      }
      GameUtility.SetActive(this.gridKnowledge.itemPrefab, false);
    }
    GameUtility.SetActive(this.knowledgeRequires, num2 > 0);
    GameUtility.SetActive(this.knowledgeParent, num1 > 0);
  }

  private void SetLocks()
  {
    if (!this.lockedParent.activeSelf)
      return;
    this.gridLocked.DestroyListItems();
    List<HQsDependency> dependencies = this.mBuilding.info.dependencies;
    GameUtility.SetActive(this.gridLocked.itemPrefab, true);
    int count = dependencies.Count;
    for (int index = 0; index < count; ++index)
      this.gridLocked.CreateListItem<UIHQUnlockTickEntry>().Setup(dependencies[index], dependencies[index].isComplete(this.mBuilding.team));
    GameUtility.SetActive(this.gridLocked.itemPrefab, false);
  }

  private List<PartTypeSlotSettings> GetSettings(bool inCurrent)
  {
    List<PartTypeSlotSettings> inSettings = new List<PartTypeSlotSettings>();
    if (inCurrent)
      this.GetSettings(PartTypeSlotSettingsManager.GetPartsRelevantToBuilding(this.mBuilding.info.type), ref inSettings);
    else if (this.mBuilding.info.sets[Game.instance.player.team.championship.championshipID].ContainsKey(this.mBuilding.nextLevel))
      inSettings = this.mBuilding.info.sets[Game.instance.player.team.championship.championshipID][this.mBuilding.nextLevel];
    return inSettings;
  }

  private void GetSettings(CarPart.PartType[] inTypes, ref List<PartTypeSlotSettings> inSettings)
  {
    if (inTypes == null)
      return;
    for (int index = 0; index < inTypes.Length; ++index)
    {
      CarPart.PartType inType = inTypes[index];
      Dictionary<CarPart.PartType, PartTypeSlotSettings> championshipPartSetting = Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID];
      if (championshipPartSetting.ContainsKey(inType))
        inSettings.Add(championshipPartSetting[inType]);
    }
  }

  public enum Mode
  {
    AutoSelect,
    Building,
    Knowledge,
    Locked,
  }
}
