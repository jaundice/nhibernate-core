using System;
using System.Collections.Generic;
using System.Text;

using Iesi.Collections.Generic;

namespace NHibernate.Test.GenericTest.SetGeneric
{
	public class A
	{
		private int? _id;
		private string _name;
		private Iesi.Collections.Generic.ISet<B> _items;

		public A() { }

		public int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public Iesi.Collections.Generic.ISet<B> Items
		{
			get { return _items; }
			set { _items = value; }
		}

	}
}
