// Decompiled with JetBrains decompiler
// Type: UISupplierOptionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISupplierOptionEntry : MonoBehaviour
{
  public UISupplierStatEntry[] stats = new UISupplierStatEntry[0];
  private Supplier.SupplierType mType = Supplier.SupplierType.Brakes;
  public UISupplierLogoWidget logo;
  public Button button;
  public GameObject selectedGFX;
  public TextMeshProUGUI cost;
  private Supplier mSupplier;
  private CarDesignScreen mScreen;

  public Supplier supplier
  {
    get
    {
      return this.mSupplier;
    }
  }

  public void Start()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
  }

  public void OnButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mScreen.SetSupplier(this.mType, this.mSupplier);
  }

  public void Setup(Supplier inSupplier, Supplier.SupplierType inType)
  {
    this.mScreen = UIManager.instance.GetScreen<CarDesignScreen>();
    this.mType = inType;
    this.mSupplier = inSupplier;
    this.gameObject.SetActive(this.mSupplier != null);
    if (this.mSupplier == null)
      return;
    this.logo.SetLogo(this.mSupplier);
    this.cost.text = GameUtility.GetCurrencyString((long) this.mSupplier.GetPrice(Game.instance.player.team), 0);
    this.SetStats(inSupplier);
  }

  private void SetStats(Supplier inSupplier)
  {
    int index = 0;
    using (Dictionary<CarChassisStats.Stats, float>.KeyCollection.Enumerator enumerator = inSupplier.supplierStats.Keys.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        CarChassisStats.Stats current = enumerator.Current;
        this.stats[index].Setup(current, inSupplier.supplierStats[current]);
        this.stats[index].gameObject.SetActive(true);
        ++index;
      }
    }
    for (int count = inSupplier.supplierStats.Keys.Count; count < this.stats.Length; ++count)
      this.stats[count].gameObject.SetActive(false);
  }
}
