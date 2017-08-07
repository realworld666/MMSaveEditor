using System;

namespace FullSerializer.Internal
{
	// Token: 0x02000092 RID: 146
	public class fsPrimitiveConverter : fsConverter
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x0001BFAC File Offset: 0x0001A1AC
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsPrimitive || type == typeof(string) || type == typeof(decimal);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		private static bool UseBool(Type type)
		{
			return type == typeof(bool);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001C004 File Offset: 0x0001A204
		private static bool UseInt64(Type type)
		{
			return type == typeof(sbyte) || type == typeof(byte) || type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001C094 File Offset: 0x0001A294
		private static bool UseDouble(Type type)
		{
			return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		private static bool UseString(Type type)
		{
			return type == typeof(string) || type == typeof(char);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001C104 File Offset: 0x0001A304
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Type type = instance.GetType();
			if (this.Serializer.Config.Serialize64BitIntegerAsString && (type == typeof(long) || type == typeof(ulong)))
			{
				serialized = new fsData((string)Convert.ChangeType(instance, typeof(string)));
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseBool(type))
			{
				serialized = new fsData((bool)instance);
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseInt64(type))
			{
				serialized = new fsData((long)Convert.ChangeType(instance, typeof(long)));
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseDouble(type))
			{
				if (instance.GetType() == typeof(float) && (float)instance != -3.40282347E+38f && (float)instance != 3.40282347E+38f && !float.IsInfinity((float)instance) && !float.IsNaN((float)instance))
				{
					serialized = new fsData((double)((decimal)((float)instance)));
					return fsResult.Success;
				}
				serialized = new fsData((double)Convert.ChangeType(instance, typeof(double)));
				return fsResult.Success;
			}
			else
			{
				if (fsPrimitiveConverter.UseString(type))
				{
					serialized = new fsData((string)Convert.ChangeType(instance, typeof(string)));
					return fsResult.Success;
				}
				serialized = null;
				return fsResult.Fail("Unhandled primitive type " + instance.GetType());
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
		public override fsResult TryDeserialize(fsData storage, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			if (fsPrimitiveConverter.UseBool(storageType))
			{
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + base.CheckType(storage, fsDataType.Boolean));
				if (fsResult2.Succeeded)
				{
					instance = storage.AsBool;
				}
				return fsResult;
			}
			if (fsPrimitiveConverter.UseDouble(storageType) || fsPrimitiveConverter.UseInt64(storageType))
			{
				if (storage.IsDouble)
				{
					instance = Convert.ChangeType(storage.AsDouble, storageType);
				}
				else if (storage.IsInt64)
				{
					instance = Convert.ChangeType(storage.AsInt64, storageType);
				}
				else
				{
					if (!this.Serializer.Config.Serialize64BitIntegerAsString || !storage.IsString || (storageType != typeof(long) && storageType != typeof(ulong)))
					{
						return fsResult.Fail(string.Concat(new object[]
						{
							base.GetType().Name,
							" expected number but got ",
							storage.Type,
							" in ",
							storage
						}));
					}
					instance = Convert.ChangeType(storage.AsString, storageType);
				}
				return fsResult.Success;
			}
			if (fsPrimitiveConverter.UseString(storageType))
			{
				fsResult fsResult3;
				fsResult = (fsResult3 = fsResult + base.CheckType(storage, fsDataType.String));
				if (fsResult3.Succeeded)
				{
					instance = storage.AsString;
				}
				return fsResult;
			}
			return fsResult.Fail(base.GetType().Name + ": Bad data; expected bool, number, string, but got " + storage);
		}
	}
}
