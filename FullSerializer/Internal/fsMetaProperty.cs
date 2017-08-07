using System;
using System.Reflection;

namespace FullSerializer.Internal
{
	// Token: 0x020000A1 RID: 161
	public class fsMetaProperty
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		internal fsMetaProperty(fsConfig config, FieldInfo field)
		{
			this._memberInfo = field;
			this.StorageType = field.FieldType;
			this.MemberName = field.Name;
			this.IsPublic = field.IsPublic;
			this.IsReadOnly = field.IsInitOnly;
			this.CanRead = true;
			this.CanWrite = true;
			this.CommonInitialize(config);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001D85C File Offset: 0x0001BA5C
		internal fsMetaProperty(fsConfig config, PropertyInfo property)
		{
			this._memberInfo = property;
			this.StorageType = property.PropertyType;
			this.MemberName = property.Name;
			this.IsPublic = (property.GetGetMethod() != null && property.GetGetMethod().IsPublic && property.GetSetMethod() != null && property.GetSetMethod().IsPublic);
			this.IsReadOnly = false;
			this.CanRead = property.CanRead;
			this.CanWrite = property.CanWrite;
			this.CommonInitialize(config);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		private void CommonInitialize(fsConfig config)
		{
			fsPropertyAttribute attribute = fsPortableReflection.GetAttribute<fsPropertyAttribute>(this._memberInfo);
			if (attribute != null)
			{
				this.JsonName = attribute.Name;
				this.OverrideConverterType = attribute.Converter;
			}
			if (string.IsNullOrEmpty(this.JsonName))
			{
				this.JsonName = config.GetJsonNameFromMemberName(this.MemberName, this._memberInfo);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0001D958 File Offset: 0x0001BB58
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0001D960 File Offset: 0x0001BB60
		public Type StorageType
		{
			get;
			private set;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0001D96C File Offset: 0x0001BB6C
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0001D974 File Offset: 0x0001BB74
		public Type OverrideConverterType
		{
			get;
			private set;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001D980 File Offset: 0x0001BB80
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0001D988 File Offset: 0x0001BB88
		public bool CanRead
		{
			get;
			private set;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001D994 File Offset: 0x0001BB94
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0001D99C File Offset: 0x0001BB9C
		public bool CanWrite
		{
			get;
			private set;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		public string JsonName
		{
			get;
			private set;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		public string MemberName
		{
			get;
			private set;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		public bool IsPublic
		{
			get;
			private set;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		public bool IsReadOnly
		{
			get;
			private set;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001D9F8 File Offset: 0x0001BBF8
		public void Write(object context, object value)
		{
			FieldInfo fieldInfo = this._memberInfo as FieldInfo;
			PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(context, value);
			}
			else if (propertyInfo != null)
			{
				MethodInfo setMethod = propertyInfo.GetSetMethod(true);
				if (setMethod != null)
				{
					setMethod.Invoke(context, new object[]
					{
						value
					});
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001DA58 File Offset: 0x0001BC58
		public object Read(object context)
		{
			if (this._memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this._memberInfo).GetValue(context, new object[0]);
			}
			return ((FieldInfo)this._memberInfo).GetValue(context);
		}

		// Token: 0x040003AC RID: 940
		private MemberInfo _memberInfo;
	}
}
