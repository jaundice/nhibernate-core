using System;
using System.Collections;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using NUnit.Framework;

namespace Iesi.Collections.Test.Generic
{
	/// <summary>
	/// Summary description for SortedSetFixture.
	/// </summary>
	[TestFixture]
	public class SortedSetFixture : GenericSetFixture
	{
		protected override Collections.Generic.ISet<string> CreateInstance()
		{
			return new Collections.Generic.SortedSet<string>();
		}

		protected override Collections.Generic.ISet<string> CreateInstance(ICollection<string> init)
		{
			return new Collections.Generic.SortedSet<string>(init);
		}

		protected override Type ExpectedType
		{
			get { return typeof(Collections.Generic.SortedSet<string>); }
		}

		[Test]
		public void OrderedEnumeration()
		{
			List<string> expectedOrder = new List<string>(3);
			expectedOrder.Add(one);
			expectedOrder.Add(two);
			expectedOrder.Add(three);
			expectedOrder.Sort();

			int index = 0;
			foreach (string str in _set)
			{
				Assert.AreEqual(str, expectedOrder[index], index.ToString() + " did not have same value");
				index++;
			}
		}

		[Test]
		public void OrderedCaseInsensitiveEnumeration()
		{
			ArrayList expectedOrder = new ArrayList(3);
			expectedOrder.Add("ONE");
			expectedOrder.Add("two");
			expectedOrder.Add("tHree");

			Collections.Generic.SortedSet<string> theSet = new Collections.Generic.SortedSet<string>(StringComparer.CurrentCultureIgnoreCase);
			foreach (string str in expectedOrder)
				theSet.Add(str);

			expectedOrder.Sort(StringComparer.CurrentCultureIgnoreCase);

			int index = 0;
			foreach (string str in theSet)
			{
				Assert.AreEqual(str, expectedOrder[index], index.ToString() + " did not have same value");
				index++;
			}
		}
	}
}