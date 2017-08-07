using System;
using System.Collections.Generic;
using System.Linq;

namespace FullSerializer
{
	// Token: 0x0200009E RID: 158
	public static class fsTypeExtensions
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
		public static string CSharpName(this Type type)
		{
			return type.CSharpName(false);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001D1AC File Offset: 0x0001B3AC
		public static string CSharpName(this Type type, bool includeNamespace, bool ensureSafeDeclarationName)
		{
			string text = type.CSharpName(includeNamespace);
			if (ensureSafeDeclarationName)
			{
				text = text.Replace('>', '_').Replace('<', '_').Replace('.', '_');
			}
			return text;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		public static string CSharpName(this Type type, bool includeNamespace)
		{
			if (type == typeof(void))
			{
				return "void";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type.IsGenericParameter)
			{
				return type.ToString();
			}
			string text = string.Empty;
			IEnumerable<Type> source = type.GetGenericArguments();
			if (type.IsNested)
			{
				text = text + type.DeclaringType.CSharpName() + ".";
				if (type.DeclaringType.GetGenericArguments().Length > 0)
				{
					source = source.Skip(type.DeclaringType.GetGenericArguments().Length);
				}
			}
			if (!source.Any<Type>())
			{
				text += type.Name;
			}
			else
			{
				text += type.Name.Substring(0, type.Name.IndexOf('`'));
				text = text + "<" + string.Join(",", (from t in source
				select t.CSharpName(includeNamespace)).ToArray<string>()) + ">";
			}
			if (includeNamespace && type.Namespace != null)
			{
				text = type.Namespace + "." + text;
			}
			return text;
		}
	}
}
