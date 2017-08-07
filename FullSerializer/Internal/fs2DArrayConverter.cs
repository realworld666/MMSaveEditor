using System;
using System.Collections;
using System.Collections.Generic;

namespace FullSerializer.Internal
{
	// Token: 0x02000088 RID: 136
	public class fs2DArrayConverter : fsConverter
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0001AA24 File Offset: 0x00018C24
		public override bool CanProcess(Type type)
		{
			return type.IsArray && type.GetArrayRank() == 2;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001AA40 File Offset: 0x00018C40
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001AA44 File Offset: 0x00018C44
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001AA48 File Offset: 0x00018C48
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Array array = (Array)instance;
			IList list = array;
			Type elementType = storageType.GetElementType();
			fsResult success = fsResult.Success;
			serialized = fsData.CreateDictionary();
			Dictionary<string, fsData> asDictionary = serialized.AsDictionary;
			fsData fsData = fsData.CreateList(list.Count);
			asDictionary.Add("c", new fsData((long)array.GetLength(1)));
			asDictionary.Add("r", new fsData((long)array.GetLength(0)));
			asDictionary.Add("a", fsData);
			List<fsData> asList = fsData.AsList;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					object value = array.GetValue(i, j);
					fsData item;
					fsResult result = this.Serializer.TrySerialize(elementType, value, out item);
					success.AddMessages(result);
					if (!result.Failed)
					{
						asList.Add(item);
					}
				}
			}
			return success;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001AB4C File Offset: 0x00018D4C
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			Type elementType = storageType.GetElementType();
			Dictionary<string, fsData> asDictionary = data.AsDictionary;
			int num;
			fsResult fsResult3;
			fsResult = (fsResult3 = fsResult + base.DeserializeMember<int>(asDictionary, null, "c", out num));
			if (fsResult3.Failed)
			{
				return fsResult;
			}
			int num2;
			fsResult fsResult4;
			fsResult = (fsResult4 = fsResult + base.DeserializeMember<int>(asDictionary, null, "r", out num2));
			if (fsResult4.Failed)
			{
				return fsResult;
			}
			fsData fsData;
			if (!asDictionary.TryGetValue("a", out fsData))
			{
				fsResult.AddMessage("Failed to get flattened list");
				return fsResult;
			}
			fsResult fsResult5;
			fsResult = (fsResult5 = fsResult + base.CheckType(fsData, fsDataType.Array));
			if (fsResult5.Failed)
			{
				return fsResult;
			}
			Array array = Array.CreateInstance(elementType, num2, num);
			List<fsData> asList = fsData.AsList;
			if (num * num2 > asList.Count)
			{
				fsResult.AddMessage("Serialised list has more items than can fit in multidimensional array");
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					fsData data2 = asList[j + i * num];
					object value = null;
					fsResult result = this.Serializer.TryDeserialize(data2, elementType, ref value);
					fsResult.AddMessages(result);
					if (!result.Failed)
					{
						array.SetValue(value, i, j);
					}
				}
			}
			instance = array;
			return fsResult;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001ACCC File Offset: 0x00018ECC
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}
	}
}
