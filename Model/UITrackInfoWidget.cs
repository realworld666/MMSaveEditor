// Decompiled with JetBrains decompiler
// Type: UITrackInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITrackInfoWidget : MonoBehaviour
{
  public Image trackTempBar;
  public Image waterOnTrackBar;
  public Image rubberOnTrackBar;
  private SessionWeatherDetails mSessionWeatherDetails;

  private void Start()
  {
  }

  private void OnEnable()
  {
    this.mSessionWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
  }

  private void Update()
  {
    GameUtility.SetImageFillAmountIfDifferent(this.waterOnTrackBar, this.mSessionWeatherDetails.GetNormalizedTrackWater(), 1f / 512f);
    GameUtility.SetImageFillAmountIfDifferent(this.trackTempBar, this.mSessionWeatherDetails.GetNormalizedTrackTemperature(), 1f / 512f);
    GameUtility.SetImageFillAmountIfDifferent(this.rubberOnTrackBar, this.mSessionWeatherDetails.GetNormalizedTrackRubber(), 1f / 512f);
  }
}
