// Decompiled with JetBrains decompiler
// Type: CarPartModelDatabase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartModelDatabase
{
  private List<CarPartModelDatabase.CarPartModel> _carPartModels;
  private List<CarPartModelDatabase.CarPartModel>[] _carPartModelsByType;

  public CarPartModelDatabase(Database database, bool loadTestParts = false)
  {
    this._carPartModelsByType = new List<CarPartModelDatabase.CarPartModel>[7];
    for (int index = 0; index < 7; ++index)
      this._carPartModelsByType[index] = new List<CarPartModelDatabase.CarPartModel>();
    List<DatabaseEntry> carPartModelsData = database.carPartModelsData;
    this._carPartModels = new List<CarPartModelDatabase.CarPartModel>(carPartModelsData.Count);
    for (int index = 0; index < carPartModelsData.Count; ++index)
    {
      DatabaseEntry carPartModelsDatabaseEntry = carPartModelsData[index];
      try
      {
        if (!loadTestParts)
        {
          if (carPartModelsDatabaseEntry.GetIntValue("Test") == 1)
            continue;
        }
        CarPartModelDatabase.CarPartModel carPartModel = CarPartModelDatabase.CarPartModelFromDatabaseEntry(carPartModelsDatabaseEntry);
        this._carPartModels.Add(carPartModel);
        this._carPartModelsByType[(int) carPartModel.type].Add(carPartModel);
      }
      catch (ArgumentException ex)
      {
        Debug.LogErrorFormat("Couldn't load CarPartModel from database; ignoring. Error: {0}", (object) ex);
      }
    }
    this._carPartModels.Sort((Comparison<CarPartModelDatabase.CarPartModel>) ((x, y) => x.id.CompareTo(y.id)));
    for (int index = 1; index < this._carPartModels.Count; ++index)
    {
      if (this._carPartModels[index].id == this._carPartModels[index - 1].id)
        Debug.LogWarningFormat("CarPartModelDatabase: parts with duplicate IDs found: {0}", (object) this._carPartModels[index].id);
    }
    for (int index = 0; index < this._carPartModels.Count; ++index)
    {
      CarPartModelDatabase.CarPartModel carPartModel = this._carPartModels[index];
      if ((double) carPartModel.performanceMin > (double) carPartModel.performanceMax)
        Debug.LogWarningFormat("CarPartModelDatabase: part with min performance greater than max: ID {0}, min {1}, max {2}", (object) carPartModel.id, (object) carPartModel.performanceMin, (object) carPartModel.performanceMax);
    }
    for (int index1 = 0; index1 < this._carPartModelsByType.Length; ++index1)
    {
      List<CarPartModelDatabase.CarPartModel> carPartModelList = this._carPartModelsByType[index1];
      List<KeyValuePair<float, float>> ranges = new List<KeyValuePair<float, float>>(carPartModelList.Count);
      for (int index2 = 0; index2 < carPartModelList.Count; ++index2)
      {
        CarPartModelDatabase.CarPartModel carPartModel = carPartModelList[index2];
        if (!carPartModel.hasPerformanceLimit)
        {
          ranges.Clear();
          break;
        }
        ranges.Add(new KeyValuePair<float, float>(carPartModel.performanceMin, carPartModel.performanceMax));
      }
      if (ranges.Count != 0)
      {
        MathsUtility.CoalesceRanges(ranges);
        if (ranges.Count > 1)
        {
          Debug.LogWarningFormat("Car part model type {0} has performance-limited parts but there are gaps in the ranges:", (object) (CarPartModelDatabase.CarPartModelType) index1);
          ranges.Sort((Comparison<KeyValuePair<float, float>>) ((x, y) => x.Key.CompareTo(y.Key)));
          for (int index2 = 1; index2 < ranges.Count; ++index2)
            Debug.LogWarningFormat("Gap between {0} and {1}", new object[2]
            {
              (object) ranges[index2 - 1].Value,
              (object) ranges[index2].Key
            });
        }
      }
    }
  }

  private static CarPartModelDatabase.CarPartModel CarPartModelFromDatabaseEntry(DatabaseEntry carPartModelsDatabaseEntry)
  {
    return new CarPartModelDatabase.CarPartModel() { id = carPartModelsDatabaseEntry.GetIntValue("ID"), type = CarPartModelDatabase.CarPartModelTypeFromString(carPartModelsDatabaseEntry.GetStringValue("Type")), assetPath = carPartModelsDatabaseEntry.GetStringValue("Asset Path"), friendlyName = carPartModelsDatabaseEntry.GetStringValue("Name"), hasPerformanceLimit = carPartModelsDatabaseEntry.GetIntValue("Has Performance Limit") == 1, performanceMin = carPartModelsDatabaseEntry.GetFloatValue("Performance Min"), performanceMax = carPartModelsDatabaseEntry.GetFloatValue("Performance Max"), championshipRestriction = CarPartModelDatabase.ChampionshipIDsFromString(carPartModelsDatabaseEntry.GetStringValue("Championship Restriction")) };
  }

  private static CarPartModelDatabase.CarPartModelType CarPartModelTypeFromString(string value)
  {
    string key = value;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (CarPartModelDatabase.\u003C\u003Ef__switch\u0024mapB == null)
      {
        // ISSUE: reference to a compiler-generated field
        CarPartModelDatabase.\u003C\u003Ef__switch\u0024mapB = new Dictionary<string, int>(7)
        {
          {
            "Chassis",
            0
          },
          {
            "Brake",
            1
          },
          {
            "Wheel",
            2
          },
          {
            "Front Wing",
            3
          },
          {
            "Rear Wing",
            4
          },
          {
            "Seat",
            5
          },
          {
            "Steering Wheel",
            6
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (CarPartModelDatabase.\u003C\u003Ef__switch\u0024mapB.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            return CarPartModelDatabase.CarPartModelType.Chassis;
          case 1:
            return CarPartModelDatabase.CarPartModelType.Brake;
          case 2:
            return CarPartModelDatabase.CarPartModelType.Wheel;
          case 3:
            return CarPartModelDatabase.CarPartModelType.FrontWing;
          case 4:
            return CarPartModelDatabase.CarPartModelType.RearWing;
          case 5:
            return CarPartModelDatabase.CarPartModelType.Seat;
          case 6:
            return CarPartModelDatabase.CarPartModelType.SteeringWheel;
        }
      }
    }
    throw new ArgumentException("Couldn't match string \"" + value + "\" with a CarPartModelType");
  }

  private static int[] ChampionshipIDsFromString(string value)
  {
    if (value.Equals("any", StringComparison.OrdinalIgnoreCase))
      return new int[0];
    string[] strArray = value.Split(';');
    List<int> intList = new List<int>();
    for (int index = 0; index < strArray.Length; ++index)
    {
      string s = strArray[index];
      int result;
      if (int.TryParse(s, out result))
        intList.Add(result);
      else
        Debug.LogWarningFormat("Championship restriction on car part fails to parse as a number (\"{0}\" as part of whole restriction \"{1}\")", new object[2]
        {
          (object) s,
          (object) value
        });
    }
    return intList.ToArray();
  }

  public static CarPartModelDatabase.CarPartModelType CarPartModelTypeFromCarPartType(CarPart.PartType partType)
  {
    switch (partType)
    {
      case CarPart.PartType.Brakes:
      case CarPart.PartType.BrakesGT:
        return CarPartModelDatabase.CarPartModelType.Brake;
      case CarPart.PartType.FrontWing:
        return CarPartModelDatabase.CarPartModelType.FrontWing;
      case CarPart.PartType.RearWing:
      case CarPart.PartType.RearWingGT:
        return CarPartModelDatabase.CarPartModelType.RearWing;
      default:
        return CarPartModelDatabase.CarPartModelType.Invalid;
    }
  }

  public CarPartModelDatabase.CarPartModel GetPart(int id)
  {
    for (int index = 0; index < this._carPartModels.Count; ++index)
    {
      if (this._carPartModels[index].id == id)
        return this._carPartModels[index];
    }
    Debug.LogErrorFormat("CarPartModel.GetPart: Couldn't find car part with ID {0}", (object) id);
    return (CarPartModelDatabase.CarPartModel) null;
  }

  public int NumOptionsForPartType(CarPartModelDatabase.CarPartModelType partType)
  {
    return this._carPartModelsByType[(int) partType].Count;
  }

  public List<CarPartModelDatabase.CarPartModel> ModelsForPartType(CarPartModelDatabase.CarPartModelType partType)
  {
    return this._carPartModelsByType[(int) partType];
  }

  public bool IsPartValidInChampionship(int id, int championship_id)
  {
    for (int index1 = 0; index1 < this._carPartModels.Count; ++index1)
    {
      if (this._carPartModels[index1].id == id)
      {
        if (this._carPartModels[index1].championshipRestriction.Length == 0)
          return true;
        for (int index2 = 0; index2 < this._carPartModels[index1].championshipRestriction.Length; ++index2)
        {
          if (championship_id == this._carPartModels[index1].championshipRestriction[index2])
            return true;
        }
      }
    }
    return false;
  }

  public enum CarPartModelType
  {
    Chassis,
    Brake,
    Wheel,
    FrontWing,
    RearWing,
    Seat,
    SteeringWheel,
    Count,
    Invalid,
  }

  [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
  public class CarPartModel
  {
    public int id;
    public CarPartModelDatabase.CarPartModelType type;
    public string assetPath;
    public string friendlyName;
    public bool hasPerformanceLimit;
    public float performanceMin;
    public float performanceMax;
    public int[] championshipRestriction;
  }
}
