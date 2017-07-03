// Decompiled with JetBrains decompiler
// Type: UIDriverEventData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDriverEventData : MonoBehaviour
{
  public Color[] podiumColors = new Color[0];
  public Color labelColor = new Color();
  public Color labelColorFaded = new Color();
  public Image backing;
  public TextMeshProUGUI pointsForEvent;

  public void SetPointsForEvent(Driver inDriver, RaceEventDetails inDetails)
  {
    if (!inDetails.hasEventEnded)
    {
      this.backing.enabled = false;
      this.pointsForEvent.text = string.Empty;
    }
    else
    {
      int num1 = -1;
      int num2 = 20;
      int count = inDetails.results.GetAllResultsForSession(SessionDetails.SessionType.Race).Count;
      for (int index = 0; index < count; ++index)
      {
        RaceEventResults.ResultData resultForDriver = inDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(inDriver);
        if (resultForDriver != null)
        {
          if (num1 == -1)
            num1 = 0;
          num1 += resultForDriver.points;
          num2 = resultForDriver.position;
        }
      }
      this.pointsForEvent.text = num1 != -1 ? (num1 != 0 ? num1.ToString() : string.Empty) : "-";
      this.pointsForEvent.color = num1 > 0 ? this.labelColor : this.labelColorFaded;
      if (num2 >= 1 && num2 <= 3)
      {
        int index = Mathf.Clamp(num2 - 1, 0, this.podiumColors.Length);
        this.backing.enabled = true;
        this.backing.color = this.podiumColors[index];
      }
      else
        this.backing.enabled = false;
    }
  }
}
