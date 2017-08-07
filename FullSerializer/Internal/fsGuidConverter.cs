using System;

namespace FullSerializer.Internal
{
	// Token: 0x0200008E RID: 142
	public class fsGuidConverter : fsConverter
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0001B904 File Offset: 0x00019B04
		public override bool CanProcess(Type type)
		{
			return type == typeof(Guid);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001B914 File Offset: 0x00019B14
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001B918 File Offset: 0x00019B18
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001B91C File Offset: 0x00019B1C
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = new fsData(((Guid)instance).ToString());
			return fsResult.Success;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001B944 File Offset: 0x00019B44
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (data.IsString)
			{
				instance = new Guid(data.AsString);
				return fsResult.Success;
			}
			return fsResult.Fail("fsGuidConverter encountered an unknown JSON data type");
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001B974 File Offset: 0x00019B74
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Guid);
		}
	}
}
