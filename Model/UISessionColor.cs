// Decompiled with JetBrains decompiler
// Type: UISessionColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UISessionColor : MonoBehaviour
{
  [SerializeField]
  [Range(0.0f, 1f)]
  private float alpha = 1f;
  private Image mImage;

  private void Awake()
  {
    this.mImage = this.GetComponent<Image>();
  }

  public void SetSessionColor(SessionDetails.SessionType inSessionType)
  {
    if (!((Object) this.mImage != (Object) null))
      return;
    Color color = Color.white;
    switch (inSessionType)
    {
      case SessionDetails.SessionType.Practice:
        color = UIConstants.practiceBackingColor;
        break;
      case SessionDetails.SessionType.Qualifying:
        color = UIConstants.qualifyingBackingColor;
        break;
      case SessionDetails.SessionType.Race:
        color = UIConstants.raceBackingColor;
        break;
    }
    color.a = this.alpha;
    this.mImage.color = color;
  }
}
