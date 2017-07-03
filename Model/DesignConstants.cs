// Decompiled with JetBrains decompiler
// Type: DesignConstants
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class DesignConstants
{
  public static int carDesignMinMaterialCost = 500000;
  public static int carDesignMaxMaterialCost = 5000000;
  public static int partDesignMinMaterialCost = 500000;
  public static int partDesignMaxMaterialCost = 1500000;
  public static int carLogisticsCost = 250000;
  public static int staffCost = 2500;

  public static int GetHospitalityTravelCost(int inValue)
  {
    if (inValue == 0)
      return 0;
    if (inValue == 1)
      return 10000;
    if (inValue == 2)
      return 50000;
    if (inValue == 3)
      return 100000;
    return inValue == 4 ? 150000 : 250000;
  }
}
