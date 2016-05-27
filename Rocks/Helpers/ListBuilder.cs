using System.Collections.Generic;

namespace Rocks
{
	static class ListBuilder
	{
		public static List<T> List<T> (params T [] items)
		{
			return new List<T> (items);
		}
	}
}