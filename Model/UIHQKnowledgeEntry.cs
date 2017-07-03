// Decompiled with JetBrains decompiler
// Type: UIHQKnowledgeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHQKnowledgeEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public bool useRollover;
  public Button button;
  public TextMeshProUGUI headingLabel;
  public TextMeshProUGUI levelLabel;
  public UIKnowledgeBar knowledgeBar;
  public GameObject[] icons;
  public Transform iconsParent;
  public HeadquartersScreen screen;
  private HQsBuilding_v1 mBuilding;
  private PartTypeSlotSettings mSetting;
  private CarPart.PartType mPartType;
  private int mLevel;

  private void Awake()
  {
    if (!((UnityEngine.Object) this.button != (UnityEngine.Object) null))
      return;
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(CarPart.PartType inPartType)
  {
    this.mSetting = Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID][inPartType];
    this.mPartType = inPartType;
    this.mLevel = this.mSetting.GetMaxLevel(Game.instance.player.team);
    this.SetKnowledge();
  }

  public void SetupKnowledge(PartTypeSlotSettings inSetting, bool inNextLevel, HQsBuilding_v1 inBuilding = null)
  {
    this.mSetting = inSetting;
    this.mPartType = this.mSetting.partType;
    this.mLevel = !inNextLevel ? this.mSetting.GetMaxLevel(Game.instance.player.team) : this.mSetting.GetRequiredLevel(inBuilding.info.type, inBuilding.nextLevel);
    this.SetKnowledge();
  }

  public void SetupEffect(HQsBuilding_v1 inBuilding, int inEffectIndex, bool inNextLevel)
  {
    this.mBuilding = inBuilding;
    this.mLevel = !inNextLevel ? this.mBuilding.currentLevel : this.mBuilding.nextLevel;
    this.SetEffect(inEffectIndex, inNextLevel);
  }

  private void SetKnowledge()
  {
    this.mBuilding = Game.instance.player.team.headquarters.GetBuilding(PartTypeSlotSettingsManager.GetBuildingRelevantToPart(this.mPartType));
    if ((UnityEngine.Object) this.headingLabel != (UnityEngine.Object) null)
      this.headingLabel.text = Localisation.LocaliseEnum((Enum) this.mPartType);
    this.levelLabel.text = this.mLevel.ToString() + "/5";
    if ((UnityEngine.Object) this.knowledgeBar != (UnityEngine.Object) null)
      this.knowledgeBar.SetupKnowledge(this.mLevel);
    for (int index = 0; index < this.icons.Length; ++index)
      GameUtility.SetActive(this.icons[index], (CarPart.PartType) index == this.mPartType);
    if (!((UnityEngine.Object) this.iconsParent != (UnityEngine.Object) null))
      return;
    for (int index = 0; index < 11; ++index)
    {
      CarPart.PartType partType = (CarPart.PartType) index;
      GameUtility.SetActive(this.iconsParent.GetChild(index).gameObject, this.mPartType == partType);
    }
  }

  private void SetEffect(int inEffectIndex, bool inNextLevel)
  {
    if ((UnityEngine.Object) this.headingLabel != (UnityEngine.Object) null)
      this.headingLabel.text = Localisation.LocaliseID(!inNextLevel ? this.mBuilding.info.effects[inEffectIndex] : this.mBuilding.info.effectsNextLevel[inEffectIndex], (GameObject) null);
    this.levelLabel.text = (this.mLevel + 1).ToString() + "/" + this.mBuilding.maxLevelUI.ToString();
    if ((UnityEngine.Object) this.knowledgeBar != (UnityEngine.Object) null)
      this.knowledgeBar.SetupEffect(this.mBuilding, this.mLevel);
    for (int index = 0; index < this.icons.Length; ++index)
      GameUtility.SetActive(this.icons[index], false);
  }

  private void OnButton()
  {
    if (!((UnityEngine.Object) this.screen != (UnityEngine.Object) null) || this.mBuilding == null)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.SelectBuilding(this.mBuilding);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (!this.useRollover || !((UnityEngine.Object) this.screen != (UnityEngine.Object) null) || this.mBuilding == null)
      return;
    UIHQRollover.Setup(this.screen.screenMode, this.mBuilding, UIHQRollover.Mode.Knowledge, true);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (!this.useRollover)
      return;
    UIHQRollover.Close();
  }
}
