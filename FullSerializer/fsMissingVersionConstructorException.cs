using System;

namespace FullSerializer
{
	// Token: 0x020000B0 RID: 176
	public sealed class fsMissingVersionConstructorException : Exception
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0001F358 File Offset: 0x0001D558
		public fsMissingVersionConstructorException(Type versionedType, Type constructorType) : base(versionedType + " is missing a constructor for previous model type " + constructorType)
		{
		}
	}
}
