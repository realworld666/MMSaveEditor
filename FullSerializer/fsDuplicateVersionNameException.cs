using System;

namespace FullSerializer
{
	// Token: 0x020000B1 RID: 177
	public sealed class fsDuplicateVersionNameException : Exception
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0001F36C File Offset: 0x0001D56C
		public fsDuplicateVersionNameException(Type typeA, Type typeB, string version) : base(string.Concat(new object[]
		{
			typeA,
			" and ",
			typeB,
			" have the same version string (",
			version,
			"); please change one of them."
		}))
		{
		}
	}
}
