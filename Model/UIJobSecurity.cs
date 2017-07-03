// Decompiled with JetBrains decompiler
// Type: UIJobSecurity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIJobSecurity : MonoBehaviour
{
  [SerializeField]
  private Image backing;
  [SerializeField]
  private TextMeshProUGUI label;

  public void SetJobSecurity(TeamPrincipal.JobSecurity inJobSecurity)
  {
    if ((UnityEngine.Object) this.backing != (UnityEngine.Object) null)
      this.backing.color = this.GetJobSecurityColor(inJobSecurity);
    if (!((UnityEngine.Object) this.label != (UnityEngine.Object) null))
      return;
    this.label.color = this.GetJobSecurityColor(inJobSecurity);
    this.label.text = Localisation.LocaliseEnum((Enum) inJobSecurity);
  }

  private Color GetJobSecurityColor(TeamPrincipal.JobSecurity inJobSecurity)
  {
    switch (inJobSecurity)
    {
      case TeamPrincipal.JobSecurity.Edge:
        return UIConstants.jobSecurityEdge;
      case TeamPrincipal.JobSecurity.Risk:
        return UIConstants.jobSecurityRisk;
      case TeamPrincipal.JobSecurity.Safe:
        return UIConstants.jobSecuritySafe;
      default:
        return UIConstants.jobSecurityGreat;
    }
  }
}
