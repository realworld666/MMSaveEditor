// Decompiled with JetBrains decompiler
// Type: UIPartDevTrackWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDevTrackWidget : MonoBehaviour
{
  public TextMeshProUGUI[] statLabels = new TextMeshProUGUI[0];
  public Dropdown dropdown;
  private Circuit mSelectedCircuit;

  private void Start()
  {
    this.dropdown.onValueChanged.AddListener((UnityAction<int>) (value => this.OnDropdownValueChanged(value)));
  }

  private void UpdateCircuitOptions()
  {
    for (int index = 0; index < this.statLabels.Length; ++index)
    {
      CarStats.RelevantToCircuit inRelevancy = (CarStats.RelevantToCircuit) Mathf.RoundToInt(this.mSelectedCircuit.trackStatsCharacteristics.GetStat((CarStats.StatType) index) - 1f);
      this.statLabels[index].text = Localisation.LocaliseEnum((Enum) inRelevancy);
      this.statLabels[index].color = CarStats.GetColorForCircuitRelevancy(inRelevancy);
    }
  }

  private void OnDropdownValueChanged(int inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Championship championship = Game.instance.player.team.championship;
    this.mSelectedCircuit = championship.calendar[inValue + championship.eventNumber].circuit;
    this.UpdateCircuitOptions();
  }

  public void Setup()
  {
    Championship championship = Game.instance.player.team.championship;
    this.dropdown.ClearOptions();
    for (int eventNumber = championship.eventNumber; eventNumber < championship.calendar.Count; ++eventNumber)
    {
      string text = Localisation.LocaliseID(championship.calendar[eventNumber].circuit.locationNameID, (GameObject) null);
      this.dropdown.get_options().Add(new Dropdown.OptionData(text));
      if (eventNumber == championship.eventNumber)
      {
        this.mSelectedCircuit = championship.calendar[eventNumber].circuit;
        this.dropdown.captionText.text = text;
      }
    }
    this.UpdateCircuitOptions();
  }
}
