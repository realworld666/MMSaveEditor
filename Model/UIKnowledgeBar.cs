// Decompiled with JetBrains decompiler
// Type: UIKnowledgeBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKnowledgeBar : MonoBehaviour
{
  private Color mColor = new Color();
  public TextMeshProUGUI label;
  public Image[] backings;
  public Image[] levelsBackings;
  public Image[] levels;
  public CanvasGroup[] canvasGroups;
  public GameObject labelContainer;
  public GameObject[] nonSpecPartContainer;
  public GameObject[] specPartContainer;
  private int mLevel;
  private int mMaxLevel;
  private HQsBuilding_v1 mBuilding;

  public void SetupKnowledge(int inLevel)
  {
    this.mMaxLevel = 5;
    this.mLevel = inLevel;
    this.mColor = UIConstants.GetPartLevelColor(this.mLevel);
    this.SetKnowledge();
    if (this.nonSpecPartContainer == null)
      return;
    for (int index = 0; index < this.nonSpecPartContainer.Length; ++index)
      this.nonSpecPartContainer[index].SetActive(this.mLevel != -1);
    for (int index = 0; index < this.specPartContainer.Length; ++index)
      this.specPartContainer[index].SetActive(this.mLevel == -1);
  }

  public void SetupEffect(HQsBuilding_v1 inBuilding, int inLevel)
  {
    this.mBuilding = inBuilding;
    this.mLevel = inLevel + 1;
    this.mMaxLevel = this.mBuilding.info.maxLevel + 1;
    this.mColor = UIConstants.colorHQEffect;
    this.SetEffect();
  }

  private void SetKnowledge()
  {
    GameUtility.SetActive(this.labelContainer, true);
    this.label.text = CarPart.GetLevelUIString(this.mLevel);
    this.mMaxLevel = 5;
    this.SetLevels();
  }

  private void SetEffect()
  {
    GameUtility.SetActive(this.labelContainer, false);
    this.label.text = string.Empty;
    this.SetLevels();
  }

  private void SetLevels()
  {
    for (int index = 0; index < this.backings.Length; ++index)
      this.backings[index].color = this.mColor;
    for (int index = 0; index < this.levelsBackings.Length; ++index)
      GameUtility.SetActive(this.levelsBackings[index].gameObject, index < this.mMaxLevel);
    for (int index = 0; index < this.levels.Length; ++index)
    {
      GameUtility.SetActive(this.levels[index].gameObject, index < this.mMaxLevel);
      if (index < this.mMaxLevel)
      {
        this.canvasGroups[index].alpha = index > this.mLevel - 1 ? 0.0f : 1f;
        this.levels[index].color = this.mColor;
      }
    }
  }
}
