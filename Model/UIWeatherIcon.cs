// Decompiled with JetBrains decompiler
// Type: UIWeatherIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIWeatherIcon : MonoBehaviour
{
  public List<GameObject> clouds = new List<GameObject>();
  public List<GameObject> rain = new List<GameObject>();

  public void SetIcon(Weather inWeather)
  {
    if (inWeather == null)
      return;
    int count1 = this.clouds.Count;
    for (int index = 0; index < count1; ++index)
      GameUtility.SetActive(this.clouds[index], inWeather.cloudType == (Weather.CloudType) index);
    int count2 = this.rain.Count;
    for (int index = 0; index < count2; ++index)
      GameUtility.SetActive(this.rain[index], inWeather.rainType == (Weather.RainType) index);
  }
}
