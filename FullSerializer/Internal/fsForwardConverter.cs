using System;

namespace FullSerializer.Internal
{
	// Token: 0x0200008D RID: 141
	public class fsForwardConverter : fsConverter
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0001B778 File Offset: 0x00019978
		public fsForwardConverter(fsForwardAttribute attribute)
		{
			this._memberName = attribute.MemberName;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001B78C File Offset: 0x0001998C
		public override bool CanProcess(Type type)
		{
			throw new NotSupportedException("Please use the [fsForward(...)] attribute.");
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001B798 File Offset: 0x00019998
		private fsResult GetProperty(object instance, out fsMetaProperty property)
		{
			fsMetaProperty[] properties = fsMetaType.Get(this.Serializer.Config, instance.GetType()).Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].MemberName == this._memberName)
				{
					property = properties[i];
					return fsResult.Success;
				}
			}
			property = null;
			return fsResult.Fail("No property named \"" + this._memberName + "\" on " + instance.GetType().CSharpName());
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001B820 File Offset: 0x00019A20
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = fsData.Null;
			fsResult fsResult = fsResult.Success;
			fsMetaProperty fsMetaProperty;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + this.GetProperty(instance, out fsMetaProperty));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			object instance2 = fsMetaProperty.Read(instance);
			return this.Serializer.TrySerialize(fsMetaProperty.StorageType, instance2, out serialized);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001B878 File Offset: 0x00019A78
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsMetaProperty fsMetaProperty;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + this.GetProperty(instance, out fsMetaProperty));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			object value = null;
			fsResult fsResult3;
			fsResult = (fsResult3 = fsResult + this.Serializer.TryDeserialize(data, fsMetaProperty.StorageType, ref value));
			if (fsResult3.Failed)
			{
				return fsResult;
			}
			fsMetaProperty.Write(instance, value);
			return fsResult;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}

		// Token: 0x04000397 RID: 919
		private string _memberName;
	}
}
