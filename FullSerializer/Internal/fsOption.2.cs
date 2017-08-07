using System;

namespace FullSerializer.Internal
{
	// Token: 0x0200009A RID: 154
	public static class fsOption
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0001CD78 File Offset: 0x0001AF78
		public static fsOption<T> Just<T>(T value)
		{
			return new fsOption<T>(value);
		}
	}
}
