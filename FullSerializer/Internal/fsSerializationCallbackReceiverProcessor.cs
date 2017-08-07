using System;
using UnityEngine;

namespace FullSerializer.Internal
{
	// Token: 0x020000B4 RID: 180
	public class fsSerializationCallbackReceiverProcessor : fsObjectProcessor
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0001F464 File Offset: 0x0001D664
		public override bool CanProcess(Type type)
		{
			return typeof(ISerializationCallbackReceiver).IsAssignableFrom(type);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001F478 File Offset: 0x0001D678
		public override void OnBeforeSerialize(Type storageType, object instance)
		{
			if (instance == null)
			{
				return;
			}
			((ISerializationCallbackReceiver)instance).OnBeforeSerialize();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001F48C File Offset: 0x0001D68C
		public override void OnAfterDeserialize(Type storageType, object instance)
		{
			if (instance == null)
			{
				return;
			}
			((ISerializationCallbackReceiver)instance).OnAfterDeserialize();
		}
	}
}
