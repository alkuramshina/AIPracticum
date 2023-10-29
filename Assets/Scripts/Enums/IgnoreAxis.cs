using System;

namespace Enums
{
	[Flags]
	public enum IgnoreAxis : byte
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4
	}
}