// Decompiled with JetBrains decompiler
// Type: UIPartDevStatImprovementPartEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDevStatImprovementPartEntry : MonoBehaviour
{
  private CarPartStats.CarPartStat mStat = CarPartStats.CarPartStat.Count;
  public Animator animator;
  public TextMeshProUGUI dataText;
  public TextMeshProUGUI levelLabel;
  public Image levelBacking;
  public Transform partIconParent;
  public Button removeButton;
  public GameObject conditionBarContainer;
  public UIPartConditionBar conditionBar;
  public GameObject performanceContainer;
  public Slider performanceSlider;
  public Slider maxPerformanceSlider;
  private UIPartDevStatImprovementPartEntry.State mState;
  private CarPart mPart;

  public UIPartDevStatImprovementPartEntry.State state
  {
    get
    {
      return this.mState;
    }
  }

  public CarPart part
  {
    get
    {
      return this.mPart;
    }
  }

  private void Start()
  {
    this.removeButton.onClick.AddListener(new UnityAction(this.RemovePart));
    GameUtility.SetActive(this.removeButton.gameObject, false);
  }

  public void ShowTooltip()
  {
    string inHeader = Localisation.LocaliseID("PSG_10010504", (GameObject) null);
    string inDescription = Localisation.LocaliseID("PSG_10010533", (GameObject) null);
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(inHeader, inDescription);
    scSoundManager.BlockSoundEvents = false;
  }

  public void HideToolTip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }

  private void OpenPopup()
  {
    if (this.mPart == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().ShowTooltip(this.mPart, this.GetComponent<RectTransform>());
  }

  private void ClosePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().HideTooltip();
  }

  private void OnMouseHover()
  {
    if (this.state != UIPartDevStatImprovementPartEntry.State.Used && this.state != UIPartDevStatImprovementPartEntry.State.UsedAndMaxed)
      return;
    GameUtility.SetActive(this.removeButton.gameObject, true);
  }

  private void OnMouseExit()
  {
    GameUtility.SetActive(this.removeButton.gameObject, false);
  }

  public void Setup(CarPart inPart, UIPartDevStatImprovementPartEntry.State inState, CarPartStats.CarPartStat inStat)
  {
    if (inState == UIPartDevStatImprovementPartEntry.State.Used)
    {
      if (!PartImprovement.AnyPartNeedsWork(inStat, inPart))
        inState = UIPartDevStatImprovementPartEntry.State.UsedAndMaxed;
    }
    this.mState = inState;
    this.mStat = inStat;
    this.mPart = inPart;
    if (inPart != null)
      this.SetupPartData(this.mPart);
    this.animator.SetTrigger(this.mState.ToString());
    GameUtility.SetActive(this.removeButton.gameObject, false);
    this.ClosePopup();
  }

  private void RemovePart()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Game.instance.player.team.carManager.partImprovement.RemovePartImprove(this.mStat, this.mPart, true);
  }

  public void UpdateStats()
  {
    if (this.mPart == null)
      return;
    this.SetupPartData(this.mPart);
  }

  private void SetupPartData(CarPart inPart)
  {
    switch (this.mStat)
    {
      case CarPartStats.CarPartStat.Reliability:
        GameUtility.SetActive(this.conditionBarContainer, true);
        GameUtility.SetActive(this.performanceContainer, false);
        this.dataText.text = Mathf.RoundToInt(this.mPart.reliability * 100f).ToString() + " %";
        break;
      case CarPartStats.CarPartStat.Condition:
        GameUtility.SetActive(this.conditionBarContainer, true);
        GameUtility.SetActive(this.performanceContainer, false);
        this.dataText.text = Mathf.FloorToInt(inPart.stats.partCondition.condition * 100f).ToString() + "/" + (object) Mathf.RoundToInt(inPart.stats.reliability * 100f);
        break;
      case CarPartStats.CarPartStat.Performance:
        GameUtility.SetActive(this.conditionBarContainer, false);
        GameUtility.SetActive(this.performanceContainer, true);
        this.dataText.text = Mathf.FloorToInt(inPart.stats.statWithPerformance).ToString() + "/" + (object) Mathf.RoundToInt(inPart.stats.stat + Mathf.Max(0.0f, inPart.stats.maxPerformance));
        break;
    }
    for (int index = 0; index < this.partIconParent.childCount; ++index)
    {
      if ((CarPart.PartType) index == inPart.GetPartType())
        GameUtility.SetActive(this.partIconParent.GetChild(index).gameObject, true);
      else
        GameUtility.SetActive(this.partIconParent.GetChild(index).gameObject, false);
    }
    Color partLevelColor = UIConstants.GetPartLevelColor(inPart.stats.level);
    this.levelBacking.color = partLevelColor;
    this.levelLabel.text = inPart.GetLevelUIString();
    this.conditionBar.Setup(inPart);
    this.performanceSlider.normalizedValue = CarPartStats.GetNormalizedStatValue(inPart.stats.statWithPerformance, inPart.GetPartType());
    this.maxPerformanceSlider.normalizedValue = CarPartStats.GetNormalizedStatValue(inPart.stats.stat + inPart.stats.maxPerformance, inPart.GetPartType());
    this.performanceSlider.fillRect.GetComponent<Image>().color = partLevelColor;
    partLevelColor.a = 0.8f;
    this.maxPerformanceSlider.fillRect.GetComponent<Image>().color = partLevelColor;
  }

  public enum State
  {
    Free,
    Locked,
    Used,
    UsedAndMaxed,
  }
}
