using System;
using System.Collections.Generic;

namespace FullSerializer
{
	// Token: 0x020000AF RID: 175
	public abstract class fsDirectConverter<TModel> : fsDirectConverter
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public override Type ModelType
		{
			get
			{
				return typeof(TModel);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		public sealed override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Dictionary<string, fsData> dictionary = new Dictionary<string, fsData>();
			fsResult result = this.DoSerialize((TModel)((object)instance), dictionary);
			serialized = new fsData(dictionary);
			return result;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001F300 File Offset: 0x0001D500
		public sealed override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			TModel tModel = (TModel)((object)instance);
			fsResult += this.DoDeserialize(data.AsDictionary, ref tModel);
			instance = tModel;
			return fsResult;
		}

		// Token: 0x06000529 RID: 1321
		protected abstract fsResult DoSerialize(TModel model, Dictionary<string, fsData> serialized);

		// Token: 0x0600052A RID: 1322
		protected abstract fsResult DoDeserialize(Dictionary<string, fsData> data, ref TModel model);
	}
}
