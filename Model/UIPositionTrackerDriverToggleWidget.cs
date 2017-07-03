// Decompiled with JetBrains decompiler
// Type: UIPositionTrackerDriverToggleWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPositionTrackerDriverToggleWidget : MonoBehaviour
{
  public UIPositionTrackerDriverEntry playerDriver1;
  public UIPositionTrackerDriverEntry playerDriver2;
  public UIGridList list;
  public Button selectAll;
  public Button deselectAll;
  public PositionTrackerScreen screen;

  public void OnStart()
  {
    this.selectAll.onClick.AddListener(new UnityAction(this.OnSelectAllButtonPressed));
    this.deselectAll.onClick.AddListener(new UnityAction(this.OnDeselectAllButtonPressed));
  }

  private void SetToggleValues(bool inValue)
  {
    for (int inIndex = 0; inIndex < this.list.itemCount; ++inIndex)
      this.list.GetItem<UIPositionTrackerDriverEntry>(inIndex).toggle.isOn = inValue;
    this.playerDriver1.toggle.isOn = inValue;
    this.playerDriver2.toggle.isOn = inValue;
  }

  private void OnSelectAllButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetToggleValues(true);
  }

  private void OnDeselectAllButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetToggleValues(false);
  }

  public void Setup()
  {
    int inIndex = 0;
    int length = this.screen.graphWidget.trackerData.Length;
    for (int index = 0; index < length; ++index)
    {
      UIPositionTrackerGraph.TrackerData inData = this.screen.graphWidget.trackerData[index];
      Driver driver = inData.driver;
      inData.isSelected = true;
      UIPositionTrackerDriverEntry trackerDriverEntry;
      if (driver == Game.instance.player.team.GetDriver(0))
        trackerDriverEntry = this.playerDriver1;
      else if (driver == Game.instance.player.team.GetDriver(1))
      {
        trackerDriverEntry = this.playerDriver2;
      }
      else
      {
        trackerDriverEntry = this.list.GetOrCreateItem<UIPositionTrackerDriverEntry>(inIndex);
        ++inIndex;
        inData.isSelected = false;
      }
      trackerDriverEntry.SetInfo(this, inData);
      trackerDriverEntry.toggle.isOn = inData.isSelected;
    }
  }
}
