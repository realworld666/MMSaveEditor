// Decompiled with JetBrains decompiler
// Type: UICompareStaffStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UICompareStaffStatsWidget : MonoBehaviour
{
  public List<UICompareStaffStatEntry> stats = new List<UICompareStaffStatEntry>();
  public Color graphColor1 = Color.gray;
  public Color graphColor2 = Color.gray;
  public UIGraphRadar graph;
  public CompareStaffScreen screen;
  public bool drawStats1;
  public bool drawStats2;

  public void Setup()
  {
    this.SetStats();
  }

  public void SetStats()
  {
    if (this.screen.mode == CompareStaffScreen.Mode.Driver)
    {
      Driver person1 = (Driver) this.screen.leftPanel.person;
      Driver person2 = (Driver) this.screen.rightPanel.person;
      DriverStats inStats1 = this.SetDriverStats(person1, ref this.drawStats1);
      DriverStats inStats2 = this.SetDriverStats(person2, ref this.drawStats2);
      this.SetGraph((Person) person1, (Person) person2, 20);
      this.stats[0].Setup(0, Localisation.LocaliseID("PSG_10001289", (GameObject) null), inStats1.braking, inStats2.braking, 20);
      this.stats[1].Setup(1, Localisation.LocaliseID("PSG_10001287", (GameObject) null), inStats1.cornering, inStats2.cornering, 20);
      this.stats[2].Setup(2, Localisation.LocaliseID("PSG_10001291", (GameObject) null), inStats1.smoothness, inStats2.smoothness, 20);
      this.stats[3].Setup(3, Localisation.LocaliseID("PSG_10001295", (GameObject) null), inStats1.overtaking, inStats2.overtaking, 20);
      this.stats[4].Setup(4, Localisation.LocaliseID("PSG_10001293", (GameObject) null), inStats1.consistency, inStats2.consistency, 20);
      this.stats[5].Setup(5, Localisation.LocaliseID("PSG_10001297", (GameObject) null), inStats1.adaptability, inStats2.adaptability, 20);
      GameUtility.SetActive(this.stats[6].gameObject, true);
      this.stats[6].Setup(6, Localisation.LocaliseID("PSG_10001303", (GameObject) null), inStats1.fitness, inStats2.fitness, 20);
      GameUtility.SetActive(this.stats[7].gameObject, true);
      this.stats[7].Setup(7, Localisation.LocaliseID("PSG_10001301", (GameObject) null), inStats1.feedback, inStats2.feedback, 20);
      GameUtility.SetActive(this.stats[8].gameObject, true);
      this.stats[8].Setup(8, Localisation.LocaliseID("PSG_10001299", (GameObject) null), inStats1.focus, inStats2.focus, 20);
      this.AddGraphDataDriver(inStats1, this.graphColor1, this.drawStats1);
      this.AddGraphDataDriver(inStats2, this.graphColor2, this.drawStats2);
    }
    else if (this.screen.mode == CompareStaffScreen.Mode.Engineer)
    {
      Engineer person1 = (Engineer) this.screen.leftPanel.person;
      Engineer person2 = (Engineer) this.screen.rightPanel.person;
      EngineerStats inStats1 = this.SetEngineerStats(person1, ref this.drawStats1);
      EngineerStats inStats2 = this.SetEngineerStats(person2, ref this.drawStats2);
      this.SetGraph((Person) person1, (Person) person2, 20);
      this.stats[0].Setup(0, Localisation.LocaliseID("PSG_10001653", (GameObject) null), inStats1.partContributionStats.topSpeed, inStats2.partContributionStats.topSpeed, 20);
      this.stats[1].Setup(1, Localisation.LocaliseID("PSG_10001654", (GameObject) null), inStats1.partContributionStats.acceleration, inStats2.partContributionStats.acceleration, 20);
      this.stats[2].Setup(2, Localisation.LocaliseID("PSG_10001657", (GameObject) null), inStats1.partContributionStats.braking, inStats2.partContributionStats.braking, 20);
      this.stats[3].Setup(3, Localisation.LocaliseID("PSG_10001651", (GameObject) null), inStats1.partContributionStats.highSpeedCorners, inStats2.partContributionStats.highSpeedCorners, 20);
      this.stats[4].Setup(4, Localisation.LocaliseID("PSG_10001655", (GameObject) null), inStats1.partContributionStats.mediumSpeedCorners, inStats2.partContributionStats.mediumSpeedCorners, 20);
      this.stats[5].Setup(5, Localisation.LocaliseID("PSG_10001652", (GameObject) null), inStats1.partContributionStats.lowSpeedCorners, inStats2.partContributionStats.lowSpeedCorners, 20);
      GameUtility.SetActive(this.stats[6].gameObject, false);
      GameUtility.SetActive(this.stats[7].gameObject, false);
      GameUtility.SetActive(this.stats[8].gameObject, false);
      this.AddGraphDataEngineer(inStats1, this.graphColor1, this.drawStats1);
      this.AddGraphDataEngineer(inStats2, this.graphColor2, this.drawStats2);
    }
    else
    {
      if (this.screen.mode != CompareStaffScreen.Mode.Mechanic)
        return;
      Mechanic person1 = (Mechanic) this.screen.leftPanel.person;
      Mechanic person2 = (Mechanic) this.screen.rightPanel.person;
      MechanicStats inStats1 = this.SetMechanicStats(person1, ref this.drawStats1);
      MechanicStats inStats2 = this.SetMechanicStats(person2, ref this.drawStats2);
      this.SetGraph((Person) person1, (Person) person2, 20);
      this.stats[0].Setup(0, Localisation.LocaliseID("PSG_10006087", (GameObject) null), inStats1.concentration, inStats2.concentration, 20);
      this.stats[1].Setup(1, Localisation.LocaliseID("PSG_10006088", (GameObject) null), inStats1.leadership, inStats2.leadership, 20);
      this.stats[2].Setup(2, Localisation.LocaliseID("PSG_10005767", (GameObject) null), inStats1.pitStops, inStats2.pitStops, 20);
      this.stats[3].Setup(3, Localisation.LocaliseID("PSG_10002078", (GameObject) null), inStats1.reliability, inStats2.reliability, 20);
      this.stats[4].Setup(4, Localisation.LocaliseID("PSG_10006091", (GameObject) null), inStats1.speed, inStats2.speed, 20);
      this.stats[5].Setup(5, Localisation.LocaliseID("PSG_10001454", (GameObject) null), inStats1.performance, inStats2.performance, 20);
      GameUtility.SetActive(this.stats[6].gameObject, false);
      GameUtility.SetActive(this.stats[7].gameObject, false);
      GameUtility.SetActive(this.stats[8].gameObject, false);
      this.AddGraphDataMechanic(inStats1, this.graphColor1, this.drawStats1);
      this.AddGraphDataMechanic(inStats2, this.graphColor2, this.drawStats2);
    }
  }

  public DriverStats SetDriverStats(Driver inDriver, ref bool inDrawStats)
  {
    DriverStats driverStats;
    if (inDriver != null)
    {
      driverStats = inDriver.GetDriverStats();
      inDrawStats = true;
    }
    else
    {
      driverStats = new DriverStats();
      driverStats.ClearForStats();
      inDrawStats = false;
    }
    return driverStats;
  }

  public EngineerStats SetEngineerStats(Engineer inEngineer, ref bool inDrawStats)
  {
    EngineerStats engineerStats;
    if (inEngineer != null)
    {
      engineerStats = inEngineer.stats;
      inDrawStats = true;
    }
    else
    {
      engineerStats = new EngineerStats();
      engineerStats.ClearForStats();
      inDrawStats = false;
    }
    return engineerStats;
  }

  public MechanicStats SetMechanicStats(Mechanic inMechanic, ref bool inDrawStats)
  {
    MechanicStats mechanicStats;
    if (inMechanic != null)
    {
      mechanicStats = inMechanic.stats;
      inDrawStats = true;
    }
    else
    {
      mechanicStats = new MechanicStats();
      mechanicStats.ClearForStats();
      inDrawStats = false;
    }
    return mechanicStats;
  }

  public void SetGraph(Person inFirstPerson, Person inSecondPerson, int inMax)
  {
    if (inFirstPerson != null)
      this.graphColor1 = inFirstPerson.GetTeamColor().primaryUIColour.normal;
    if (inSecondPerson != null)
    {
      this.graphColor2 = inSecondPerson.GetTeamColor().primaryUIColour.normal;
      if (this.graphColor1 == this.graphColor2)
        this.graphColor2 = inSecondPerson.GetTeamColor().secondaryUIColour.normal;
      this.graphColor2.a *= 0.7f;
    }
    if (this.screen.mode == CompareStaffScreen.Mode.Driver && this.graph.numberOfLines != 8)
    {
      this.graph.DestroyGFX();
      this.graph.numberOfLines = 9;
    }
    else if (this.screen.mode == CompareStaffScreen.Mode.Engineer && this.graph.numberOfLines != 7)
    {
      this.graph.DestroyGFX();
      this.graph.numberOfLines = 6;
    }
    else if (this.screen.mode == CompareStaffScreen.Mode.Mechanic && this.graph.numberOfLines != 5)
    {
      this.graph.DestroyGFX();
      this.graph.numberOfLines = 6;
    }
    this.graph.displayLabel = true;
    this.graph.maxValue = inMax;
    this.graph.GenerateGFX();
    this.graph.ClearGraphs();
  }

  public void AddGraphDataDriver(DriverStats inStats, Color inColor, bool inDrawGraph)
  {
    if (!inDrawGraph)
      return;
    this.graph.graphData.Add(new UIGraphRadarDataEntry()
    {
      graphLabels = {
        Localisation.LocaliseID("PSG_10001289", (GameObject) null),
        Localisation.LocaliseID("PSG_10001287", (GameObject) null),
        Localisation.LocaliseID("PSG_10001291", (GameObject) null),
        Localisation.LocaliseID("PSG_10001295", (GameObject) null),
        Localisation.LocaliseID("PSG_10001293", (GameObject) null),
        Localisation.LocaliseID("PSG_10001297", (GameObject) null),
        Localisation.LocaliseID("PSG_10001303", (GameObject) null),
        Localisation.LocaliseID("PSG_10001301", (GameObject) null),
        Localisation.LocaliseID("PSG_10001299", (GameObject) null)
      },
      graphData = {
        inStats.braking,
        inStats.cornering,
        inStats.smoothness,
        inStats.overtaking,
        inStats.consistency,
        inStats.adaptability,
        inStats.fitness,
        inStats.feedback,
        inStats.focus
      },
      graphColor = inColor,
      dotSize = 3,
      lineThickness = 3
    });
    this.graph.CreateGraph(this.graph.graphData[this.graph.graphData.Count - 1]);
  }

  public void AddGraphDataEngineer(EngineerStats inStats, Color inColor, bool inDrawGraph)
  {
    if (!inDrawGraph)
      return;
    this.graph.graphData.Add(new UIGraphRadarDataEntry()
    {
      graphLabels = {
        Localisation.LocaliseID("PSG_10001653", (GameObject) null),
        Localisation.LocaliseID("PSG_10001654", (GameObject) null),
        Localisation.LocaliseID("PSG_10001657", (GameObject) null),
        Localisation.LocaliseID("PSG_10001651", (GameObject) null),
        Localisation.LocaliseID("PSG_10001655", (GameObject) null),
        Localisation.LocaliseID("PSG_10001652", (GameObject) null)
      },
      graphData = {
        inStats.partContributionStats.topSpeed,
        inStats.partContributionStats.acceleration,
        inStats.partContributionStats.braking,
        inStats.partContributionStats.lowSpeedCorners,
        inStats.partContributionStats.mediumSpeedCorners,
        inStats.partContributionStats.highSpeedCorners
      },
      graphColor = inColor,
      dotSize = 3,
      lineThickness = 3
    });
    this.graph.CreateGraph(this.graph.graphData[this.graph.graphData.Count - 1]);
  }

  public void AddGraphDataMechanic(MechanicStats inStats, Color inColor, bool inDrawGraph)
  {
    if (!inDrawGraph)
      return;
    this.graph.graphData.Add(new UIGraphRadarDataEntry()
    {
      graphLabels = {
        Localisation.LocaliseID("PSG_10006087", (GameObject) null),
        Localisation.LocaliseID("PSG_10006088", (GameObject) null),
        Localisation.LocaliseID("PSG_10005767", (GameObject) null),
        Localisation.LocaliseID("PSG_10002078", (GameObject) null),
        Localisation.LocaliseID("PSG_10006091", (GameObject) null),
        Localisation.LocaliseID("PSG_10001454", (GameObject) null)
      },
      graphData = {
        inStats.concentration,
        inStats.leadership,
        inStats.pitStops,
        inStats.reliability,
        inStats.speed,
        inStats.performance
      },
      graphColor = inColor,
      dotSize = 3,
      lineThickness = 3
    });
    this.graph.CreateGraph(this.graph.graphData[this.graph.graphData.Count - 1]);
  }
}
