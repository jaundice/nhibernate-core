/* Copyright � 2002-2004 by Aidant Systems, Inc., and by Jason Smith. */
using System;
using System.Collections;
#if !SILVERLIGHT
using System.Collections.Specialized;
#endif
namespace Iesi.Collections
{
	/// <summary>
	/// Implements a <c>Set</c> based on a list.  Performance is much better for very small lists 
	/// than either <c>HashedSet</c> or <c>SortedSet</c>.  However, performance degrades rapidly as 
	/// the data-set gets bigger.  Use a <c>HybridSet</c> instead if you are not sure your data-set
	/// will always remain very small.  Iteration produces elements in the order they were added.
	/// However, element order is not guaranteed to be maintained by the various <c>Set</c>
	/// mathematical operators.  
	/// </summary>
#if !SILVERLIGHT
	[Serializable]
#endif
    public class ListSet : DictionarySet
	{
		/// <summary>
		/// Creates a new set instance based on a list.
		/// </summary>
		public ListSet()
		{
		    InternalDictionary =
#if !SILVERLIGHT
                new ListDictionary();
#else
		        new System.Collections.Generic.Dictionary<object, object>();
#endif
		}

		/// <summary>
		/// Creates a new set instance based on a list and
		/// initializes it based on a collection of elements.
		/// </summary>
		/// <param name="initialValues">A collection of elements that defines the initial set contents.</param>
		public ListSet(ICollection initialValues) : this()
		{
			this.AddAll(initialValues);
		}
	}
}