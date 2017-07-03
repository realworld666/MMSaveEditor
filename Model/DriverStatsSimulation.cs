// Decompiled with JetBrains decompiler
// Type: DriverStatsSimulation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

public class DriverStatsSimulation
{
  public static void RunSimulation(string outFile, Driver inDriver, params DriverStatsSimulationScenario[] inScenarios)
  {
    string directoryName = System.IO.Path.GetDirectoryName(outFile);
    if (!Directory.Exists(directoryName))
      Directory.CreateDirectory(directoryName);
    TextWriter textWriter = (TextWriter) new StreamWriter(outFile, true);
    textWriter.WriteLine("FirstName,LastName,Nationality,DOB,Age,Potential,ImprovementRate,Peak Date,Age At Peak,Peak Duration");
    textWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", (object) inDriver.firstName, (object) inDriver.lastName, (object) inDriver.nationality.localisedCountry, (object) inDriver.dateOfBirth, (object) inDriver.GetAge(), (object) inDriver.GetPotentialString(), (object) inDriver.GetImprovementRate(), (object) inDriver.peakAge, (object) inDriver.GetAgeAtPeak(), (object) inDriver.peakDuration);
    textWriter.WriteLine(string.Empty);
    foreach (DriverStatsSimulationScenario inScenario in inScenarios)
    {
      DateTime inDate = Game.instance.time.now;
      textWriter.WriteLine(string.Empty);
      textWriter.WriteLine(string.Empty);
      textWriter.WriteLine("Scenario - Duration {0} Months", (object) inScenario.mDurationMonths);
      textWriter.WriteLine("Entry,Frequency Days,Amount,Num Per Year,Allow Decline");
      List<DriverStatsProgression> statsProgressionList = new List<DriverStatsProgression>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DriverStatsSimulation.\u003CRunSimulation\u003Ec__AnonStorey49 simulationCAnonStorey49 = new DriverStatsSimulation.\u003CRunSimulation\u003Ec__AnonStorey49();
      using (List<DriverStatsSimulationScenarioEntry>.Enumerator enumerator = inScenario.mScenarioEntries.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          simulationCAnonStorey49.curEntry = enumerator.Current;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          textWriter.WriteLine("{0},{1},{2},{3},{4}", (object) simulationCAnonStorey49.curEntry.mType, (object) simulationCAnonStorey49.curEntry.mFrequencyDays, (object) simulationCAnonStorey49.curEntry.mAmount, (object) simulationCAnonStorey49.curEntry.mNumPerYear, (object) simulationCAnonStorey49.curEntry.mAllowDecline);
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated field
          statsProgressionList.Add(new DriverStatsProgression(App.instance.database.driverStatProgressionData.Find(new Predicate<DatabaseEntry>(simulationCAnonStorey49.\u003C\u003Em__48)), simulationCAnonStorey49.curEntry.mType));
          // ISSUE: reference to a compiler-generated field
          simulationCAnonStorey49.curEntry.mLastDayProcessed = -1;
          // ISSUE: reference to a compiler-generated field
          simulationCAnonStorey49.curEntry.mCountPerYear = 0;
        }
      }
      textWriter.WriteLine(string.Empty);
      textWriter.WriteLine(string.Empty);
      textWriter.WriteLine("Month,Braking,Cornering,Smoothness,Overtaking,Consistency,Adaptability,Fitness,Feedback,Focus,Improvement Rate");
      DriverStats driverStats = new DriverStats(inDriver.GetDriverStats());
      float increaseTimePerDay = PersonConstants.statIncreaseTimePerDay;
      int num1 = 0;
      for (int index1 = 0; index1 < inScenario.mDurationMonths; ++index1)
      {
        float improvementRateForAge = inDriver.GetImprovementRateForAge(inDate);
        int num2 = DateTime.DaysInMonth(inDate.Year, inDate.Month);
        int num3 = 0;
        while (num3 < num2)
        {
          improvementRateForAge = inDriver.GetImprovementRateForAge(inDate);
          if ((double) improvementRateForAge > 0.0)
          {
            for (int index2 = 0; index2 < statsProgressionList.Count; ++index2)
            {
              DriverStatsSimulationScenarioEntry mScenarioEntry = inScenario.mScenarioEntries[index2];
              if ((mScenarioEntry.mLastDayProcessed < 0 || num1 - mScenarioEntry.mLastDayProcessed >= mScenarioEntry.mFrequencyDays) && (mScenarioEntry.mNumPerYear == 0 || mScenarioEntry.mCountPerYear < mScenarioEntry.mNumPerYear))
              {
                DriverStatsProgression inProgression = statsProgressionList[index2];
                driverStats.ApplyStatsProgression(inProgression, increaseTimePerDay * improvementRateForAge * mScenarioEntry.mAmount);
                ++mScenarioEntry.mCountPerYear;
                mScenarioEntry.mLastDayProcessed = num1;
              }
            }
          }
          else if ((double) improvementRateForAge < 0.0)
          {
            for (int index2 = 0; index2 < statsProgressionList.Count; ++index2)
            {
              DriverStatsSimulationScenarioEntry mScenarioEntry = inScenario.mScenarioEntries[index2];
              if (mScenarioEntry.mAllowDecline && (mScenarioEntry.mLastDayProcessed < 0 || num1 - mScenarioEntry.mLastDayProcessed >= mScenarioEntry.mFrequencyDays) && (mScenarioEntry.mNumPerYear == 0 || mScenarioEntry.mCountPerYear < mScenarioEntry.mNumPerYear))
              {
                DriverStatsProgression inProgression = statsProgressionList[index2];
                driverStats.ApplyStatsProgression(inProgression, increaseTimePerDay * improvementRateForAge * mScenarioEntry.mAmount);
                ++mScenarioEntry.mCountPerYear;
                mScenarioEntry.mLastDayProcessed = num1;
              }
            }
          }
          ++num3;
          inDate = inDate.AddDays(1.0);
          ++num1;
        }
        textWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", (object) index1, (object) driverStats.braking, (object) driverStats.cornering, (object) driverStats.smoothness, (object) driverStats.overtaking, (object) driverStats.consistency, (object) driverStats.adaptability, (object) driverStats.fitness, (object) driverStats.feedback, (object) driverStats.focus, (object) improvementRateForAge);
      }
      textWriter.Flush();
    }
  }
}
