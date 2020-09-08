#if NESTANDARD1_0 || NETSTANDARD2_0
#define ITUPLE_NOTSUPPORT
#endif
using System;
using System.Collections.Generic;
#if !ITUPLE_NOTSUPPORT
using System.Runtime.CompilerServices;
#endif

namespace TupleConverters
{
    internal static class InternalExtensions
    {
#if NETSTANDARD1_0
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> Source, TSource element)
        {
            foreach (var s in Source)
                yield return s;
            yield return element;
        }
#endif
    }
}