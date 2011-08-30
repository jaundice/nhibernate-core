/* Copyright © 2002-2004 by Aidant Systems, Inc., and by Jason Smith. */
using System;
using System.Collections;

namespace Iesi.Collections
{
	/// <summary>
	/// Implements a <c>Set</c> based on a hash table.  This will give the best lookup, add, and remove
	/// performance for very large data-sets, but iteration will occur in no particular order.
	/// </summary>
#if!SILVERLIGHT
	[Serializable]
#endif
    public class HashedSet : DictionarySet
	{
		/// <summary>
		/// Creates a new set instance based on a hash table.
		/// </summary>
		public HashedSet()
		{
#if!SILVERLIGHT
            InternalDictionary = new Hashtable();
#else
            InternalDictionary =  new System.Collections.Generic.Dictionary<object, object>();
#endif
        }

		/// <summary>
		/// Creates a new set instance based on a hash table and
		/// initializes it based on a collection of elements.
		/// </summary>
		/// <param name="initialValues">A collection of elements that defines the initial set contents.</param>
		public HashedSet(ICollection initialValues) : this()
		{
			this.AddAll(initialValues);
		}
	}
}