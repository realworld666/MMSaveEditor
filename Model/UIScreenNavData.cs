// Decompiled with JetBrains decompiler
// Type: UIScreenNavData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIScreenNavData
{
  public UIScreen screen;
  public Entity data;

  public bool IsEqual(UIScreenNavData inNavigationData)
  {
    if ((Object) inNavigationData.screen == (Object) this.screen)
      return this.data == inNavigationData.data;
    return false;
  }

  public bool IsEqual(UIScreen inScreen)
  {
    if ((Object) inScreen == (Object) this.screen)
      return this.data == inScreen.data;
    return false;
  }
}
