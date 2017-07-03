// Decompiled with JetBrains decompiler
// Type: HomeScreenStatsInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class HomeScreenStatsInfoWidget : MonoBehaviour
{
  public HomeScreenInfoPanel[] panels;

  public void OnStart()
  {
    for (int index = 0; index < this.panels.Length; ++index)
      this.panels[index].OnStart();
  }

  public void Setup()
  {
    for (int index = 0; index < this.panels.Length; ++index)
      this.panels[index].Setup();
  }

  public void AutoSelect()
  {
    HomeScreenInfoPanel homeScreenInfoPanel = this.panels[0];
    int inValue = 0;
    for (int index = 0; index < this.panels.Length; ++index)
    {
      HomeScreenInfoPanel panel = this.panels[index];
      if ((double) panel.GetBarScore() < (double) homeScreenInfoPanel.GetBarScore())
      {
        homeScreenInfoPanel = panel;
        inValue = index;
      }
    }
    this.Show(inValue);
  }

  public void Show(int inValue)
  {
    for (int index = 0; index < this.panels.Length; ++index)
    {
      if (index == inValue)
        this.panels[index].Setup();
      GameUtility.SetActive(this.panels[index].gameObject, index == inValue);
    }
  }
}
