using System;
using System.Collections.Generic;
using System.Reflection;

namespace FullSerializer.Internal
{
	// Token: 0x020000A4 RID: 164
	public static class fsTypeCache
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0001E29C File Offset: 0x0001C49C
		static fsTypeCache()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				Assembly assembly = assemblies[i];
				fsTypeCache._assembliesByName[assembly.FullName] = assembly;
				fsTypeCache._assembliesByIndex.Add(assembly);
			}
			fsTypeCache._cachedTypes = new Dictionary<string, Type>();
			AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(fsTypeCache.OnAssemblyLoaded);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001E328 File Offset: 0x0001C528
		private static void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
		{
			fsTypeCache._assembliesByName[args.LoadedAssembly.FullName] = args.LoadedAssembly;
			fsTypeCache._assembliesByIndex.Add(args.LoadedAssembly);
			fsTypeCache._cachedTypes = new Dictionary<string, Type>();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001E36C File Offset: 0x0001C56C
		private static bool TryDirectTypeLookup(string assemblyName, string typeName, out Type type)
		{
			Assembly assembly;
			if (assemblyName != null && fsTypeCache._assembliesByName.TryGetValue(assemblyName, out assembly))
			{
				type = assembly.GetType(typeName, false);
				return type != null;
			}
			type = null;
			return false;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		private static bool TryIndirectTypeLookup(string typeName, out Type type)
		{
			for (int i = 0; i < fsTypeCache._assembliesByIndex.Count; i++)
			{
				Assembly assembly = fsTypeCache._assembliesByIndex[i];
				type = assembly.GetType(typeName);
				if (type != null)
				{
					return true;
				}
				Type[] types = assembly.GetTypes();
				for (int j = 0; j < types.Length; j++)
				{
					Type type2 = types[j];
					if (type2.FullName == typeName)
					{
						type = type2;
						return true;
					}
				}
			}
			type = null;
			return false;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001E42C File Offset: 0x0001C62C
		public static void Reset()
		{
			fsTypeCache._cachedTypes = new Dictionary<string, Type>();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001E438 File Offset: 0x0001C638
		public static Type GetType(string name)
		{
			return fsTypeCache.GetType(name, null);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001E444 File Offset: 0x0001C644
		public static Type GetType(string name, string assemblyHint)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			Type type;
			if (!fsTypeCache._cachedTypes.TryGetValue(name, out type))
			{
				if (fsTypeCache.TryDirectTypeLookup(assemblyHint, name, out type) || !fsTypeCache.TryIndirectTypeLookup(name, out type))
				{
				}
				fsTypeCache._cachedTypes[name] = type;
			}
			return type;
		}

		// Token: 0x040003BB RID: 955
		private static Dictionary<string, Type> _cachedTypes = new Dictionary<string, Type>();

		// Token: 0x040003BC RID: 956
		private static Dictionary<string, Assembly> _assembliesByName = new Dictionary<string, Assembly>();

		// Token: 0x040003BD RID: 957
		private static List<Assembly> _assembliesByIndex = new List<Assembly>();
	}
}
