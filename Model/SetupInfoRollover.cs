// Decompiled with JetBrains decompiler
// Type: SetupInfoRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SetupInfoRollover : UIDialogBox
{
  private SessionSetup.SetupOutput mCurrentSetupOutput = new SessionSetup.SetupOutput();
  public UISetupBalanceSliderEntry[] balanceSliders;
  private RectTransform mRectTransform;

  protected override void Awake()
  {
    base.Awake();
    for (int index = 0; index < this.balanceSliders.Length; ++index)
      this.balanceSliders[index].OnStart();
    this.mRectTransform = this.GetComponent<RectTransform>();
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  public void Show(RacingVehicle inVehicle, SessionSetup.SetupOutput inTargetOutput, SetupStintData inStintData)
  {
    GameUtility.SetActive(this.gameObject, true);
    if ((Object) this.mRectTransform == (Object) null)
      this.Awake();
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    inVehicle.setup.GetSetupOutput(ref this.mCurrentSetupOutput);
    for (int index = 0; index < this.balanceSliders.Length; ++index)
    {
      switch (this.balanceSliders[index].setupBalanceEntry)
      {
        case UISetupBalanceSliderEntry.balanceEntryType.Aero:
          this.balanceSliders[index].SetupSlider(inVehicle, inStintData.setupOutput.aerodynamics, inStintData.aerodynamicsOpinion);
          break;
        case UISetupBalanceSliderEntry.balanceEntryType.Speed:
          this.balanceSliders[index].SetupSlider(inVehicle, inStintData.setupOutput.speedBalance, inStintData.speedBalanceOpinion);
          break;
        case UISetupBalanceSliderEntry.balanceEntryType.Handling:
          this.balanceSliders[index].SetupSlider(inVehicle, inStintData.setupOutput.handling, inStintData.handlingOpinion);
          break;
      }
    }
  }

  public override void Hide()
  {
    this.gameObject.SetActive(false);
  }
}
