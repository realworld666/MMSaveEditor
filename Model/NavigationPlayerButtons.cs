// Decompiled with JetBrains decompiler
// Type: NavigationPlayerButtons
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class NavigationPlayerButtons : MonoBehaviour
{
  private bool mPlayerEmployed = true;
  public GameObject[] buttons;

  private void Update()
  {
    if (!Game.IsActive())
      return;
    this.mPlayerEmployed = !Game.instance.player.IsUnemployed();
    for (int index = 0; index < this.buttons.Length; ++index)
      GameUtility.SetActive(this.buttons[index], this.mPlayerEmployed);
  }
}
