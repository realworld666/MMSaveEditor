// Decompiled with JetBrains decompiler
// Type: UIRadarGraphWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UIRadarGraphWidget : MonoBehaviour
{
  public UIRadarGraphWidget.Stat graphStat = UIRadarGraphWidget.Stat.Performance;
  public Image[] carLegends = new Image[0];
  public Transform[] icons = new Transform[0];
  public TextMeshProUGUI[] iconsLabels = new TextMeshProUGUI[0];
  public UIPolygon[] polygons = new UIPolygon[2];
  public Animator animator;

  public void UpdateGraphData()
  {
    this.UpdateTextLabels();
    if (!Game.IsActive() || Game.instance.player.IsUnemployed())
      return;
    float num1 = 0.1f;
    List<CarPart.PartType> partTypeList = new List<CarPart.PartType>((IEnumerable<CarPart.PartType>) CarPart.GetPartType(Game.instance.player.team.championship.series, true));
    for (int inIndex = 0; inIndex < CarManager.carCount; ++inIndex)
    {
      Car car = Game.instance.player.team.carManager.GetCar(inIndex);
      CarStats stats = car.GetStats();
      for (int index1 = 0; index1 < partTypeList.Count; ++index1)
      {
        float num2 = 10f;
        CarPart.PartType index2 = partTypeList[index1];
        CarStats.StatType statForPartType = CarPart.GetStatForPartType(index2);
        switch (this.graphStat)
        {
          case UIRadarGraphWidget.Stat.Reliability:
            float num3 = 1f;
            CarPart part = car.GetPart(index2);
            this.polygons[inIndex].VerticesDistances[index1] = part != null ? Mathf.Clamp(part.reliability / num3, 0.1f, 1f) : num1;
            break;
          case UIRadarGraphWidget.Stat.Performance:
            float num4 = (float) Game.instance.player.team.championship.rules.partStatSeasonMaxValue[index2] + num2;
            num1 = (float) Game.instance.player.team.championship.rules.partStatSeasonMinValue[index2];
            float num5 = Mathf.Clamp((float) (((double) stats.GetStat(statForPartType) - (double) num1 + (double) num2) / ((double) num4 - (double) num1)), 0.1f, 1f);
            this.polygons[inIndex].VerticesDistances[index1] = num5;
            break;
        }
      }
    }
    this.UpdatePolygonOrder();
    this.PlayAnimation();
  }

  private void UpdatePolygonOrder()
  {
    this.polygons[1].transform.SetAsLastSibling();
    this.polygons[0].transform.SetAsLastSibling();
  }

  private void UpdateTextLabels()
  {
    List<CarPart.PartType> partTypeList = new List<CarPart.PartType>((IEnumerable<CarPart.PartType>) CarPart.GetPartType(Game.instance.player.team.championship.series, true));
    for (int index1 = 0; index1 < CarManager.carCount; ++index1)
    {
      for (int index2 = 0; index2 < partTypeList.Count; ++index2)
      {
        int index3 = partTypeList.Count - 1 - index2;
        CarStats.StatType statForPartType = CarPart.GetStatForPartType(partTypeList[index3]);
        CarPart.SetIcon(this.icons[index2], partTypeList[index3]);
        if (this.graphStat == UIRadarGraphWidget.Stat.Performance)
          this.iconsLabels[index2].text = Localisation.LocaliseEnum((Enum) statForPartType);
        else
          this.iconsLabels[index2].text = Localisation.LocaliseEnum((Enum) partTypeList[index3]);
      }
      Color color = this.carLegends[index1].color;
      this.polygons[index1].color = color;
    }
  }

  public void PlayAnimation()
  {
    if (!this.animator.isActiveAndEnabled)
      return;
    this.animator.SetTrigger(AnimationHashes.Play);
  }

  public enum Stat
  {
    Reliability,
    Performance,
  }
}
