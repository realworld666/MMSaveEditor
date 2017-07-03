// Decompiled with JetBrains decompiler
// Type: UIMailMessageHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIMailMessageHeader : MonoBehaviour
{
  public Message.HeaderType headerType;
  public TextMeshProUGUI messageTime;
  public TextMeshProUGUI messageDate;
  public UISponsorLogo sponsorLogo;
  public UITeamLogo teamLogo;
  public UIPressLogo mediaLogo;
  public GameObject IMALogo;
  private Message mMessage;
  private Person mPerson;

  public virtual void OpenMail(Message inMessage)
  {
    this.mMessage = inMessage;
    this.mPerson = inMessage.sender;
    this.messageTime.text = this.mMessage.deliverDate.ToShortTimeString();
    this.messageDate.text = GameUtility.FormatDateTimeToShortDateString(this.mMessage.deliverDate);
    this.SetLogo();
    this.gameObject.SetActive(true);
  }

  private void SetLogo()
  {
    this.IMALogo.SetActive(false);
    this.teamLogo.gameObject.SetActive(false);
    this.sponsorLogo.gameObject.SetActive(false);
    this.mediaLogo.gameObject.SetActive(false);
    if (this.mPerson.contract.GetTeam() != Game.instance.teamManager.nullTeam)
    {
      this.teamLogo.SetTeam(this.mPerson.contract.GetTeam());
      this.teamLogo.gameObject.SetActive(true);
    }
    else if (this.mPerson.contract.GetSponsor() != null)
    {
      this.sponsorLogo.SetSponsor(this.mPerson.contract.GetSponsor());
      this.sponsorLogo.gameObject.SetActive(true);
    }
    else if (this.mPerson.contract.GetMediaOutlet() != null)
    {
      this.mediaLogo.SetMediaOutlet(this.mPerson.contract.GetMediaOutlet());
      this.mediaLogo.gameObject.SetActive(true);
    }
    else
    {
      if (this.mPerson != Game.instance.player.team.championship.politicalSystem.president)
        return;
      this.IMALogo.SetActive(true);
    }
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }
}
