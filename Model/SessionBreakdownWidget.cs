// Decompiled with JetBrains decompiler
// Type: SessionBreakdownWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class SessionBreakdownWidget : MonoBehaviour
{
  public CanvasGroup[] canvasGroup;
  public GameObject[] arrow;
  public GameObject[] highlight;
  public TextMeshProUGUI[] sessionDateTimeLabel;
  public TextMeshProUGUI[] tvAudeniceLabel;
  public TextMeshProUGUI[] attendanceLabel;

  private void Awake()
  {
  }

  private void Update()
  {
    this.UpdateSessionData();
  }

  private void UpdateSessionData()
  {
    int length = this.highlight.Length;
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    SessionDetails[] sessionDetailsArray = new SessionDetails[length];
    sessionDetailsArray[0] = eventDetails.practiceSessions[0];
    sessionDetailsArray[1] = eventDetails.qualifyingSessions[0];
    sessionDetailsArray[2] = eventDetails.raceSessions[0];
    for (int index = 0; index < length; ++index)
    {
      SessionDetails sessionDetails = sessionDetailsArray[index];
      bool flag = eventDetails.currentSession == sessionDetails;
      this.arrow[index].SetActive(flag);
      this.highlight[index].SetActive(flag);
      this.canvasGroup[index].alpha = !flag ? 0.6f : 1f;
      this.sessionDateTimeLabel[index].text = sessionDetails.sessionDateTime.ToLongDateString();
      if (flag || sessionDetails.hasEnded)
      {
        this.tvAudeniceLabel[index].text = sessionDetails.tvAudience.ToString() + " million";
        this.attendanceLabel[index].text = GameUtility.FormatNumberString(sessionDetails.attendence);
      }
      else
      {
        this.tvAudeniceLabel[index].text = "-";
        this.attendanceLabel[index].text = "-";
      }
    }
  }
}
