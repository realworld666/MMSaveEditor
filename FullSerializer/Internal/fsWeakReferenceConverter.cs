using System;

namespace FullSerializer.Internal
{
	// Token: 0x02000095 RID: 149
	public class fsWeakReferenceConverter : fsConverter
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0001C780 File Offset: 0x0001A980
		public override bool CanProcess(Type type)
		{
			return type == typeof(WeakReference);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001C790 File Offset: 0x0001A990
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001C794 File Offset: 0x0001A994
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001C798 File Offset: 0x0001A998
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			WeakReference weakReference = (WeakReference)instance;
			fsResult fsResult = fsResult.Success;
			serialized = fsData.CreateDictionary();
			if (weakReference.IsAlive)
			{
				fsData value;
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + this.Serializer.TrySerialize<object>(weakReference.Target, out value));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				serialized.AsDictionary["Target"] = value;
				serialized.AsDictionary["TrackResurrection"] = new fsData(weakReference.TrackResurrection);
			}
			return fsResult;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001C820 File Offset: 0x0001AA20
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			if (data.AsDictionary.ContainsKey("Target"))
			{
				fsData data2 = data.AsDictionary["Target"];
				object target = null;
				fsResult fsResult3;
				fsResult = (fsResult3 = fsResult + this.Serializer.TryDeserialize(data2, typeof(object), ref target));
				if (fsResult3.Failed)
				{
					return fsResult;
				}
				bool trackResurrection = false;
				if (data.AsDictionary.ContainsKey("TrackResurrection") && data.AsDictionary["TrackResurrection"].IsBool)
				{
					trackResurrection = data.AsDictionary["TrackResurrection"].AsBool;
				}
				instance = new WeakReference(target, trackResurrection);
			}
			return fsResult;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new WeakReference(null);
		}
	}
}
