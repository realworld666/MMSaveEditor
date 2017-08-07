using System;

namespace FullSerializer.Internal
{
	// Token: 0x02000094 RID: 148
	public class fsTypeConverter : fsConverter
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		public override bool CanProcess(Type type)
		{
			return typeof(Type).IsAssignableFrom(type);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		public override bool RequestCycleSupport(Type type)
		{
			return false;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		public override bool RequestInheritanceSupport(Type type)
		{
			return false;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Type type = (Type)instance;
			serialized = new fsData(type.FullName);
			return fsResult.Success;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001C720 File Offset: 0x0001A920
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Type converter requires a string");
			}
			instance = fsTypeCache.GetType(data.AsString);
			if (instance == null)
			{
				return fsResult.Fail("Unable to find type " + data.AsString);
			}
			return fsResult.Success;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001C774 File Offset: 0x0001A974
		public override object CreateInstance(fsData data, Type storageType)
		{
			return storageType;
		}
	}
}
