// Decompiled with JetBrains decompiler
// Type: UIHUBKnowledgeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIHUBKnowledgeIcon : MonoBehaviour
{
  public Image[] knowledgeIcons;

  public void SetupForKnowledgeType(PracticeReportSessionData.KnowledgeType inKnowlegdeType)
  {
    for (int index = 0; index < this.knowledgeIcons.Length; ++index)
      this.knowledgeIcons[index].gameObject.SetActive(inKnowlegdeType == (PracticeReportSessionData.KnowledgeType) index);
  }
}
