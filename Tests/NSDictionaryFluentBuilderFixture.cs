using Foundation;

using NUnit.Framework;
using static Rocks.NSDictionaryFluentBuilder;

namespace Tests
{
	[TestFixture]
	public class NSDictionaryFluentBuilderFixture
	{
		[Test]
		public void StringKeyNumberValue ()
		{
			NSDictionary dict = NSDictionary ()
				.Key ("key1").Value (10)
				.Key ("key2").Value (20)
				.ToDictionary ();

			var num1 = (NSNumber)dict ["key1"];
			Assert.True (num1.Int32Value == 10);

			var num2 = (NSNumber)dict ["key2"];
			Assert.True (num2.Int32Value == 20);
		}

		[Test]
		public void StringKeyDictionaryValue ()
		{
			var dict1 = NSDictionary ()
				.Key ("key1").Value ("value1")
				.Key ("key2").Value ("value2")
				.ToDictionary ();

			var dict2 = NSDictionary ()
				.Key ("key10").Value ("value10")
				.Key ("key20").Value ("value20")
				.ToDictionary ();

			var result = NSDictionary ()
				.Key ("dict1").Value (dict1)
				.Key ("dict2").Value (dict2)
				.ToDictionary ();

			var resultDict1 = result [(NSString)"dict1"];
			Assert.True (ReferenceEquals (dict1, resultDict1));

			var resultDict2 = result [(NSString)"dict2"];
			Assert.True (ReferenceEquals (dict2, resultDict2));
		}
	}
}
