using System;

namespace FullSerializer
{
	// Token: 0x0200008C RID: 140
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class fsForwardAttribute : Attribute
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0001B768 File Offset: 0x00019968
		public fsForwardAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		// Token: 0x04000396 RID: 918
		public string MemberName;
	}
}
