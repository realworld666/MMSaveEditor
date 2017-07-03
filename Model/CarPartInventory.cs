using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartInventory
{
  private List<CarPart> brakesInventory = new List<CarPart>();
  private List<CarPart> engineInventory = new List<CarPart>();
  private List<CarPart> frontWingInventory = new List<CarPart>();
  private List<CarPart> gearboxInventory = new List<CarPart>();
  private List<CarPart> rearWingInventory = new List<CarPart>();
  private List<CarPart> suspensionInventory = new List<CarPart>();
  private List<CarPart> rearWingGTInventory = new List<CarPart>();
  private List<CarPart> brakesGTInventory = new List<CarPart>();
  private List<CarPart> engineGTInventory = new List<CarPart>();
  private List<CarPart> gearboxGTInventory = new List<CarPart>();
  private List<CarPart> suspensionGTInventory = new List<CarPart>();
  private List<List<CarPart>> inventories = new List<List<CarPart>>();
}
