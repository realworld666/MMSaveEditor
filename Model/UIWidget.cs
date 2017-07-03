// Decompiled with JetBrains decompiler
// Type: UIWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIWidget : MonoBehaviour
{
  private UIScreen mScreen;

  public UIScreen screen
  {
    get
    {
      return this.mScreen;
    }
  }

  public virtual void OnStart(UIScreen inScreen)
  {
    this.mScreen = inScreen;
  }
}
