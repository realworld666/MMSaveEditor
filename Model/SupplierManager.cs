using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SupplierManager
{
    public List<Supplier> engineSuppliers = new List<Supplier>();
    public List<Supplier> brakesSuppliers = new List<Supplier>();
    public List<Supplier> materialsSuppliers = new List<Supplier>();
    public List<Supplier> fuelSuppliers = new List<Supplier>();
    public List<Supplier> batterySuppliers = new List<Supplier>();
    public List<Supplier> ersAdvancedSuppliers;
    private List<Supplier> mSuppliers = new List<Supplier>();

}
