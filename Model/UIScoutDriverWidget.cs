// Decompiled with JetBrains decompiler
// Type: UIScoutDriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutDriverWidget : MonoBehaviour
{
  public TextMeshProUGUI scoutButtonText;
  public Button scoutButton;
  public UIButtonColor buttonColor;
  private Driver mDriver;

  private void Show(bool inShow)
  {
    GameUtility.SetActive(this.gameObject, inShow);
  }

  public void SetupScoutDriverWidget(Driver inDriver)
  {
    this.mDriver = inDriver;
    bool inShow = !Game.instance.player.IsUnemployed() && !inDriver.CanShowStats();
    this.Show(inShow);
    if (!inShow)
      return;
    bool flag = App.instance.gameStateManager.currentState.group != GameState.Group.Frontend;
    this.scoutButton.onClick.RemoveAllListeners();
    this.scoutButton.onClick.AddListener(new UnityAction(this.OnScoutDriverButton));
    this.scoutButton.interactable = !flag;
    this.UpdateWidgetStatus();
  }

  private void OnScoutDriverButton()
  {
    ScoutingManager scoutingManager = Game.instance.scoutingManager;
    if (!scoutingManager.IsDriverInScoutQueue(this.mDriver) && !scoutingManager.IsDriverCurrentlyScouted(this.mDriver))
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      scoutingManager.AddScoutingAssignment(this.mDriver);
      this.UpdateWidgetStatus();
      if (!UIManager.instance.IsScreenOpen("ScoutingScreen"))
        return;
      UIManager.instance.GetScreen<ScoutingScreen>().scoutWidget.Refresh();
    }
    else
      UIManager.instance.ChangeScreen("ScoutingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void UpdateWidgetStatus()
  {
    ScoutingManager scoutingManager = Game.instance.scoutingManager;
    if (scoutingManager.IsDriverInScoutQueue(this.mDriver))
    {
      this.buttonColor.SetMode(UIButtonColor.ColorMode.Close);
      this.scoutButtonText.text = Localisation.LocaliseID("PSG_10007778", (GameObject) null);
    }
    else if (scoutingManager.IsDriverCurrentlyScouted(this.mDriver))
    {
      this.buttonColor.SetMode(UIButtonColor.ColorMode.Close);
      this.scoutButtonText.text = Localisation.LocaliseID("PSG_10009316", (GameObject) null);
    }
    else
    {
      this.buttonColor.SetMode(UIButtonColor.ColorMode.Regular);
      StringVariableParser.subject = (Person) this.mDriver;
      this.scoutButtonText.text = Localisation.LocaliseID("PSG_10007777", (GameObject) null);
      StringVariableParser.subject = (Person) null;
    }
  }
}
