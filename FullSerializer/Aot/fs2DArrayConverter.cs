// Decompiled with JetBrains decompiler
// Type: FullSerializer.Internal.fs2DArrayConverter
// Assembly: Assembly-CSharp-firstpass, Version=8.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 124041DF-140C-4BC8-9E08-73B1F0C68F17
// Assembly location: D:\Programs\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp-firstpass.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace FullSerializer.Internal
{
    public class fs2DArrayConverter : fsConverter
    {
        public override bool CanProcess(Type type)
        {
            if (type.IsArray)
                return type.GetArrayRank() == 2;
            return false;
        }

        public override bool RequestCycleSupport(Type storageType)
        {
            return false;
        }

        public override bool RequestInheritanceSupport(Type storageType)
        {
            return false;
        }

        public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
        {
            Array array = (Array)instance;
            IList list1 = (IList)array;
            Type elementType = storageType.GetElementType();
            fsResult success = fsResult.Success;
            serialized = fsData.CreateDictionary();
            Dictionary<string, fsData> asDictionary = serialized.AsDictionary;
            fsData list2 = fsData.CreateList(list1.Count);
            asDictionary.Add("c", new fsData((long)array.GetLength(1)));
            asDictionary.Add("r", new fsData((long)array.GetLength(0)));
            asDictionary.Add("a", list2);
            List<fsData> asList = list2.AsList;
            for (int index1 = 0; index1 < array.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < array.GetLength(1); ++index2)
                {
                    object instance1 = array.GetValue(index1, index2);
                    fsData data;
                    fsResult result = this.Serializer.TrySerialize(elementType, instance1, out data);
                    success.AddMessages(result);
                    if (!result.Failed)
                        asList.Add(data);
                }
            }
            return success;
        }

        public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
        {
            fsResult fsResult1;
            if ((fsResult1 = fsResult.Success + this.CheckType(data, fsDataType.Object)).Failed)
                return fsResult1;
            Type elementType = storageType.GetElementType();
            Dictionary<string, fsData> asDictionary = data.AsDictionary;
            int length2;
            fsResult fsResult2;
            if ((fsResult2 = fsResult1 + this.DeserializeMember<int>(asDictionary, (Type)null, "c", out length2)).Failed)
                return fsResult2;
            int length1;
            fsResult fsResult3;
            if ((fsResult3 = fsResult2 + this.DeserializeMember<int>(asDictionary, (Type)null, "r", out length1)).Failed)
                return fsResult3;
            fsData data1;
            if (!asDictionary.TryGetValue("a", out data1))
            {
                fsResult3.AddMessage("Failed to get flattened list");
                return fsResult3;
            }
            fsResult fsResult4;
            if ((fsResult4 = fsResult3 + this.CheckType(data1, fsDataType.Array)).Failed)
                return fsResult4;
            Array instance1 = Array.CreateInstance(elementType, length1, length2);
            List<fsData> asList = data1.AsList;
            if (length2 * length1 > asList.Count)
                fsResult4.AddMessage("Serialised list has more items than can fit in multidimensional array");
            for (int index1 = 0; index1 < length1; ++index1)
            {
                for (int index2 = 0; index2 < length2; ++index2)
                {
                    fsData data2 = asList[index2 + index1 * length2];
                    object result1 = (object)null;
                    fsResult result2 = this.Serializer.TryDeserialize(data2, elementType, ref result1);
                    fsResult4.AddMessages(result2);
                    if (!result2.Failed)
                        instance1.SetValue(result1, index1, index2);
                }
            }
            instance = (object)instance1;
            return fsResult4;
        }

        public override object CreateInstance(fsData data, Type storageType)
        {
            return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
        }
    }
}
