// Decompiled with JetBrains decompiler
// Type: UIDesignNewCarWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDesignNewCarWidget : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI mPercentageLabel;
  [SerializeField]
  private TextMeshProUGUI mEndPreSeasonDateLabel;
  [SerializeField]
  private Slider mProgressBar;
  private NextYearCarDesign mCarDesign;
  private float mNormalizedTime;

  private void OnEnable()
  {
    if (App.instance.gameStateManager.currentState.type != GameState.Type.PreSeasonState)
      return;
    this.mCarDesign = Game.instance.player.team.carManager.nextYearCarDesign;
    if (this.mCarDesign.state != NextYearCarDesign.State.WaitingForDesign)
      this.mEndPreSeasonDateLabel.text = GameUtility.FormatDateTimeToShortDateString(this.mCarDesign.designEndDate);
    else
      this.mEndPreSeasonDateLabel.text = GameUtility.FormatDateTimeToShortDateString(Game.instance.player.team.championship.currentPreSeasonEndDate);
  }

  private void Update()
  {
    this.mNormalizedTime = this.mCarDesign == null || this.mCarDesign.state == NextYearCarDesign.State.WaitingForDesign ? 0.0f : this.mCarDesign.GetNormalizedTimeElapsed();
    GameUtility.SetSliderAmountIfDifferent(this.mProgressBar, this.mNormalizedTime, 1000f);
    this.mPercentageLabel.text = string.Format("{0}%", (object) Mathf.RoundToInt(this.mNormalizedTime * 100f).ToString());
  }
}
