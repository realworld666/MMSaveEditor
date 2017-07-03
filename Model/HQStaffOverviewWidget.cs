// Decompiled with JetBrains decompiler
// Type: HQStaffOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HQStaffOverviewWidget : MonoBehaviour
{
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI staffLabel;
  public TextMeshProUGUI staffSickLabel;
  public TextMeshProUGUI tierLabel;
  public Image tierBacking;
  public Image[] stars;
  public Button upgradeButton;
  public Button hireButton;

  private void Awake()
  {
    this.upgradeButton.onClick.AddListener(new UnityAction(this.OnUpgradeButton));
    this.hireButton.onClick.AddListener(new UnityAction(this.OnHireButton));
  }

  public void SetOverview(string inTitle, int inTier, int inStars, int inStaff, int inStaffCount, int inSickCount)
  {
    this.titleLabel.text = inTitle;
    this.staffLabel.text = inStaff.ToString() + "/" + inStaffCount.ToString() + " Staff";
    this.staffSickLabel.text = "Off Sick: " + inSickCount.ToString();
    this.SetTierDetails(inTier, inStars);
  }

  private void SetTierDetails(int inTier, int inStars)
  {
    this.tierLabel.text = "T" + inTier.ToString();
    this.tierBacking.color = this.GetTierColor(inTier);
    for (int index = 0; index < this.stars.Length; ++index)
    {
      this.stars[index].gameObject.SetActive(index < inStars);
      this.stars[index].color = this.GetTierColor(inTier);
    }
  }

  private Color GetTierColor(int inTier)
  {
    switch (inTier)
    {
      case 2:
        return UIConstants.colorBandRed;
      case 3:
        return UIConstants.colorBandYellow;
      case 4:
        return UIConstants.colorBandBlue;
      default:
        return UIConstants.colorBandGreen;
    }
  }

  public void OnUpgradeButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  public void OnHireButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }
}
