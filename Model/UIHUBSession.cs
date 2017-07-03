// Decompiled with JetBrains decompiler
// Type: UIHUBSession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIHUBSession : MonoBehaviour
{
  public TextMeshProUGUI sessionType;
  public UISessionColor sessionColor;
  public UIHUBSessionSponsor sessionSponsor;
  public UIHUBUltimatumWidget ultimatum;
  public GameObject multipleQualifyingContainer;
  private SessionDetails mSession;

  public SessionDetails session
  {
    get
    {
      return this.mSession;
    }
  }

  public void Setup()
  {
    this.mSession = Game.instance.sessionManager.eventDetails.currentSession;
    GameUtility.SetActive(this.multipleQualifyingContainer, this.mSession.sessionType == SessionDetails.SessionType.Qualifying && Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions);
    this.SetDetails();
    this.sessionSponsor.Setup();
    this.ultimatum.Setup();
  }

  private void SetDetails()
  {
    this.sessionType.text = Localisation.LocaliseEnum((Enum) this.mSession.sessionType);
    this.sessionColor.SetSessionColor(this.mSession.sessionType);
  }
}
