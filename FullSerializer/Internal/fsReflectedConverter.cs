using System;
using System.Collections;

namespace FullSerializer.Internal
{
	// Token: 0x02000093 RID: 147
	public class fsReflectedConverter : fsConverter
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0001C438 File Offset: 0x0001A638
		public override bool CanProcess(Type type)
		{
			return !type.Resolve().IsArray && !typeof(ICollection).IsAssignableFrom(type);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001C470 File Offset: 0x0001A670
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = fsData.CreateDictionary();
			fsResult success = fsResult.Success;
			fsMetaType fsMetaType = fsMetaType.Get(this.Serializer.Config, instance.GetType());
			fsMetaType.EmitAotData();
			for (int i = 0; i < fsMetaType.Properties.Length; i++)
			{
				fsMetaProperty fsMetaProperty = fsMetaType.Properties[i];
				if (fsMetaProperty.CanRead)
				{
					fsData value;
					fsResult result = this.Serializer.TrySerialize(fsMetaProperty.StorageType, fsMetaProperty.OverrideConverterType, fsMetaProperty.Read(instance), out value);
					success.AddMessages(result);
					if (!result.Failed)
					{
						serialized.AsDictionary[fsMetaProperty.JsonName] = value;
					}
				}
			}
			return success;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001C52C File Offset: 0x0001A72C
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			fsMetaType fsMetaType = fsMetaType.Get(this.Serializer.Config, storageType);
			fsMetaType.EmitAotData();
			for (int i = 0; i < fsMetaType.Properties.Length; i++)
			{
				fsMetaProperty property = fsMetaType.Properties[i];
				if (property.CanWrite)
				{
					fsData data2;
					if (data.AsDictionary.TryGetValue(property.JsonName, out data2))
					{
						int referenceId = fsBaseConverter.GetReferenceId(data2);
						if (referenceId != -1 && this.Serializer.IsObjectVersionedAndUnderConstruction(referenceId))
						{
							object temp = instance;
							this.Serializer.WhenInstanceCreated(referenceId, delegate(object refObject)
							{
								property.Write(temp, refObject);
							});
						}
						else
						{
							object value = null;
							if (property.CanRead)
							{
								value = property.Read(instance);
							}
							fsResult result = this.Serializer.TryDeserialize(data2, property.StorageType, property.OverrideConverterType, ref value);
							fsResult.AddMessages(result);
							if (!result.Failed)
							{
								property.Write(instance, value);
							}
						}
					}
				}
			}
			return fsResult;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		public override object CreateInstance(fsData data, Type storageType)
		{
			fsMetaType fsMetaType = fsMetaType.Get(this.Serializer.Config, storageType);
			return fsMetaType.CreateInstance();
		}
	}
}
