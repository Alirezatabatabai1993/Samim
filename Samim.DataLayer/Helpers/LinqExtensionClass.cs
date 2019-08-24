using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samim.DataLayer.Helpers
{
	public static class LinqExtensionClass
	{
		public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, Func<T, IEnumerable<T>> func)
		{
			return collection.Concat(collection.SelectMany(x => func(x).Flatten(func)));
		}
	}
}
