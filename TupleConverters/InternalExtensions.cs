#if NET45 || NET461 || NET47 || NETSTANDARD2_0
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
#if NET45 || NET461 || NET47
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> Source, TSource element)
        {
            foreach (var s in Source)
                yield return s;
            yield return element;
        }
#endif
        internal static IEnumerable<object?> GetEnumerable(this object? Source)
        {
            if (Source is null)
                throw new ArgumentNullException($"{nameof(Source)} is null", nameof(Source));

            var Type = Source.GetType();

#if ITUPLE_NOTSUPPORT
            if (Type.IsValueTuple())
                return Source.GetValueTupleEnumerable();
            if (Type.IsTuple())
                return Source.GetTupleEnumerable();
#else
            if (Source is ITuple Tuple)
                return Tuple.GetEnumerable();
#endif
            throw new ArgumentException($"{nameof(Source)} is not Tuple Type ( Tuple<> or ValueTuple<>)", nameof(Source));
        }
#if ITUPLE_NOTSUPPORT
        static IEnumerable<object?> GetValueTupleEnumerable(this object? Values)
        {
            if (Values is null)
                throw new ArgumentNullException($"{nameof(Values)} is null", nameof(Values));
            return GetEnumerable(Values.GetType(), Values);
            static IEnumerable<object?> GetEnumerable(Type Type, object? Source)
            {
                var GenericTypeDefinition = Type.GetGenericTypeDefinition();
                if (GenericTypeDefinition == typeof(ValueTuple<>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int>.Item1)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int>.Item2)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int>.Item3)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,,,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item4)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,,,,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item5)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,,,,,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item6)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,,,,,,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(ValueTuple<,,,,,,,>))
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                    var Rest = Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Rest)).GetValue(Source);
                    foreach (var r in GetEnumerable(Rest.GetType(), Rest))
                        yield return r;
                }
            }
        }
        static IEnumerable<object?> GetTupleEnumerable(this object? Values)
        {
            if (Values is null)
                throw new ArgumentNullException($"{nameof(Values)} is null", nameof(Values));
            return GetEnumerable(Values.GetType(), Values);
            static IEnumerable<object?> GetEnumerable(Type Type, object? Source)
            {
                var GenericTypeDefinition = Type.GetGenericTypeDefinition();
                if (GenericTypeDefinition == typeof(Tuple<>))
                {
                    yield return Type.GetField(nameof(Tuple<int>.Item1)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int>.Item2)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int>.Item3)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,,,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int>.Item4)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,,,,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int>.Item5)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,,,,,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int>.Item6)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,,,,,,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                }
                else if (GenericTypeDefinition == typeof(Tuple<,,,,,,,>))
                {
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                    var Rest = Type.GetField(nameof(Tuple<int, int, int, int, int, int, int, int>.Rest)).GetValue(Source);
                    foreach (var r in GetEnumerable(Rest.GetType(), Rest))
                        yield return r;
                }
            }
        }
#else
        public static IEnumerable<object?> GetEnumerable(this ITuple Source)
        {
            for (var i = 0; i < Source.Length; i++)
                yield return Source[i];
        }
#endif
    }
}
