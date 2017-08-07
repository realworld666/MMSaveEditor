using System;
using System.Globalization;

namespace FullSerializer.Internal
{
	// Token: 0x02000089 RID: 137
	public class fsDateConverter : fsConverter
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001ACEC File Offset: 0x00018EEC
		private string DateTimeFormatString
		{
			get
			{
				return this.Serializer.Config.CustomDateTimeFormatString ?? "o";
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001AD0C File Offset: 0x00018F0C
		public override bool CanProcess(Type type)
		{
			return type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001AD4C File Offset: 0x00018F4C
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			if (instance is DateTime)
			{
				serialized = new fsData(((DateTime)instance).ToString(this.DateTimeFormatString));
				return fsResult.Success;
			}
			if (instance is DateTimeOffset)
			{
				serialized = new fsData(((DateTimeOffset)instance).ToString("o"));
				return fsResult.Success;
			}
			if (instance is TimeSpan)
			{
				serialized = new fsData(((TimeSpan)instance).ToString());
				return fsResult.Success;
			}
			throw new InvalidOperationException("FullSerializer Internal Error -- Unexpected serialization type");
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Date deserialization requires a string, not " + data.Type);
			}
			if (storageType == typeof(DateTime))
			{
				DateTime dateTime;
				if (DateTime.TryParse(data.AsString, null, DateTimeStyles.RoundtripKind, out dateTime))
				{
					instance = dateTime;
					return fsResult.Success;
				}
				if (fsGlobalConfig.AllowInternalExceptions)
				{
					try
					{
						instance = Convert.ToDateTime(data.AsString);
						fsResult result = fsResult.Success;
						return result;
					}
					catch (Exception ex)
					{
						fsResult result = fsResult.Fail(string.Concat(new object[]
						{
							"Unable to parse ",
							data.AsString,
							" into a DateTime; got exception ",
							ex
						}));
						return result;
					}
				}
				return fsResult.Fail("Unable to parse " + data.AsString + " into a DateTime");
			}
			else if (storageType == typeof(DateTimeOffset))
			{
				DateTimeOffset dateTimeOffset;
				if (DateTimeOffset.TryParse(data.AsString, null, DateTimeStyles.RoundtripKind, out dateTimeOffset))
				{
					instance = dateTimeOffset;
					return fsResult.Success;
				}
				return fsResult.Fail("Unable to parse " + data.AsString + " into a DateTimeOffset");
			}
			else
			{
				if (storageType != typeof(TimeSpan))
				{
					throw new InvalidOperationException("FullSerializer Internal Error -- Unexpected deserialization type");
				}
				TimeSpan timeSpan;
				if (TimeSpan.TryParse(data.AsString, out timeSpan))
				{
					instance = timeSpan;
					return fsResult.Success;
				}
				return fsResult.Fail("Unable to parse " + data.AsString + " into a TimeSpan");
			}
		}

		// Token: 0x04000394 RID: 916
		private const string DefaultDateTimeFormatString = "o";

		// Token: 0x04000395 RID: 917
		private const string DateTimeOffsetFormatString = "o";
	}
}
