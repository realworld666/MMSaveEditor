using System;

namespace FullSerializer.Internal
{
	// Token: 0x02000091 RID: 145
	public class fsNullableConverter : fsConverter
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0001BF3C File Offset: 0x0001A13C
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001BF70 File Offset: 0x0001A170
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			return this.Serializer.TrySerialize(Nullable.GetUnderlyingType(storageType), instance, out serialized);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001BF88 File Offset: 0x0001A188
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			return this.Serializer.TryDeserialize(data, Nullable.GetUnderlyingType(storageType), ref instance);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public override object CreateInstance(fsData data, Type storageType)
		{
			return storageType;
		}
	}
}
