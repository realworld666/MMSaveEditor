// Decompiled with JetBrains decompiler
// Type: SwapStaffPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwapStaffPopup : UIDialogBox
{
  private int mDriverIndexToSwap = -1;
  public TextMeshProUGUI explanationLabel;
  public Dropdown driversDropdown;
  private Mechanic mMechanic;

  public void SetupManager(Mechanic inMechanic)
  {
    this.OnOKButton -= new Action(this.SwapStaff);
    this.OnOKButton += new Action(this.SwapStaff);
    this.mMechanic = inMechanic;
    this.driversDropdown.get_options().Clear();
    for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
      this.driversDropdown.get_options().Add(new Dropdown.OptionData(this.mMechanic.contract.GetTeam().GetDriver(inIndex).name));
    this.explanationLabel.gameObject.SetActive(false);
    this.okButton.interactable = false;
    this.driversDropdown.value = this.mMechanic.driver;
    this.driversDropdown.captionText = this.driversDropdown.captionText;
    this.driversDropdown.onValueChanged.AddListener((UnityAction<int>) (value => this.NewDriverIndex(value, this.mMechanic.contract.GetTeam().GetDriver(value).shortName)));
  }

  private void NewDriverIndex(int inIndex, string driverName)
  {
    this.mDriverIndexToSwap = inIndex;
    if (this.mMechanic.driver != this.mDriverIndexToSwap)
    {
      this.explanationLabel.gameObject.SetActive(true);
      this.okButton.interactable = true;
      this.explanationLabel.text = "This will move " + this.mMechanic.shortName + " to be " + driverName + "'s mechanic.";
    }
    else
    {
      this.explanationLabel.gameObject.SetActive(false);
      this.okButton.interactable = false;
    }
  }

  private void SwapStaff()
  {
    if (this.mMechanic.driver == this.mDriverIndexToSwap)
      return;
    this.mMechanic.contract.GetTeam().contractManager.SwapMechanicForDriver(this.mMechanic, this.mDriverIndexToSwap);
    UIManager.instance.RefreshCurrentPage();
  }
}
