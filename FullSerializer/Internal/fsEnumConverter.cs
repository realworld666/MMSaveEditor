using System;
using System.Collections.Generic;
using System.Text;

namespace FullSerializer.Internal
{
	// Token: 0x0200008B RID: 139
	public class fsEnumConverter : fsConverter
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0001B504 File Offset: 0x00019704
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsEnum;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001B514 File Offset: 0x00019714
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001B518 File Offset: 0x00019718
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001B51C File Offset: 0x0001971C
		public override object CreateInstance(fsData data, Type storageType)
		{
			return Enum.ToObject(storageType, 0);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001B52C File Offset: 0x0001972C
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			if (this.Serializer.Config.SerializeEnumsAsInteger)
			{
				serialized = new fsData(Convert.ToInt64(instance));
			}
			else if (fsPortableReflection.GetAttribute<FlagsAttribute>(storageType) != null)
			{
				long num = Convert.ToInt64(instance);
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object current in Enum.GetValues(storageType))
				{
					long num2 = Convert.ToInt64(current);
					bool flag2 = (num & num2) != 0L;
					if (flag2)
					{
						if (!flag)
						{
							stringBuilder.Append(",");
						}
						flag = false;
						stringBuilder.Append(current.ToString());
					}
				}
				serialized = new fsData(stringBuilder.ToString());
			}
			else
			{
				serialized = new fsData(Enum.GetName(storageType, instance));
			}
			return fsResult.Success;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001B63C File Offset: 0x0001983C
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (data.IsString)
			{
				string[] array = data.AsString.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				long num = 0L;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (!fsEnumConverter.ArrayContains<string>(Enum.GetNames(storageType), text))
					{
						return fsResult.Fail(string.Concat(new object[]
						{
							"Cannot find enum name ",
							text,
							" on type ",
							storageType
						}));
					}
					long num2 = (long)Convert.ChangeType(Enum.Parse(storageType, text), typeof(long));
					num |= num2;
				}
				instance = Enum.ToObject(storageType, num);
				return fsResult.Success;
			}
			if (data.IsInt64)
			{
				int num3 = (int)data.AsInt64;
				instance = Enum.ToObject(storageType, num3);
				return fsResult.Success;
			}
			return fsResult.Fail("EnumConverter encountered an unknown JSON data type");
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001B72C File Offset: 0x0001992C
		private static bool ArrayContains<T>(T[] values, T value)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (EqualityComparer<T>.Default.Equals(values[i], value))
				{
					return true;
				}
			}
			return false;
		}
	}
}
