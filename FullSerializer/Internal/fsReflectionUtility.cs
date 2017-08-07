using System;

namespace FullSerializer.Internal
{
	// Token: 0x020000A3 RID: 163
	public static class fsReflectionUtility
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0001E200 File Offset: 0x0001C400
		public static Type GetInterface(Type type, Type interfaceType)
		{
			if (interfaceType.Resolve().IsGenericType && !interfaceType.Resolve().IsGenericTypeDefinition)
			{
				throw new ArgumentException("GetInterface requires that if the interface type is generic, then it must be the generic type definition, not a specific generic type instantiation");
			}
			while (type != null)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					Type type2 = interfaces[i];
					if (type2.Resolve().IsGenericType)
					{
						if (interfaceType == type2.GetGenericTypeDefinition())
						{
							return type2;
						}
					}
					else if (interfaceType == type2)
					{
						return type2;
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}
	}
}
