// Decompiled with JetBrains decompiler
// Type: UIContractCoolOffMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractCoolOffMessage : MonoBehaviour
{
  public List<Toggle> strikes = new List<Toggle>();
  public UICharacterPortrait portrait;
  public UICharacterPortrait staffPortrait;
  public Flag flag;
  public TextMeshProUGUI jobTitle;
  public TextMeshProUGUI name;
  public TextMeshProUGUI message;
  public TextMeshProUGUI periodCooldown;
  public Button continure;
  private Person mPerson;

  private void Start()
  {
    this.continure.onClick.AddListener(new UnityAction(this.OnContinueButton));
  }

  public void Setup(Person inPerson)
  {
    this.mPerson = inPerson;
    if (this.mPerson is Driver)
    {
      this.portrait.gameObject.SetActive(true);
      this.staffPortrait.gameObject.SetActive(false);
      this.portrait.SetPortrait(this.mPerson);
    }
    else
    {
      this.staffPortrait.gameObject.SetActive(true);
      this.portrait.gameObject.SetActive(false);
      this.staffPortrait.SetPortrait(this.mPerson);
    }
    this.flag.SetNationality(this.mPerson.nationality);
    this.jobTitle.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.job);
    this.name.text = this.mPerson.name;
    this.message.text = Localisation.LocaliseID("PSG_10009264", (GameObject) null);
    this.periodCooldown.text = Localisation.LocaliseID("PSG_10009265", (GameObject) null);
    this.SetStrikes(this.mPerson);
  }

  private void OnContinueButton()
  {
    if (UIManager.instance.backStackLength > 0)
      UIManager.instance.OnBackButton();
    else
      UIManager.instance.ChangeScreen("HomeScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void Open()
  {
    GameUtility.SetActive(this.gameObject, true);
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void SetStrikes(Person inPerson)
  {
    for (int index = 0; index < this.strikes.Count; ++index)
    {
      if (index < inPerson.contractManager.contractPatienceAvailable)
      {
        this.strikes[index].gameObject.SetActive(true);
        this.strikes[index].isOn = true;
      }
      else
        this.strikes[index].gameObject.SetActive(false);
    }
  }
}
