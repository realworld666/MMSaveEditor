using System;
using System.Collections;
using System.Collections.Generic;

namespace FullSerializer.Internal
{
	// Token: 0x02000087 RID: 135
	public class fsArrayConverter : fsConverter
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0001A78C File Offset: 0x0001898C
		public override bool CanProcess(Type type)
		{
			return type.IsArray && type.GetArrayRank() == 1;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001A7A8 File Offset: 0x000189A8
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001A7AC File Offset: 0x000189AC
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001A7B0 File Offset: 0x000189B0
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			IList list = (Array)instance;
			Type elementType = storageType.GetElementType();
			fsResult success = fsResult.Success;
			serialized = fsData.CreateList(list.Count);
			List<fsData> asList = serialized.AsList;
			for (int i = 0; i < list.Count; i++)
			{
				object instance2 = list[i];
				fsData item;
				fsResult result = this.Serializer.TrySerialize(elementType, instance2, out item);
				success.AddMessages(result);
				if (!result.Failed)
				{
					asList.Add(item);
				}
			}
			return success;
		}

        // Token: 0x06000418 RID: 1048 RVA: 0x0001A840 File Offset: 0x00018A40
        public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
        {
            var result = fsResult.Success;

            // Verify that we actually have an List
            if ((result += CheckType(data, fsDataType.Array)).Failed)
            {
                return result;
            }

            Type elementType = storageType.GetElementType();

            var serializedList = data.AsList;
            var list = new ArrayList(serializedList.Count);
            int existingCount = list.Count;

            for (int i = 0; i < serializedList.Count; ++i)
            {
                var serializedItem = serializedList[i];
                object deserialized = null;
                if (i < existingCount) deserialized = list[i];

                var itemResult = Serializer.TryDeserialize(serializedItem, elementType, ref deserialized);
                result.AddMessages(itemResult);
                if (itemResult.Failed) continue;

                if (i < existingCount) list[i] = deserialized;
                else list.Add(deserialized);
            }

            instance = list.ToArray(elementType);
            return result;
        }

        // Token: 0x06000419 RID: 1049 RVA: 0x0001AA04 File Offset: 0x00018C04
        public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}
	}
}
