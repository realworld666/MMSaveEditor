// Decompiled with JetBrains decompiler
// Type: UIQuickRaceCircuitEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIQuickRaceCircuitEntry : MonoBehaviour
{
  private string mCircuitName = string.Empty;
  public TextMeshProUGUI label;
  public Flag flag;
  public Toggle toggle;
  public GameObject interactableBacking;

  public void Start()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnToggleValueChange(value)));
  }

  private void OnToggleValueChange(bool inValue)
  {
    if (!inValue)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    else
      UIManager.instance.GetScreen<QuickRaceSetupScreen>().SetSelectedCircuit(this.mCircuitName);
  }

  public void Setup(Circuit inCircuit)
  {
    Circuit circuit = inCircuit;
    this.label.text = Localisation.LocaliseID(circuit.locationNameID, (GameObject) null);
    this.mCircuitName = circuit.locationName;
    this.flag = this.GetComponentInChildren<Flag>();
    this.flag.SetNationality(circuit.nationality);
    GameUtility.SetActive(this.interactableBacking, this.toggle.interactable);
  }
}
