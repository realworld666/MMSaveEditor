using System;

namespace FullSerializer.Internal
{
	// Token: 0x020000B3 RID: 179
	public class fsSerializationCallbackProcessor : fsObjectProcessor
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		public override bool CanProcess(Type type)
		{
			return typeof(fsISerializationCallbacks).IsAssignableFrom(type);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		public override void OnBeforeSerialize(Type storageType, object instance)
		{
			if (instance == null)
			{
				return;
			}
			((fsISerializationCallbacks)instance).OnBeforeSerialize(storageType);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		public override void OnAfterSerialize(Type storageType, object instance, ref fsData data)
		{
			if (instance == null)
			{
				return;
			}
			((fsISerializationCallbacks)instance).OnAfterSerialize(storageType, ref data);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		public override void OnBeforeDeserializeAfterInstanceCreation(Type storageType, object instance, ref fsData data)
		{
			if (!(instance is fsISerializationCallbacks))
			{
				throw new InvalidCastException(string.Concat(new object[]
				{
					"Please ensure the converter for ",
					storageType,
					" actually returns an instance of it, not an instance of ",
					instance.GetType()
				}));
			}
			((fsISerializationCallbacks)instance).OnBeforeDeserialize(storageType, ref data);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001F444 File Offset: 0x0001D644
		public override void OnAfterDeserialize(Type storageType, object instance)
		{
			if (instance == null)
			{
				return;
			}
			((fsISerializationCallbacks)instance).OnAfterDeserialize(storageType);
		}
	}
}
