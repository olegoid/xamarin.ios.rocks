﻿using System;

namespace Rocks
{
	internal static class TupleBuilder
	{
		public static Tuple<T1, T2> Tuple<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2> (item1, item2);
		}
	}
}
