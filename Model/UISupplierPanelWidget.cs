// Decompiled with JetBrains decompiler
// Type: UISupplierPanelWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISupplierPanelWidget : MonoBehaviour
{
  public UISupplierOptionEntry[] supplierOptions = new UISupplierOptionEntry[0];
  public Color emptyDotColor = new Color();
  private List<CarChassisStats.Stats> mHighlightStats = new List<CarChassisStats.Stats>();
  private List<Supplier> mSuppliers = new List<Supplier>();
  public TextMeshProUGUI chooseSupplierLabel;
  public TextMeshProUGUI chooseButtonSupplierLabel;
  public TextMeshProUGUI engineerName;
  public Flag flag;
  public UICharacterPortrait portrait;
  public UIAbilityStars stars;
  public TextMeshProUGUI engineerComment;
  public Button selectSupplierButton;
  public Button leftSupplierButton;
  public Button rightSupplierButton;
  public UIGridList dotList;
  public Animator supplierAnimator;
  private int mSupplierIndex;
  private bool mRightAnim;

  private void Start()
  {
    this.selectSupplierButton.onClick.AddListener(new UnityAction(this.Close));
    this.leftSupplierButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
    this.rightSupplierButton.onClick.AddListener(new UnityAction(this.OnRightButton));
  }

  private void OnLeftButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.mRightAnim)
      --this.mSupplierIndex;
    this.mRightAnim = false;
    this.mSupplierIndex = this.LoopIndex(this.mSupplierIndex);
    this.supplierAnimator.SetTrigger(AnimationHashes.ChangeSupplierRight);
    this.SetEntryToIndex(this.mSupplierIndex);
  }

  private void OnRightButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRightAnim)
      ++this.mSupplierIndex;
    this.mRightAnim = true;
    this.mSupplierIndex = this.LoopIndex(this.mSupplierIndex);
    this.supplierAnimator.SetTrigger(AnimationHashes.ChangeSupplierLeft);
    this.SetEntryToIndex(this.mSupplierIndex);
  }

  private void SetEntryToIndex(int inIndex)
  {
    this.supplierOptions[0].Setup(this.mSuppliers[this.LoopIndex(inIndex - 2)], this.mSuppliers[inIndex].supplierType);
    this.supplierOptions[1].Setup(this.mSuppliers[this.LoopIndex(inIndex - 1)], this.mSuppliers[inIndex].supplierType);
    this.supplierOptions[2].Setup(this.mSuppliers[inIndex], this.mSuppliers[inIndex].supplierType);
    this.supplierOptions[3].Setup(this.mSuppliers[this.LoopIndex(inIndex + 1)], this.mSuppliers[inIndex].supplierType);
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    int index = inIndex;
    if (!this.mRightAnim)
      index = this.LoopIndex(index - 1);
    screen.SetSupplier(this.mSuppliers[index].supplierType, this.mSuppliers[index]);
    this.UpdateEnabledGFX(this.mSuppliers[index].supplierType);
    for (int inIndex1 = 0; inIndex1 < this.mSuppliers.Count; ++inIndex1)
    {
      Transform transform = this.dotList.GetOrCreateItem<Transform>(inIndex1);
      if (index == inIndex1)
        transform.GetComponent<Image>().color = Game.instance.player.GetTeamColor().primaryUIColour.normal;
      else
        transform.GetComponent<Image>().color = this.emptyDotColor;
    }
  }

  private int LoopIndex(int inIndex)
  {
    if (inIndex > this.mSuppliers.Count - 1)
      return 0;
    if (inIndex < 0)
      return this.mSuppliers.Count - 1;
    return inIndex;
  }

  public void OnEnter()
  {
    this.SetupEngineerData();
    this.Close();
  }

  private void SetupEngineerData()
  {
    Engineer personOnJob = (Engineer) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
    this.engineerName.text = personOnJob.name;
    this.flag.SetNationality(personOnJob.nationality);
    this.portrait.SetPortrait((Person) personOnJob);
    this.stars.SetAbilityStarsData((Person) personOnJob);
  }

  public void Setup(Supplier.SupplierType inType)
  {
    this.SetupSupplierOptions(inType);
    string inID = string.Empty;
    switch (inType)
    {
      case Supplier.SupplierType.Engine:
        inID = "PSG_10004267";
        break;
      case Supplier.SupplierType.Brakes:
        inID = "PSG_10004266";
        break;
      case Supplier.SupplierType.Fuel:
        inID = "PSG_10004268";
        break;
      case Supplier.SupplierType.Materials:
        inID = "PSG_10004265";
        break;
    }
    this.engineerComment.text = Localisation.LocaliseID(inID, (GameObject) null);
  }

  public void SetupSupplierOptions(Supplier.SupplierType inType)
  {
    this.mRightAnim = true;
    this.mSuppliers = new List<Supplier>();
    switch (inType)
    {
      case Supplier.SupplierType.Engine:
        this.mSuppliers = Game.instance.supplierManager.engineSuppliers;
        break;
      case Supplier.SupplierType.Brakes:
        this.mSuppliers = Game.instance.supplierManager.brakesSuppliers;
        break;
      case Supplier.SupplierType.Fuel:
        this.mSuppliers = Game.instance.supplierManager.fuelSuppliers;
        break;
      case Supplier.SupplierType.Materials:
        this.mSuppliers = Game.instance.supplierManager.materialsSuppliers;
        break;
    }
    this.chooseSupplierLabel.text = "Choose a " + Localisation.LocaliseEnum((Enum) inType) + " Supplier";
    this.chooseButtonSupplierLabel.text = "Select " + Localisation.LocaliseEnum((Enum) inType) + " Supplier";
    Supplier supplier = UIManager.instance.GetScreen<CarDesignScreen>().supplierList[(int) inType];
    this.mSupplierIndex = supplier == null ? 0 : this.LoopIndex(this.mSuppliers.IndexOf(supplier));
    this.mHighlightStats.Clear();
    int num = 0;
    using (Dictionary<CarChassisStats.Stats, float>.KeyCollection.Enumerator enumerator = this.mSuppliers[0].supplierStats.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        this.mHighlightStats.Add(enumerator.Current);
        ++num;
      }
    }
    this.SetEntryToIndex(this.mSupplierIndex);
    this.UpdateEnabledGFX(inType);
    for (int inIndex = 0; inIndex < this.dotList.itemCount; ++inIndex)
    {
      Transform transform = this.dotList.GetItem<Transform>(inIndex);
      if (inIndex < this.mSuppliers.Count)
        transform.gameObject.SetActive(true);
      else
        transform.gameObject.SetActive(false);
    }
  }

  public void HighlightStats(bool inValue)
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    if (inValue)
      screen.estimatedOutputWidget.HighlightBarsForStats(this.mHighlightStats);
    else
      screen.estimatedOutputWidget.ResetHighlightState();
  }

  private void Update()
  {
    this.HighlightStats(true);
  }

  public void UpdateEnabledGFX(Supplier.SupplierType inType)
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    for (int index = 0; index < this.supplierOptions.Length; ++index)
      this.supplierOptions[index].selectedGFX.SetActive(this.supplierOptions[index].supplier == screen.supplierList[(int) inType]);
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
    this.HighlightStats(false);
  }

  public void Open()
  {
    this.gameObject.SetActive(true);
  }
}
