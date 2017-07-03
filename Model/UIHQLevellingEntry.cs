// Decompiled with JetBrains decompiler
// Type: UIHQLevellingEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQLevellingEntry : MonoBehaviour
{
  public Button viewButton;
  public TextMeshProUGUI status;
  public TextMeshProUGUI timeLeft;
  public Slider progressBar;
  public UIHQLevelling widget;
  private HQsBuilding_v1 mBuilding;

  public int secondsRemaining
  {
    get
    {
      if (this.mBuilding != null && this.mBuilding.isLeveling)
        return (int) this.mBuilding.timeRemaining.TotalSeconds;
      return 0;
    }
  }

  public HQsBuilding_v1 building
  {
    get
    {
      return this.mBuilding;
    }
  }

  public void Setup(HQsBuilding_v1 inBuilding)
  {
    this.mBuilding = inBuilding;
    this.viewButton.onClick.RemoveAllListeners();
    this.viewButton.onClick.AddListener(new UnityAction(this.OnClick));
    this.UpdateStatus();
    this.UpdateTimeLeft();
    GameUtility.SetActive(this.gameObject, this.mBuilding.isLeveling);
  }

  private void Update()
  {
    this.UpdateTimeLeft();
  }

  private void UpdateStatus()
  {
    StringVariableParser.building = this.mBuilding;
    this.status.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10009999", (GameObject) null) : Localisation.LocaliseID("PSG_10010037", (GameObject) null);
  }

  private void UpdateTimeLeft()
  {
    if (this.mBuilding == null || !this.mBuilding.isLeveling)
      return;
    this.timeLeft.text = GameUtility.FormatTimeSpanWeeks(this.mBuilding.timeRemaining);
    this.progressBar.value = this.mBuilding.normalizedProgressUI;
  }

  private void OnClick()
  {
    if (this.mBuilding == null)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.widget.screen.SelectBuilding(this.mBuilding);
  }
}
