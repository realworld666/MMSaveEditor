// Decompiled with JetBrains decompiler
// Type: UIDriverSeriesIcons
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIDriverSeriesIcons : MonoBehaviour
{
  public ActivateForSeries.GameObjectData[] seriesSpecificData = new ActivateForSeries.GameObjectData[0];

  public void Setup(Driver inDriver)
  {
    GameUtility.SetActive(this.gameObject, Game.instance.championshipManager.isGTSeriesActive);
    if (!Game.instance.championshipManager.isGTSeriesActive)
      return;
    for (int index1 = 0; index1 < this.seriesSpecificData.Length; ++index1)
    {
      ActivateForSeries.GameObjectData gameObjectData = this.seriesSpecificData[index1];
      bool inIsActive = inDriver.joinsAnySeries || inDriver.preferedSeries == gameObjectData.series;
      for (int index2 = 0; index2 < gameObjectData.targetObjects.Length; ++index2)
        GameUtility.SetActive(gameObjectData.targetObjects[index2], inIsActive);
    }
  }
}
