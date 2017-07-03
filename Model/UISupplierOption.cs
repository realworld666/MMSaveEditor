// Decompiled with JetBrains decompiler
// Type: UISupplierOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISupplierOption : MonoBehaviour
{
  public UISupplierLogoWidget supplierLogoWidget;
  public TextMeshProUGUI statOne;
  public TextMeshProUGUI statTwo;
  public TextMeshProUGUI statThree;
  public TextMeshProUGUI cost;
  public Toggle toggle;
  public GameObject discountDetails;
  public CanvasGroup canvasGroup;
  public GameObject thirdStatObject;
  private UISupplierSelectionEntry mSupplierEntry;
  private Supplier mSupplier;

  public void Setup(UISupplierSelectionEntry inSupplierOptionEntry, Supplier inSupplier, ToggleGroup inToggleGroup)
  {
    GameUtility.SetActive(this.gameObject, true);
    this.mSupplierEntry = inSupplierOptionEntry;
    this.mSupplier = inSupplier;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.group = (ToggleGroup) null;
    this.toggle.isOn = false;
    this.toggle.group = inToggleGroup;
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    UISupplierOption.SetupSupplierDetails(this.mSupplier, this.supplierLogoWidget, this.thirdStatObject, this.statOne, this.statTwo, this.statThree);
    this.SetupSupplierData();
  }

  private void SetupSupplierData()
  {
    Team team = Game.instance.player.team;
    this.cost.text = GameUtility.GetCurrencyString((long) this.mSupplier.GetPrice(team), 0);
    GameUtility.SetActive(this.discountDetails, this.mSupplier.HasDiscountWithTeam(team));
    if (this.mSupplier.CanTeamBuyThis(team))
    {
      this.canvasGroup.alpha = 1f;
      this.canvasGroup.interactable = true;
    }
    else
    {
      this.canvasGroup.alpha = 0.2f;
      this.canvasGroup.interactable = false;
    }
  }

  public static void SetupSupplierDetails(Supplier inSupplier, UISupplierLogoWidget inSupplierLogoWidget, GameObject inThirdStatObject, params TextMeshProUGUI[] inStatLabels)
  {
    inSupplierLogoWidget.SetLogo(inSupplier);
    switch (inSupplier.supplierType)
    {
      case Supplier.SupplierType.Engine:
      case Supplier.SupplierType.Fuel:
        UISupplierOption.SetStatString(inStatLabels[0], inSupplier.supplierStats[CarChassisStats.Stats.FuelEfficiency]);
        UISupplierOption.SetStatString(inStatLabels[1], inSupplier.supplierStats[CarChassisStats.Stats.Improvability]);
        if (inSupplier.supplierType == Supplier.SupplierType.Engine)
        {
          GameUtility.SetActiveAndCheckNull(inThirdStatObject, true);
          inStatLabels[2].text = string.Format("+{0}", (object) inSupplier.randomEngineLevelModifier.ToString());
          break;
        }
        GameUtility.SetActiveAndCheckNull(inThirdStatObject, false);
        break;
      case Supplier.SupplierType.Brakes:
      case Supplier.SupplierType.Materials:
        UISupplierOption.SetStatString(inStatLabels[0], inSupplier.supplierStats[CarChassisStats.Stats.TyreWear]);
        UISupplierOption.SetStatString(inStatLabels[1], inSupplier.supplierStats[CarChassisStats.Stats.TyreHeating]);
        GameUtility.SetActiveAndCheckNull(inThirdStatObject, false);
        break;
      case Supplier.SupplierType.Battery:
        inStatLabels[0].text = string.Format("{0}%", (object) inSupplier.supplierStats[CarChassisStats.Stats.StartingCharge]);
        inStatLabels[1].text = string.Format("{0}%", (object) Mathf.RoundToInt(inSupplier.randomHarvestEfficiencyModifier * 100f));
        GameUtility.SetActiveAndCheckNull(inThirdStatObject, false);
        break;
    }
  }

  private void OnToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    screen.SetSupplier(this.mSupplier.supplierType, this.mSupplier);
    screen.ShowStatContibution(this.mSupplier);
    screen.preferencesWidget.UpdateBounds();
    this.mSupplierEntry.SupplierSelected(this.mSupplier);
  }

  public static void SetStatString(TextMeshProUGUI inLabel, float inValue)
  {
    if ((double) inValue > 4.0)
    {
      inLabel.color = UIConstants.positiveColor;
      inLabel.text = Localisation.LocaliseID("PSG_10010192", (GameObject) null);
    }
    else if ((double) inValue > 3.0)
    {
      inLabel.color = UIConstants.positiveColor;
      inLabel.text = Localisation.LocaliseID("PSG_10010191", (GameObject) null);
    }
    else if ((double) inValue > 2.0)
    {
      inLabel.color = UIConstants.mailMedia;
      inLabel.text = Localisation.LocaliseID("PSG_10010190", (GameObject) null);
    }
    else if ((double) inValue > 1.0)
    {
      inLabel.color = UIConstants.negativeColor;
      inLabel.text = Localisation.LocaliseID("PSG_10010189", (GameObject) null);
    }
    else
    {
      inLabel.color = UIConstants.negativeColor;
      inLabel.text = Localisation.LocaliseID("PSG_10010188", (GameObject) null);
    }
  }

  public void OnMouseEnter()
  {
    if (this.mSupplier.CanTeamBuyThis(Game.instance.player.team))
      return;
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10010193", (GameObject) null), Localisation.LocaliseID("PSG_10010194", (GameObject) null));
  }

  public void OnMouseExit()
  {
    if (this.mSupplier.CanTeamBuyThis(Game.instance.player.team))
      return;
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }

  public void OnDiscountDetailsEnter()
  {
    Team team = Game.instance.player.team;
    if (!this.mSupplier.HasDiscountWithTeam(team))
      return;
    GenericInfoRollover dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>();
    StringVariableParser.supplierOriginalPrice = this.mSupplier.GetPriceNoDiscount(team.championship, team.championship.rules.batterySize);
    StringVariableParser.supplierDiscountPercent = this.mSupplier.GetTeamDiscount(team);
    dialog.Open(Localisation.LocaliseID("PSG_10010195", (GameObject) null), Localisation.LocaliseID("PSG_10010196", (GameObject) null));
  }

  public void OnDiscountDetailsExit()
  {
    if (!this.mSupplier.HasDiscountWithTeam(Game.instance.player.team))
      return;
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }
}
