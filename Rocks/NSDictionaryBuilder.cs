using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Foundation;
using ObjCRuntime;

namespace Rocks
{
	public class NSDictionaryBuilder<TKey, TValue> : IEnumerable where TKey : class, INativeObject where TValue : class, INativeObject
	{
		readonly List<KeyValuePair<TKey, TValue>> items = new List<KeyValuePair<TKey, TValue>> ();

		public void Add (object key, object value)
		{
			var nKey = (TKey)(INativeObject)NSObject.FromObject (key);
			var nValue = (TValue)(INativeObject)NSObject.FromObject (value);
			items.Add (new KeyValuePair<TKey, TValue> (nKey, nValue));
		}

		public void Add (TKey key, TValue value)
		{
			items.Add (new KeyValuePair<TKey, TValue> (key, value));
		}

		public IEnumerator GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		public NSDictionary<TKey, TValue> ToDictionary ()
		{
			var keys = items.Select (kvp => kvp.Key).ToArray ();
			var values = items.Select (kvp => kvp.Value).ToArray ();

			// TODO: https://bugzilla.xamarin.com/show_bug.cgi?id=41343
			return NSDictionary<TKey, TValue>.FromObjectsAndKeys (values, keys);
		}
	}
}