// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPositionTrackerDriverEntry : MonoBehaviour
{
  public Flag flag;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI teamName;
  public Toggle toggle;
  public Button button;
  public Image backingGFX;
  private UIPositionTrackerDriverToggleWidget mWidget;
  private UIPositionTrackerGraph.TrackerData mData;
  private Driver mDriver;

  public void SetInfo(UIPositionTrackerDriverToggleWidget inWidget, UIPositionTrackerGraph.TrackerData inData)
  {
    this.mWidget = inWidget;
    this.mDriver = inData.driver;
    this.mData = inData;
    this.flag.SetNationality(this.mDriver.nationality);
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.driverName.text = this.mDriver.name;
    this.teamName.text = this.mDriver.contract.GetTeam().name;
    this.backingGFX.color = this.mDriver.GetTeamColor().primaryUIColour.normal;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChanged));
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnMouseClick));
  }

  private void OnMouseClick()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.toggle.isOn = !this.toggle.isOn;
  }

  public void OnToggleChanged(bool value)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mData == null)
      return;
    this.mData.isSelected = value;
    this.mWidget.screen.graphWidget.UpdateLine(this.mData);
  }
}
