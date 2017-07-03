// Decompiled with JetBrains decompiler
// Type: UITeamInvestorOfferEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamInvestorOfferEntry : MonoBehaviour
{
  public Button acceptButton;
  public UIInvestorLogo investorLogo;
  public TextMeshProUGUI investorName;
  public TextMeshProUGUI bonus;
  public TextMeshProUGUI pressure;
  public UITeamInvestorOffers widget;
  private Investor mInvestor;

  public void Awake()
  {
    this.acceptButton.onClick.AddListener(new UnityAction(this.OnAcceptButton));
  }

  public void Setup(Investor inInvestor)
  {
    this.mInvestor = inInvestor;
    this.investorLogo.SetInvestor(this.mInvestor);
    this.investorName.text = this.mInvestor.GetName();
    this.bonus.text = this.mInvestor.GetBonusDescription();
    this.pressure.text = this.mInvestor.GetPressureString();
    this.SetPressureColor();
  }

  private void SetPressureColor()
  {
    switch (this.mInvestor.pressure)
    {
      case 1:
        this.pressure.color = UIConstants.colorBandGreen;
        break;
      case 2:
        this.pressure.color = UIConstants.colorBandYellow;
        break;
      case 3:
        this.pressure.color = UIConstants.colorBandRed;
        break;
    }
  }

  private void OnAcceptButton()
  {
    this.widget.SelectInvestor(this.mInvestor);
  }
}
