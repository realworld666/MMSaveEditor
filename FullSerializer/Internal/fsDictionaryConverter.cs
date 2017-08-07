using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace FullSerializer.Internal
{
	// Token: 0x0200008A RID: 138
	public class fsDictionaryConverter : fsConverter
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0001AF98 File Offset: 0x00019198
		public override bool CanProcess(Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001AFAC File Offset: 0x000191AC
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001AFC4 File Offset: 0x000191C4
		public override fsResult TryDeserialize(fsData data, ref object instance_, Type storageType)
		{
			IDictionary dictionary = (IDictionary)instance_;
			fsResult fsResult = fsResult.Success;
			Type storageType2;
			Type storageType3;
			fsDictionaryConverter.GetKeyValueTypes(dictionary.GetType(), out storageType2, out storageType3);
			if (data.IsList)
			{
				List<fsData> asList = data.AsList;
				for (int i = 0; i < asList.Count; i++)
				{
					fsData data2 = asList[i];
					fsResult fsResult2;
					fsResult = (fsResult2 = fsResult + base.CheckType(data2, fsDataType.Object));
					if (fsResult2.Failed)
					{
						return fsResult;
					}
					fsData data3;
					fsResult fsResult3;
					fsResult = (fsResult3 = fsResult + base.CheckKey(data2, "Key", out data3));
					if (fsResult3.Failed)
					{
						return fsResult;
					}
					fsData data4;
					fsResult fsResult4;
					fsResult = (fsResult4 = fsResult + base.CheckKey(data2, "Value", out data4));
					if (fsResult4.Failed)
					{
						return fsResult;
					}
					object key = null;
					object value = null;
					fsResult fsResult5;
					fsResult = (fsResult5 = fsResult + this.Serializer.TryDeserialize(data3, storageType2, ref key));
					if (fsResult5.Failed)
					{
						return fsResult;
					}
					fsResult fsResult6;
					fsResult = (fsResult6 = fsResult + this.Serializer.TryDeserialize(data4, storageType3, ref value));
					if (fsResult6.Failed)
					{
						return fsResult;
					}
					this.AddItemToDictionary(dictionary, key, value);
				}
			}
			else
			{
				if (data.IsDictionary)
				{
					foreach (KeyValuePair<string, fsData> current in data.AsDictionary)
					{
						if (!fsSerializer.IsReservedKeyword(current.Key))
						{
							fsData data5 = new fsData(current.Key);
							fsData value2 = current.Value;
							object key2 = null;
							object value3 = null;
							fsResult fsResult7;
							fsResult = (fsResult7 = fsResult + this.Serializer.TryDeserialize(data5, storageType2, ref key2));
							if (fsResult7.Failed)
							{
								fsResult result = fsResult;
								return result;
							}
							fsResult fsResult8;
							fsResult = (fsResult8 = fsResult + this.Serializer.TryDeserialize(value2, storageType3, ref value3));
							if (fsResult8.Failed)
							{
								fsResult result = fsResult;
								return result;
							}
							this.AddItemToDictionary(dictionary, key2, value3);
						}
					}
					return fsResult;
				}
				return base.FailExpectedType(data, new fsDataType[]
				{
					fsDataType.Array,
					fsDataType.Object
				});
			}
			return fsResult;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001B214 File Offset: 0x00019414
		public override fsResult TrySerialize(object instance_, out fsData serialized, Type storageType)
		{
			serialized = fsData.Null;
			fsResult fsResult = fsResult.Success;
			IDictionary dictionary = (IDictionary)instance_;
			Type storageType2;
			Type storageType3;
			fsDictionaryConverter.GetKeyValueTypes(dictionary.GetType(), out storageType2, out storageType3);
			IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
			bool flag = true;
			List<fsData> list = new List<fsData>(dictionary.Count);
			List<fsData> list2 = new List<fsData>(dictionary.Count);
			while (enumerator.MoveNext())
			{
				fsData fsData;
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + this.Serializer.TrySerialize(storageType2, enumerator.Key, out fsData));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				fsData item;
				fsResult fsResult3;
				fsResult = (fsResult3 = fsResult + this.Serializer.TrySerialize(storageType3, enumerator.Value, out item));
				if (fsResult3.Failed)
				{
					return fsResult;
				}
				list.Add(fsData);
				list2.Add(item);
				flag &= fsData.IsString;
			}
			if (flag)
			{
				serialized = fsData.CreateDictionary();
				Dictionary<string, fsData> asDictionary = serialized.AsDictionary;
				for (int i = 0; i < list.Count; i++)
				{
					fsData fsData2 = list[i];
					fsData value = list2[i];
					asDictionary[fsData2.AsString] = value;
				}
			}
			else
			{
				serialized = fsData.CreateList(list.Count);
				List<fsData> asList = serialized.AsList;
				for (int j = 0; j < list.Count; j++)
				{
					fsData value2 = list[j];
					fsData value3 = list2[j];
					Dictionary<string, fsData> dictionary2 = new Dictionary<string, fsData>();
					dictionary2["Key"] = value2;
					dictionary2["Value"] = value3;
					asList.Add(new fsData(dictionary2));
				}
			}
			return fsResult;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001B3CC File Offset: 0x000195CC
		private fsResult AddItemToDictionary(IDictionary dictionary, object key, object value)
		{
			if (key != null && value != null)
			{
				dictionary[key] = value;
				return fsResult.Success;
			}
			Type @interface = fsReflectionUtility.GetInterface(dictionary.GetType(), typeof(ICollection<>));
			if (@interface == null)
			{
				return fsResult.Warn(dictionary.GetType() + " does not extend ICollection");
			}
			Type type = @interface.GetGenericArguments()[0];
			object obj = Activator.CreateInstance(type, new object[]
			{
				key,
				value
			});
			MethodInfo flattenedMethod = @interface.GetFlattenedMethod("Add");
			try
			{
				flattenedMethod.Invoke(dictionary, new object[]
				{
					obj
				});
			}
			catch (Exception ex)
			{
				string str = ex.Message;
				str += string.Empty;
			}
			return fsResult.Success;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001B4A8 File Offset: 0x000196A8
		private static void GetKeyValueTypes(Type dictionaryType, out Type keyStorageType, out Type valueStorageType)
		{
			Type @interface = fsReflectionUtility.GetInterface(dictionaryType, typeof(IDictionary<, >));
			if (@interface != null)
			{
				Type[] genericArguments = @interface.GetGenericArguments();
				keyStorageType = genericArguments[0];
				valueStorageType = genericArguments[1];
			}
			else
			{
				keyStorageType = typeof(object);
				valueStorageType = typeof(object);
			}
		}
	}
}
