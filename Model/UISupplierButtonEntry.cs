// Decompiled with JetBrains decompiler
// Type: UISupplierButtonEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISupplierButtonEntry : MonoBehaviour
{
  public Supplier.SupplierType supplierType = Supplier.SupplierType.Brakes;
  private List<CarChassisStats.Stats> mHighlightStats = new List<CarChassisStats.Stats>();
  public Button[] button;
  public TextMeshProUGUI nameLabel;
  public GameObject emptyContainer;
  public GameObject filledContainer;
  public UISupplierLogoWidget logo;
  private Supplier mSupplier;

  public Supplier supplier
  {
    get
    {
      return this.mSupplier;
    }
  }

  private void Start()
  {
    EventTrigger eventTrigger = this.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseEnter()));
    eventTrigger.get_triggers().Add(entry1);
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseExit()));
    eventTrigger.get_triggers().Add(entry2);
  }

  private void OnMouseEnter()
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    screen.estimatedOutputWidget.HighlightBarsForStats(this.mHighlightStats);
    screen.ShowStatContibution(this.mSupplier);
  }

  private void OnMouseExit()
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    screen.estimatedOutputWidget.ResetHighlightState();
    screen.HideStatContribution();
  }

  private void SetHighlightStats()
  {
    Supplier supplier = (Supplier) null;
    switch (this.supplierType)
    {
      case Supplier.SupplierType.Engine:
        supplier = Game.instance.supplierManager.engineSuppliers[0];
        break;
      case Supplier.SupplierType.Brakes:
        supplier = Game.instance.supplierManager.brakesSuppliers[0];
        break;
      case Supplier.SupplierType.Fuel:
        supplier = Game.instance.supplierManager.fuelSuppliers[0];
        break;
      case Supplier.SupplierType.Materials:
        supplier = Game.instance.supplierManager.materialsSuppliers[0];
        break;
    }
    this.mHighlightStats.Clear();
    int num = 0;
    using (Dictionary<CarChassisStats.Stats, float>.KeyCollection.Enumerator enumerator = supplier.supplierStats.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        this.mHighlightStats.Add(enumerator.Current);
        ++num;
      }
    }
  }

  public void OnEnter()
  {
    this.nameLabel.text = Localisation.LocaliseEnum((Enum) this.supplierType);
    for (int index = 0; index < this.button.Length; ++index)
      this.button[index].onClick.AddListener(new UnityAction(this.OnButtonPressed));
    this.mSupplier = (Supplier) null;
    this.UpdateSupplierState();
    this.SetHighlightStats();
  }

  private void OnButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.GetScreen<CarDesignScreen>().HideStatContribution();
  }

  public void SetSupplier(Supplier inSupplier)
  {
    if (inSupplier != null)
    {
      this.mSupplier = inSupplier;
      this.logo.SetLogo(this.mSupplier);
    }
    this.UpdateSupplierState();
    UIManager.instance.GetScreen<CarDesignScreen>().preferencesWidget.UpdateBounds();
  }

  private void UpdateSupplierState()
  {
    if (this.mSupplier == null)
    {
      this.emptyContainer.SetActive(true);
      this.filledContainer.SetActive(false);
    }
    else
    {
      this.emptyContainer.SetActive(false);
      this.filledContainer.SetActive(true);
    }
  }
}
