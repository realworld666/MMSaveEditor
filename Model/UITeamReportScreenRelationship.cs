// Decompiled with JetBrains decompiler
// Type: UITeamReportScreenRelationship
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITeamReportScreenRelationship : MonoBehaviour
{
  public float animationDuration = 1f;
  public Flag flag;
  public TextMeshProUGUI mechanicName;
  public TextMeshProUGUI ammount;
  public TextMeshProUGUI change;
  public RectTransform unlockContainer;
  public float animationDelay;
  private UITeamReportScreenDriverWidget.UIDriverStatChangeEntry mEntry;
  private Mechanic mMechanic;
  private MechanicBonus mBonusUnlocked;
  private float mTimer;
  private float mTotalTimer;
  private float mChangeValue;
  private bool mAnimating;

  public void Setup(UITeamReportScreenDriverWidget.UIDriverStatChangeEntry inEntry, Mechanic inMechanic)
  {
    this.mAnimating = false;
    this.mEntry = inEntry;
    this.mMechanic = inMechanic;
    this.mBonusUnlocked = (MechanicBonus) null;
    if (this.hasUnlockedBonus(this.mMechanic.bonusOne))
      this.mBonusUnlocked = this.mMechanic.bonusOne;
    else if (this.hasUnlockedBonus(this.mMechanic.bonusTwo))
      this.mBonusUnlocked = this.mMechanic.bonusTwo;
    GameUtility.SetActive(this.unlockContainer.gameObject, this.mBonusUnlocked != null);
    this.mChangeValue = MathsUtility.RoundToDecimal(Mathf.Clamp(this.mEntry.valueChange / this.mEntry.statMax, -1f, 1f) * 100f, 2f);
    GameUtility.SetActive(this.change.gameObject, false);
    this.change.color = this.GetColor(this.mChangeValue);
    this.change.text = this.FormatChange(this.mChangeValue);
    this.ammount.text = string.Format("{0:f2}%", (object) this.mEntry.newValue);
    this.flag.SetNationality(this.mMechanic.nationality);
    this.mechanicName.text = this.mMechanic.name;
  }

  public void Animate()
  {
    this.mTotalTimer = 0.0f;
    this.mAnimating = true;
  }

  public void Update()
  {
    if (!this.mAnimating)
      return;
    this.mTotalTimer += GameTimer.deltaTime;
    this.mTimer = this.mTotalTimer - this.animationDelay;
    if ((double) this.mTimer < 0.0 || (double) this.mTimer < (double) this.animationDuration)
      return;
    GameUtility.SetActive(this.change.gameObject, true);
    this.mAnimating = false;
  }

  private string FormatChange(float inValue)
  {
    if ((double) inValue == 0.0)
      return "-";
    return ((double) inValue <= 0.0 ? string.Empty : "+") + string.Format("{0:f2}%", (object) this.mChangeValue);
  }

  private Color GetColor(float inValue)
  {
    if ((double) inValue > 0.0)
      return UIConstants.positiveColor;
    if ((double) inValue == 0.0)
      return UIConstants.whiteColor;
    return UIConstants.negativeColor;
  }

  private bool hasUnlockedBonus(MechanicBonus inBonus)
  {
    if (inBonus != null && (double) this.mEntry.oldValue < (double) inBonus.bonusUnlockAt)
      return (double) this.mEntry.newValue >= (double) inBonus.bonusUnlockAt;
    return false;
  }

  public void ShowToolTip()
  {
    if (this.mBonusUnlocked == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().ShowTooltip(true, this.mBonusUnlocked);
  }

  public void HideToolTip()
  {
    if (this.mBonusUnlocked == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<UIDialogBoxMechanicBonusTooltip>().HideTooltip();
  }
}
