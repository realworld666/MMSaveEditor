// Decompiled with JetBrains decompiler
// Type: FrontendScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FrontendScene : BackgroundScene
{
  public GameObject garage;
  public GameObject studio;
  public CameraControllerUI studioCameraController;

  private void Start()
  {
    this.SetActiveSection(FrontendScene.Section.None);
  }

  public void SetActiveSection(FrontendScene.Section inSection)
  {
    switch (inSection)
    {
      case FrontendScene.Section.None:
        this.garage.SetActive(false);
        this.studio.SetActive(false);
        break;
      case FrontendScene.Section.Garage:
        this.garage.SetActive(true);
        this.studio.SetActive(false);
        break;
      case FrontendScene.Section.Studio:
        this.garage.SetActive(false);
        this.studio.SetActive(true);
        break;
    }
  }

  public enum Section
  {
    None,
    Garage,
    Studio,
  }
}
