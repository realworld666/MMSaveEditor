// Decompiled with JetBrains decompiler
// Type: UIDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDriverEntry : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public Flag flag;
  public UIDriverHelmet helmet;
  public UIDriverSeriesIcons driverSeriesIcon;
  public TextMeshProUGUI name;
  public TextMeshProUGUI age;
  public TextMeshProUGUI weightLabel;
  public Button compareButton;
  public Button details;
  public GameObject slotOcupied;
  public GameObject slotEmpty;
  public UIAbilityStars abilityStars;
  private Driver mDriver;

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  private void Start()
  {
    this.details.onClick.AddListener(new UnityAction(this.OnDetailsButtonPress));
    this.compareButton.onClick.AddListener(new UnityAction(this.OnCompareButton));
  }

  public virtual void Setup(Driver inDriver)
  {
    this.mDriver = (Driver) null;
    bool flag = inDriver != null;
    if (inDriver != null)
    {
      this.mDriver = inDriver;
      this.flag.SetNationality(this.mDriver.nationality);
      this.portrait.SetPortrait((Person) this.mDriver);
      this.name.text = this.mDriver.name;
      this.age.text = this.mDriver.GetAge().ToString();
      GameUtility.SetActive(this.weightLabel.gameObject, this.mDriver.hasWeigthSet);
      this.weightLabel.text = GameUtility.GetWeightText((float) this.mDriver.weight, 2);
      this.abilityStars.SetAbilityStarsData(this.mDriver);
      if ((UnityEngine.Object) this.helmet != (UnityEngine.Object) null)
        this.helmet.SetHelmet(this.mDriver);
      if ((UnityEngine.Object) this.driverSeriesIcon != (UnityEngine.Object) null)
        this.driverSeriesIcon.Setup(this.mDriver);
    }
    this.details.onClick.AddListener(new UnityAction(this.OnDetailsButtonPress));
    this.abilityStars.gameObject.SetActive(flag);
    this.slotOcupied.gameObject.SetActive(flag);
    this.slotEmpty.gameObject.SetActive(!flag);
  }

  private void OnDetailsButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver != null)
      UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    else
      UIManager.instance.ChangeScreen("ScoutingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnCompareButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver == null)
      return;
    UIManager.instance.ChangeScreen("CompareStaffScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
