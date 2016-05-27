using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace Rocks
{
	public class KeyConfig
	{
		public ValueConfig<TKey> Key<TKey> (TKey key)
		{
			return new ValueConfig<TKey> (key);
		}
	}

	public class ValueConfig<TKey>
	{
		readonly TKey key;

		public ValueConfig (TKey key)
		{
			this.key = key;
		}

		public KeySetup<TKey, TValue> Value<TValue> (TValue value)
		{
			return new KeySetup<TKey, TValue> (new List<Tuple<TKey, TValue>> { new Tuple<TKey, TValue> (key, value) });
		}
	}

	public class KeySetup<TKey, TValue>
	{
		readonly List<Tuple<TKey, TValue>> list;

		internal KeySetup (List<Tuple<TKey, TValue>> list)
		{
			if (list == null)
				throw new ArgumentNullException (nameof (list));
			this.list = list;
		}

		public ValueSetup<TKey, TValue> Key (TKey key)
		{
			list.Add (new Tuple<TKey, TValue> (key, default (TValue)));
			return new ValueSetup<TKey, TValue> (list);
		}

		// TODO: should return generic dictionary
		public NSDictionary ToDictionary ()
		{
			var keys = list.Select (kvp => NSObject.FromObject (kvp.Item1)).ToArray ();
			var values = list.Select (kvp => NSObject.FromObject (kvp.Item2)).ToArray ();

			return NSDictionary.FromObjectsAndKeys (values, keys);
		}
	}

	public class ValueSetup<TKey, TValue>
	{
		readonly List<Tuple<TKey, TValue>> list;

		internal ValueSetup (List<Tuple<TKey, TValue>> list)
		{
			if (list == null)
				throw new ArgumentNullException (nameof (list));
			this.list = list;
		}

		public KeySetup<TKey, TValue> Value (TValue value)
		{
			var index = list.Count - 1;
			var tpl = list [index];
			list [index] = new Tuple<TKey, TValue> (tpl.Item1, value);

			return new KeySetup<TKey, TValue> (list);
		}
	}

	public static class NSDictionaryFluentBuilder
	{
		public static KeyConfig NSDictionary ()
		{
			return new KeyConfig ();
		}
	}
}