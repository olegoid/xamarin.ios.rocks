using System;

using Rocks;
using NUnit.Framework;
using Foundation;

namespace Tests
{
	[TestFixture]
	public class NSDictionaryBuilderFixture
	{
		[Test]
		public void StringKeyStringValue ()
		{
			var dict = new NSDictionaryBuilder<NSString, NSString> {
				{ "key1", "value1" },
				{ "key2", "value2" },
				{ "key3", "value3" }
			}.ToDictionary ();

			NSString value;

			Assert.True (dict.TryGetValue ((NSString)"key1", out value));
			Assert.True (value == "value1");

			Assert.True (dict.TryGetValue ((NSString)"key2", out value));
			Assert.True (value == "value2");

			Assert.True (dict.TryGetValue ((NSString)"key3", out value));
			Assert.True (value == "value3");
		}

		[Test]
		public void IntKeyIntValue ()
		{
			var dict = new NSDictionaryBuilder<NSNumber, NSNumber> {
				{ 1, 10 },
				{ 2, 20 },
				{ 3, 30 }
			}.ToDictionary ();

			NSNumber value;

			Assert.True (dict.TryGetValue (new NSNumber(1), out value));
			Assert.True (value.Int32Value == 10);

			Assert.True (dict.TryGetValue (new NSNumber (2), out value));
			Assert.True (value.Int32Value == 20);

			Assert.True (dict.TryGetValue (new NSNumber (3), out value));
			Assert.True (value.Int32Value == 30);
		}

		[Test]
		public void StringKeyDictionaryValue ()
		{
			var dict1 = new NSDictionaryBuilder<NSString, NSString> {
				{ "key1", "value1" },
				{ "key2", "value2" }
			}.ToDictionary ();

			var dict2 = new NSDictionaryBuilder<NSString, NSString> {
				{ "key10", "value10" },
				{ "key20", "value20" }
			}.ToDictionary ();

			var result = new NSDictionaryBuilder<NSString, NSDictionary<NSString, NSString>> {
				{ "dict1", dict1 },
				{ "dict2", dict2 }
			}.ToDictionary ();

			var resultDict1 = result [(NSString)"dict1"];
			Assert.True (ReferenceEquals (dict1, resultDict1));

			var resultDict2 = result [(NSString)"dict2"];
			Assert.True (ReferenceEquals (dict2, resultDict2));
		}
	}
}
