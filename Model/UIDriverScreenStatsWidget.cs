// Decompiled with JetBrains decompiler
// Type: UIDriverScreenStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIDriverScreenStatsWidget : MonoBehaviour
{
  public UIStat[] stats;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    this.stats[0].SetStat(Localisation.LocaliseID("PSG_10001289", (GameObject) null), this.mDriver.GetDriverStats().braking, (Person) this.mDriver);
    this.stats[0].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Braking);
    this.stats[1].SetStat(Localisation.LocaliseID("PSG_10001287", (GameObject) null), this.mDriver.GetDriverStats().cornering, (Person) this.mDriver);
    this.stats[1].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Cornering);
    this.stats[2].SetStat(Localisation.LocaliseID("PSG_10001291", (GameObject) null), this.mDriver.GetDriverStats().smoothness, (Person) this.mDriver);
    this.stats[2].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Smoothness);
    this.stats[3].SetStat(Localisation.LocaliseID("PSG_10001295", (GameObject) null), this.mDriver.GetDriverStats().overtaking, (Person) this.mDriver);
    this.stats[3].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Overtakng);
    this.stats[4].SetStat(Localisation.LocaliseID("PSG_10001293", (GameObject) null), this.mDriver.GetDriverStats().consistency, (Person) this.mDriver);
    this.stats[4].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Consistency);
    this.stats[5].SetStat(Localisation.LocaliseID("PSG_10001297", (GameObject) null), this.mDriver.GetDriverStats().adaptability, (Person) this.mDriver);
    this.stats[5].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Adaptability);
    this.stats[6].SetStat(Localisation.LocaliseID("PSG_10001303", (GameObject) null), this.mDriver.GetDriverStats().fitness, (Person) this.mDriver);
    this.stats[6].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Fitness);
    this.stats[7].SetStat(Localisation.LocaliseID("PSG_10001301", (GameObject) null), this.mDriver.GetDriverStats().feedback, (Person) this.mDriver);
    this.stats[7].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Feedback);
    this.stats[8].SetStat(Localisation.LocaliseID("PSG_10001299", (GameObject) null), this.mDriver.GetDriverStats().focus, (Person) this.mDriver);
    this.stats[8].SetupForDriverStatsModifiersRollover(this.mDriver, PersonalityTrait.StatModified.Focus);
  }
}
