using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace FullSerializer.Internal
{
	// Token: 0x0200008F RID: 143
	public class fsIEnumerableConverter : fsConverter
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x0001B998 File Offset: 0x00019B98
		public override bool CanProcess(Type type)
		{
			return typeof(IEnumerable).IsAssignableFrom(type) && fsIEnumerableConverter.GetAddMethod(type) != null;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001B9C0 File Offset: 0x00019BC0
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		public override fsResult TrySerialize(object instance_, out fsData serialized, Type storageType)
		{
			IEnumerable enumerable = (IEnumerable)instance_;
			fsResult success = fsResult.Success;
			Type elementType = fsIEnumerableConverter.GetElementType(storageType);
			serialized = fsData.CreateList(fsIEnumerableConverter.HintSize(enumerable));
			List<fsData> asList = serialized.AsList;
			foreach (object current in enumerable)
			{
				fsData item;
				fsResult result = this.Serializer.TrySerialize(elementType, current, out item);
				success.AddMessages(result);
				if (!result.Failed)
				{
					asList.Add(item);
				}
			}
			if (this.IsStack(enumerable.GetType()))
			{
				asList.Reverse();
			}
			return success;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		private bool IsStack(Type type)
		{
			return type.Resolve().IsGenericType && type.Resolve().GetGenericTypeDefinition() == typeof(Stack<>);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001BAEC File Offset: 0x00019CEC
		public override fsResult TryDeserialize(fsData data, ref object instance_, Type storageType)
		{
			IEnumerable enumerable = (IEnumerable)instance_;
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Array));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			Type elementType = fsIEnumerableConverter.GetElementType(storageType);
			MethodInfo addMethod = fsIEnumerableConverter.GetAddMethod(storageType);
			MethodInfo flattenedMethod = storageType.GetFlattenedMethod("get_Item");
			MethodInfo flattenedMethod2 = storageType.GetFlattenedMethod("set_Item");
			if (flattenedMethod2 == null)
			{
				fsIEnumerableConverter.TryClear(storageType, enumerable);
			}
			int num = fsIEnumerableConverter.TryGetExistingSize(storageType, enumerable);
			List<fsData> asList = data.AsList;
			for (int i = 0; i < asList.Count; i++)
			{
				fsData data2 = asList[i];
				object obj = null;
				if (flattenedMethod != null && i < num)
				{
					obj = flattenedMethod.Invoke(enumerable, new object[]
					{
						i
					});
				}
				fsResult result = this.Serializer.TryDeserialize(data2, elementType, ref obj);
				fsResult.AddMessages(result);
				if (!result.Failed)
				{
					if (flattenedMethod2 != null && i < num)
					{
						flattenedMethod2.Invoke(enumerable, new object[]
						{
							i,
							obj
						});
					}
					else
					{
						addMethod.Invoke(enumerable, new object[]
						{
							obj
						});
					}
				}
			}
			return fsResult;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001BC38 File Offset: 0x00019E38
		private static int HintSize(IEnumerable collection)
		{
			if (collection is ICollection)
			{
				return ((ICollection)collection).Count;
			}
			return 0;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001BC54 File Offset: 0x00019E54
		private static Type GetElementType(Type objectType)
		{
			if (objectType.HasElementType)
			{
				return objectType.GetElementType();
			}
			Type @interface = fsReflectionUtility.GetInterface(objectType, typeof(IEnumerable<>));
			if (@interface != null)
			{
				return @interface.GetGenericArguments()[0];
			}
			return typeof(object);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		private static void TryClear(Type type, object instance)
		{
			MethodInfo flattenedMethod = type.GetFlattenedMethod("Clear");
			if (flattenedMethod != null)
			{
				flattenedMethod.Invoke(instance, null);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		private static int TryGetExistingSize(Type type, object instance)
		{
			PropertyInfo flattenedProperty = type.GetFlattenedProperty("Count");
			if (flattenedProperty != null)
			{
				return (int)flattenedProperty.GetGetMethod().Invoke(instance, null);
			}
			return 0;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001BCFC File Offset: 0x00019EFC
		private static MethodInfo GetAddMethod(Type type)
		{
			Type @interface = fsReflectionUtility.GetInterface(type, typeof(ICollection<>));
			if (@interface != null)
			{
				MethodInfo declaredMethod = @interface.GetDeclaredMethod("Add");
				if (declaredMethod != null)
				{
					return declaredMethod;
				}
			}
			MethodInfo arg_5A_0;
			if ((arg_5A_0 = type.GetFlattenedMethod("Add")) == null)
			{
				arg_5A_0 = (type.GetFlattenedMethod("Push") ?? type.GetFlattenedMethod("Enqueue"));
			}
			return arg_5A_0;
		}
	}
}
