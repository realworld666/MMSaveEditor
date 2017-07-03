// Decompiled with JetBrains decompiler
// Type: UINationalityFilterEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UINationalityFilterEntry : MonoBehaviour
{
  public Flag flag;
  public Nationality nationality;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI amountLabel;
  public Toggle toggle;
  private Action mUpdateFilterAction;
  private int mTotalAmount;

  public int totalAmount
  {
    get
    {
      return this.mTotalAmount;
    }
  }

  private void Awake()
  {
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.SetFilter));
  }

  public void Setup(Nationality inNationality, Action inAction, int inAmount)
  {
    this.mTotalAmount = inAmount;
    this.mUpdateFilterAction = inAction;
    this.nationality = inNationality;
    this.flag.SetNationality(this.nationality);
    this.nameLabel.text = this.nationality.localisedCountry;
    this.SetupAmount(this.mTotalAmount);
  }

  public void SetupAmount(int inAmount)
  {
    this.amountLabel.text = inAmount.ToString();
  }

  private void SetFilter(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mUpdateFilterAction == null)
      return;
    this.mUpdateFilterAction();
  }
}
