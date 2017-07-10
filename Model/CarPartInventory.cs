using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CarPartInventory
{
	public List<CarPart> brakesInventory = new List<CarPart>();
	public List<CarPart> engineInventory = new List<CarPart>();
	public List<CarPart> frontWingInventory = new List<CarPart>();
	public List<CarPart> gearboxInventory = new List<CarPart>();
	public List<CarPart> rearWingInventory = new List<CarPart>();
	public List<CarPart> suspensionInventory = new List<CarPart>();
	public List<CarPart> rearWingGTInventory = new List<CarPart>();
	public List<CarPart> brakesGTInventory = new List<CarPart>();
	public List<CarPart> engineGTInventory = new List<CarPart>();
	public List<CarPart> gearboxGTInventory = new List<CarPart>();
	public List<CarPart> suspensionGTInventory = new List<CarPart>();
	public List<List<CarPart>> inventories = new List<List<CarPart>>();
}
