using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using FullSerializer.Internal;
using UnityEngine;

namespace FullSerializer
{
	// Token: 0x020000A2 RID: 162
	public class fsMetaType
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0001DA94 File Offset: 0x0001BC94
		private fsMetaType(fsConfig config, Type reflectedType)
		{
			this.ReflectedType = reflectedType;
			List<fsMetaProperty> list = new List<fsMetaProperty>();
			fsMetaType.CollectProperties(config, list, reflectedType);
			this.Properties = list.ToArray();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		public static fsMetaType Get(fsConfig config, Type type)
		{
			Dictionary<Type, fsMetaType> dictionary;
			if (!fsMetaType._configMetaTypes.TryGetValue(config, out dictionary))
			{
				Dictionary<Type, fsMetaType> dictionary2 = new Dictionary<Type, fsMetaType>();
				fsMetaType._configMetaTypes[config] = dictionary2;
				dictionary = dictionary2;
			}
			fsMetaType fsMetaType;
			if (!dictionary.TryGetValue(type, out fsMetaType))
			{
				fsMetaType = new fsMetaType(config, type);
				dictionary[type] = fsMetaType;
			}
			return fsMetaType;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001DB28 File Offset: 0x0001BD28
		public static Dictionary<Type, fsMetaType> GetAll(fsConfig config)
		{
			Dictionary<Type, fsMetaType> result;
			if (!fsMetaType._configMetaTypes.TryGetValue(config, out result))
			{
				Dictionary<Type, fsMetaType> dictionary = new Dictionary<Type, fsMetaType>();
				fsMetaType._configMetaTypes[config] = dictionary;
				result = dictionary;
			}
			return result;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		public static void ClearCache()
		{
			fsMetaType._configMetaTypes = new Dictionary<fsConfig, Dictionary<Type, fsMetaType>>();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001DB68 File Offset: 0x0001BD68
		private static void CollectProperties(fsConfig config, List<fsMetaProperty> properties, Type reflectedType)
		{
			bool flag = config.DefaultMemberSerialization == fsMemberSerialization.OptIn;
			bool flag2 = config.DefaultMemberSerialization == fsMemberSerialization.OptOut;
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(reflectedType);
			if (attribute != null)
			{
				flag = (attribute.MemberSerialization == fsMemberSerialization.OptIn);
				flag2 = (attribute.MemberSerialization == fsMemberSerialization.OptOut);
			}
			MemberInfo[] declaredMembers = reflectedType.GetDeclaredMembers();
			MemberInfo[] array = declaredMembers;
			MemberInfo member;
			for (int i = 0; i < array.Length; i++)
			{
				member = array[i];
				if (!config.IgnoreSerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
				{
					PropertyInfo propertyInfo = member as PropertyInfo;
					FieldInfo fieldInfo = member as FieldInfo;
					if (propertyInfo != null || fieldInfo != null)
					{
						if (propertyInfo == null || config.EnablePropertySerialization)
						{
							if (!flag || config.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
							{
								if (!flag2 || !config.IgnoreSerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
								{
									if (propertyInfo != null)
									{
										if (fsMetaType.CanSerializeProperty(config, propertyInfo, declaredMembers, flag2))
										{
											properties.Add(new fsMetaProperty(config, propertyInfo));
										}
									}
									else if (fieldInfo != null && fsMetaType.CanSerializeField(config, fieldInfo, flag2))
									{
										properties.Add(new fsMetaProperty(config, fieldInfo));
									}
								}
							}
						}
					}
				}
			}
			if (reflectedType.Resolve().BaseType != null)
			{
				fsMetaType.CollectProperties(config, properties, reflectedType.Resolve().BaseType);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001DD08 File Offset: 0x0001BF08
		private static bool IsAutoProperty(PropertyInfo property, MemberInfo[] members)
		{
			if (!property.CanWrite || !property.CanRead)
			{
				return false;
			}
			string b = "<" + property.Name + ">k__BackingField";
			for (int i = 0; i < members.Length; i++)
			{
				if (members[i].Name == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001DD70 File Offset: 0x0001BF70
		private static bool CanSerializeProperty(fsConfig config, PropertyInfo property, MemberInfo[] members, bool annotationFreeValue)
		{
			if (typeof(Delegate).IsAssignableFrom(property.PropertyType))
			{
				return false;
			}
			MethodInfo getMethod = property.GetGetMethod(false);
			MethodInfo setMethod = property.GetSetMethod(false);
			return (getMethod == null || !getMethod.IsStatic) && (setMethod == null || !setMethod.IsStatic) && property.GetIndexParameters().Length <= 0 && (config.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(property, t)) || (property.CanRead && property.CanWrite && (((config.SerializeNonAutoProperties || fsMetaType.IsAutoProperty(property, members)) && getMethod != null && (config.SerializeNonPublicSetProperties || setMethod != null)) || annotationFreeValue)));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001DE74 File Offset: 0x0001C074
		private static bool CanSerializeField(fsConfig config, FieldInfo field, bool annotationFreeValue)
		{
			return !typeof(Delegate).IsAssignableFrom(field.FieldType) && !field.IsDefined(typeof(CompilerGeneratedAttribute), false) && !field.IsStatic && (config.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(field, t)) || annotationFreeValue || field.IsPublic);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001DF18 File Offset: 0x0001C118
		public bool EmitAotData()
		{
			if (this._hasEmittedAotData)
			{
				return false;
			}
			this._hasEmittedAotData = true;
			for (int i = 0; i < this.Properties.Length; i++)
			{
				if (!this.Properties[i].IsPublic)
				{
					return false;
				}
				if (this.Properties[i].IsReadOnly)
				{
					return false;
				}
			}
			if (!this.HasDefaultConstructor)
			{
				return false;
			}
			fsAotCompilationManager.AddAotCompilation(this.ReflectedType, this.Properties, this._isDefaultConstructorPublic);
			return true;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
		public fsMetaProperty[] Properties
		{
			get;
			private set;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		public bool HasDefaultConstructor
		{
			get
			{
				if (!this._hasDefaultConstructorCache.HasValue)
				{
					if (this.ReflectedType.Resolve().IsArray)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else if (this.ReflectedType.Resolve().IsValueType)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else
					{
						ConstructorInfo declaredConstructor = this.ReflectedType.GetDeclaredConstructor(fsPortableReflection.EmptyTypes);
						this._hasDefaultConstructorCache = new bool?(declaredConstructor != null);
						if (declaredConstructor != null)
						{
							this._isDefaultConstructorPublic = declaredConstructor.IsPublic;
						}
					}
				}
				return this._hasDefaultConstructorCache.Value;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001E06C File Offset: 0x0001C26C
		public object CreateInstance()
		{
			if (this.ReflectedType.Resolve().IsInterface || this.ReflectedType.Resolve().IsAbstract)
			{
				throw new Exception("Cannot create an instance of an interface or abstract type for " + this.ReflectedType);
			}
			if (typeof(ScriptableObject).IsAssignableFrom(this.ReflectedType))
			{
				return ScriptableObject.CreateInstance(this.ReflectedType);
			}
			if (typeof(string) == this.ReflectedType)
			{
				return string.Empty;
			}
			if (!this.HasDefaultConstructor)
			{
				return FormatterServices.GetSafeUninitializedObject(this.ReflectedType);
			}
			if (this.ReflectedType.Resolve().IsArray)
			{
				return Array.CreateInstance(this.ReflectedType.GetElementType(), 0);
			}
			object result;
			try
			{
				result = Activator.CreateInstance(this.ReflectedType, true);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException("Unable to create instance of " + this.ReflectedType + "; there is no default constructor", innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new InvalidOperationException("Constructor of " + this.ReflectedType + " threw an exception when creating an instance", innerException2);
			}
			catch (MemberAccessException innerException3)
			{
				throw new InvalidOperationException("Unable to access constructor of " + this.ReflectedType, innerException3);
			}
			return result;
		}

		// Token: 0x040003B5 RID: 949
		private static Dictionary<fsConfig, Dictionary<Type, fsMetaType>> _configMetaTypes = new Dictionary<fsConfig, Dictionary<Type, fsMetaType>>();

		// Token: 0x040003B6 RID: 950
		public Type ReflectedType;

		// Token: 0x040003B7 RID: 951
		private bool _hasEmittedAotData;

		// Token: 0x040003B8 RID: 952
		private bool? _hasDefaultConstructorCache;

		// Token: 0x040003B9 RID: 953
		private bool _isDefaultConstructorPublic;
	}
}
