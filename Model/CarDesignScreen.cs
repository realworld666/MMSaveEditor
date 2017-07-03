// Decompiled with JetBrains decompiler
// Type: CarDesignScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CarDesignScreen : UIScreen
{
  private Supplier[] mSelectedSupplier = new Supplier[4];
  private string mCurrentCamera = string.Empty;
  private string mNextCamera = string.Empty;
  public UIEstimatedOutputWidget estimatedOutputWidget;
  public UIPreferencesSettingsWidget preferencesWidget;
  public UISupplierSelectionWidget supplierSelectionWidget;
  public UICarSupplierRolloverTrigger supplierRollover;
  public GameObject unselectedOptionsWarningContainter;

  public Supplier[] supplierList
  {
    get
    {
      return this.mSelectedSupplier;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.supplierSelectionWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.supplierRollover.Setup(Game.instance.player.team, true);
    UIManager.instance.ClearNavigationStacks();
    this.mSelectedSupplier = !Game.instance.player.team.championship.rules.isEnergySystemActive ? new Supplier[4] : new Supplier[5];
    this.ResetSuppliers();
    this.estimatedOutputWidget.OnEnter();
    this.preferencesWidget.OnEnter();
    this.supplierSelectionWidget.OnEnter();
    this.UpdateStats();
    this.dontAddToBackStack = true;
    this.canUseScreenHotkeys = false;
    this.canEnterPreferencesScreen = false;
    this.needsPlayerConfirmation = true;
    this.showNavigationBars = true;
    SceneManager.instance.SwitchScene("GarageNextYearCar");
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject("GarageNextYearCar");
    if ((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null && this.screenMode == UIScreen.ScreenMode.Mode3D)
    {
      StudioScene component = sceneGameObject.GetComponent<StudioScene>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        TeamColor.LiveryColour colour = new TeamColor.LiveryColour();
        switch (Game.instance.player.team.championship.series)
        {
          case Championship.Series.SingleSeaterSeries:
            this.SetCamera("RearPackageCamera");
            this.preferencesWidget.gameObject.SetActive(true);
            break;
          case Championship.Series.GTSeries:
            this.SetCamera("GTCamera");
            this.preferencesWidget.gameObject.SetActive(false);
            colour.SetAllColours(Color.black);
            break;
        }
        Game.instance.player.team.carManager.nextFrontendCar.SetColours(colour);
        component.SetCarType(StudioScene.Car.NextYearCar);
        component.SetSeries(Game.instance.player.team.championship.series);
        component.SetCarVisualsToCurrentGame();
      }
    }
    long moneyForCarDev = Game.instance.player.team.financeController.moneyForCarDev;
    if (moneyForCarDev != 0L)
    {
      Transaction transaction = new Transaction(Transaction.Group.CarParts, Transaction.Type.Credit, moneyForCarDev, Localisation.LocaliseID("PSG_10010580", (GameObject) null));
      Game.instance.player.team.financeController.unnallocatedTransactions.Add(transaction);
      TeamFinanceController.ShowPopupToPlayer((Action) null);
      Game.instance.player.team.financeController.moneyForCarDev = 0L;
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!this.supplierSelectionWidget.IsComplete())
      this.supplierSelectionWidget.GoToNextStep();
    else
      this.OnStartDesign();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public void Update()
  {
    GameUtility.SetActive(this.unselectedOptionsWarningContainter, !this.supplierSelectionWidget.IsComplete());
  }

  public void OnStartDesign()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CarDesignScreen.\u003COnStartDesign\u003Ec__AnonStorey76 designCAnonStorey76 = new CarDesignScreen.\u003COnStartDesign\u003Ec__AnonStorey76();
    // ISSUE: reference to a compiler-generated field
    designCAnonStorey76.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    designCAnonStorey76.newChassis = new CarChassisStats();
    using (Dictionary<CarChassisStats.Stats, float>.Enumerator enumerator = this.preferencesWidget.sliderStats.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<CarChassisStats.Stats, float> current = enumerator.Current;
        switch (current.Key)
        {
          case CarChassisStats.Stats.TyreWear:
            // ISSUE: reference to a compiler-generated field
            designCAnonStorey76.newChassis.tyreWear = current.Value;
            continue;
          case CarChassisStats.Stats.TyreHeating:
            // ISSUE: reference to a compiler-generated field
            designCAnonStorey76.newChassis.tyreHeating = current.Value;
            continue;
          case CarChassisStats.Stats.FuelEfficiency:
            // ISSUE: reference to a compiler-generated field
            designCAnonStorey76.newChassis.fuelEfficiency = current.Value;
            continue;
          case CarChassisStats.Stats.Improvability:
            // ISSUE: reference to a compiler-generated field
            designCAnonStorey76.newChassis.improvability = current.Value;
            continue;
          default:
            continue;
        }
      }
    }
    for (int index = 0; index < this.mSelectedSupplier.Length; ++index)
    {
      Supplier supplier = this.mSelectedSupplier[index];
      switch (supplier.supplierType)
      {
        case Supplier.SupplierType.Engine:
          // ISSUE: reference to a compiler-generated field
          designCAnonStorey76.newChassis.supplierEngine = supplier;
          break;
        case Supplier.SupplierType.Brakes:
          // ISSUE: reference to a compiler-generated field
          designCAnonStorey76.newChassis.supplierBrakes = supplier;
          break;
        case Supplier.SupplierType.Fuel:
          // ISSUE: reference to a compiler-generated field
          designCAnonStorey76.newChassis.supplierFuel = supplier;
          break;
        case Supplier.SupplierType.Materials:
          // ISSUE: reference to a compiler-generated field
          designCAnonStorey76.newChassis.supplierMaterials = supplier;
          break;
        case Supplier.SupplierType.Battery:
          // ISSUE: reference to a compiler-generated field
          designCAnonStorey76.newChassis.supplierBattery = supplier;
          break;
      }
    }
    // ISSUE: reference to a compiler-generated field
    designCAnonStorey76.newChassis.ApplySupplierStats();
    Finance finance = Game.instance.player.team.financeController.finance;
    Team team = Game.instance.player.team;
    // ISSUE: reference to a compiler-generated field
    Transaction brakesTransaction = designCAnonStorey76.newChassis.GetBrakesTransaction(team);
    // ISSUE: reference to a compiler-generated field
    Transaction engineTransaction = designCAnonStorey76.newChassis.GetEngineTransaction(team);
    // ISSUE: reference to a compiler-generated field
    Transaction fuelTransaction = designCAnonStorey76.newChassis.GetFuelTransaction(team);
    // ISSUE: reference to a compiler-generated field
    Transaction materialTransaction = designCAnonStorey76.newChassis.GetMaterialTransaction(team);
    Transaction transaction = (Transaction) null;
    if (Game.instance.player.team.championship.rules.isEnergySystemActive)
    {
      // ISSUE: reference to a compiler-generated field
      transaction = designCAnonStorey76.newChassis.GetBatteryTransaction(team);
    }
    // ISSUE: reference to a compiler-generated field
    designCAnonStorey76.totalCost = brakesTransaction.amount + engineTransaction.amount + fuelTransaction.amount + materialTransaction.amount;
    if (transaction != null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      designCAnonStorey76.totalCost = designCAnonStorey76.totalCost + transaction.amount;
    }
    // ISSUE: reference to a compiler-generated method
    Action inOnTransactionSucess = new Action(designCAnonStorey76.\u003C\u003Em__103);
    if (transaction != null)
      finance.ProcessTransactions(inOnTransactionSucess, (Action) null, true, engineTransaction, fuelTransaction, materialTransaction, brakesTransaction, transaction);
    else
      finance.ProcessTransactions(inOnTransactionSucess, (Action) null, true, engineTransaction, fuelTransaction, materialTransaction, brakesTransaction);
  }

  public override void OpenConfirmDialogBox(Action inAction)
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    string inTitle = Localisation.LocaliseID("PSG_10001275", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10009119", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
    UIManager.instance.dialogBoxManager.Show("GenericConfirmation");
    dialog.Show(inCancelAction, inCancelString, (Action) null, string.Empty, inText, inTitle);
  }

  public void SetCamera(string inString)
  {
    this.mNextCamera = inString;
    if (this.mNextCamera == this.mCurrentCamera)
      return;
    App.instance.StopCoroutine(this.ChangeCamera());
    App.instance.StartCoroutine(this.ChangeCamera());
  }

  [DebuggerHidden]
  private IEnumerator ChangeCamera()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new CarDesignScreen.\u003CChangeCamera\u003Ec__Iterator1F()
    {
      \u003C\u003Ef__this = this
    };
  }

  public void SetSupplier(Supplier.SupplierType inType, Supplier inSupplier)
  {
    this.mSelectedSupplier[(int) inType] = inSupplier;
    this.UpdateStats();
  }

  public void UpdateStats()
  {
    this.estimatedOutputWidget.ResetStats();
    for (int index = 0; index < this.mSelectedSupplier.Length; ++index)
    {
      if (this.mSelectedSupplier[index] != null)
      {
        using (Dictionary<CarChassisStats.Stats, float>.Enumerator enumerator = this.mSelectedSupplier[index].supplierStats.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<CarChassisStats.Stats, float> current = enumerator.Current;
            this.estimatedOutputWidget.AddToStat(current.Key, current.Value);
          }
        }
      }
    }
    using (Dictionary<CarChassisStats.Stats, float>.Enumerator enumerator = this.preferencesWidget.sliderStats.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<CarChassisStats.Stats, float> current = enumerator.Current;
        this.estimatedOutputWidget.SetMaxStat(current.Key, current.Value);
      }
    }
  }

  public void ShowStatContibution(Supplier inSupplier)
  {
    if (inSupplier == null)
      return;
    using (Dictionary<CarChassisStats.Stats, float>.Enumerator enumerator = inSupplier.supplierStats.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<CarChassisStats.Stats, float> current = enumerator.Current;
        this.estimatedOutputWidget.SetSupplierContribution(current.Key, current.Value);
      }
    }
  }

  public void HideStatContribution()
  {
    this.estimatedOutputWidget.HideSupplierContribution();
  }

  private void ResetSuppliers()
  {
    for (int index = 0; index < this.mSelectedSupplier.Length; ++index)
      this.mSelectedSupplier[index] = (Supplier) null;
  }

  private bool SuppliersChosen()
  {
    bool flag = true;
    for (int index = 0; index < this.mSelectedSupplier.Length; ++index)
    {
      if (this.mSelectedSupplier[index] == null)
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public override void OnExit()
  {
    base.OnExit();
    Game.instance.player.team.carManager.frontendCar.gameObject.SetActive(false);
    SceneManager.instance.LeaveCurrentScene();
  }

  private void UpdateCarDesignAchievements(long inTotalCost)
  {
    float num = 3E+07f;
    if ((double) inTotalCost <= (double) num)
      return;
    App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Spend_30m_On_New_Car);
  }
}
