#if NET45 || NET461 || NET47 || NETSTANDARD2_0
#define ITUPLE_NOTSUPPORT
#endif
using System;
using System.Collections.Generic;
using System.Linq;
#if !ITUPLE_NOTSUPPORT
using System.Runtime.CompilerServices;
#endif
namespace TupleConverters
{
    public static class TupleUtility
    {
        /// <summary>
        /// オブジェクトの配列を元に規定の型に変換する
        /// </summary>
        /// <typeparam name="Tuple"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static
#if ITUPLE_NOTSUPPORT
            object
#else
            Tuple
#endif
            To<Tuple>(params object?[] Value)
            where Tuple : class
#if !ITUPLE_NOTSUPPORT
            , ITuple
#endif
        {
            return (Tuple)To(typeof(Tuple), Value);
        }
        public static object To(Type TupleType, IEnumerable<object?> Values, bool NoThrow = false)
        {
            var GenericArguments = TupleType.GetGenericArguments();
            var Constructor = TupleType.GetConstructor(GenericArguments)!;
            var Parameter = Values.Take(7);
            if (GenericArguments.Length == 8)
                Parameter = Parameter.Append(To(GenericArguments[7], Values.Skip(7)));
            else if (!NoThrow && GenericArguments.Length != (Values is object?[] _Values ? _Values.Length : Values.Count()))
                throw new ArgumentException($"Type Argument length:{GenericArguments.Length} is not Value length:{Values.Count()}", nameof(Values));
            return
#if ITUPLE_NOTSUPPORT
#else
            (ITuple)
#endif
            Constructor.Invoke(Parameter.ToArray());
        }
        public static Type MakeTupleType(IEnumerable<Type> Types) => (Types is Type[] __Types ? __Types.Length : Types.Count()) switch
        {
            0 => throw new ArgumentOutOfRangeException(nameof(Types) + " is 1 or later length support.", nameof(Types)),
            1 => typeof(Tuple<>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            2 => typeof(Tuple<,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            3 => typeof(Tuple<,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            4 => typeof(Tuple<,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            5 => typeof(Tuple<,,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            6 => typeof(Tuple<,,,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            7 => typeof(Tuple<,,,,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            _ => typeof(Tuple<,,,,,,,>).MakeGenericType(Types.Take(7).Append(MakeTupleType(Types.Skip(7))).ToArray()),
        };
        public static Type MakeValueTupleType(IEnumerable<Type> Types) => (Types is Type[] __Types ? __Types.Length : Types.Count()) switch
        {
            0 => throw new ArgumentOutOfRangeException(nameof(Types) + " is 1 or later length support.", nameof(Types)),
            1 => typeof(ValueTuple<>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            2 => typeof(ValueTuple<,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            3 => typeof(ValueTuple<,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            4 => typeof(ValueTuple<,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            5 => typeof(ValueTuple<,,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            6 => typeof(ValueTuple<,,,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            7 => typeof(ValueTuple<,,,,,,>).MakeGenericType(Types is Type[] _Types ? _Types : Types.ToArray()),
            _ => typeof(ValueTuple<,,,,,,,>).MakeGenericType(Types.Take(7).Append(MakeValueTupleType(Types.Skip(7))).ToArray()),
        };
        public static bool IsTuple(this Type Tuple)
        {
            if (!Tuple.IsGenericType)
                return false;
            if (!Tuple.IsClass)
                return false;
#if ITUPLE_NOTSUPPORT
            var OpenType = Tuple.GetGenericTypeDefinition();
            return OpenType == typeof(Tuple<>)
                || OpenType == typeof(Tuple<,>)
                || OpenType == typeof(Tuple<,,>)
                || OpenType == typeof(Tuple<,,,>)
                || OpenType == typeof(Tuple<,,,,>)
                || OpenType == typeof(Tuple<,,,,,>)
                || OpenType == typeof(Tuple<,,,,,,>)
                || (OpenType == typeof(Tuple<,,,,,,,>) && IsTuple(Tuple.GetGenericArguments()[7]));
#else
            return typeof(ITuple).IsAssignableFrom(Tuple);
#endif
        }
        public static bool IsValueTuple(this Type Tuple)
        {
            if (!Tuple.IsGenericType)
                return false;
            if (!Tuple.IsValueType)
                return false;
#if ITUPLE_NOTSUPPORT
            var OpenType = Tuple.GetGenericTypeDefinition();
            return OpenType == typeof(ValueTuple<>)
                || OpenType == typeof(ValueTuple<,>)
                || OpenType == typeof(ValueTuple<,,>)
                || OpenType == typeof(ValueTuple<,,,>)
                || OpenType == typeof(ValueTuple<,,,,>)
                || OpenType == typeof(ValueTuple<,,,,,>)
                || OpenType == typeof(ValueTuple<,,,,,,>)
                || (OpenType == typeof(ValueTuple<,,,,,,,>) && IsTuple(Tuple.GetGenericArguments()[7]));
#else
            return typeof(ITuple).IsAssignableFrom(Tuple);
#endif
        }
        public static object?[] From(object? Source) => Source.GetEnumerable().ToArray();
        internal static IEnumerable<object?> GetEnumerable(this object? Source)
        {
            if (Source is null)
                throw new ArgumentNullException($"{nameof(Source)} is null", nameof(Source));

            var Type = Source.GetType();

#if ITUPLE_NOTSUPPORT
            if (Type.IsValueType)
                return Source.GetValueTupleEnumerable();
            if (Type.IsClass)
                return Source.GetTupleEnumerable();
#else
            if (Source is ITuple Tuple)
                return Tuple.GetEnumerable();
#endif
            throw new NotSupportedException($"{nameof(Source)} is not Tuple Type ( Tuple<> or ValueTuple<>)");
        }
#if ITUPLE_NOTSUPPORT
        internal static IEnumerable<object?> GetValueTupleEnumerable(this object? Values)
        {
            if (Values is null)
                throw new ArgumentNullException($"{nameof(Values)} is null", nameof(Values));
            return GetEnumerable(Values.GetType(), Values);
            static IEnumerable<object?> GetEnumerable(Type Type, object? Source)
            {
                var GenericArguments = Type.GetGenericArguments().Length;
                if (GenericArguments == 1)
                {
                    yield return Type.GetField(nameof(ValueTuple<int>.Item1)).GetValue(Source);
                }
                else if (GenericArguments == 2)
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int>.Item2)).GetValue(Source);
                }
                else if (GenericArguments == 3)
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int>.Item3)).GetValue(Source);
                }
                else if (GenericArguments == 4)
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int>.Item4)).GetValue(Source);
                }
                else if (GenericArguments == 5)
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int>.Item5)).GetValue(Source);
                }
                else if (GenericArguments == 6)
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int>.Item6)).GetValue(Source);
                }
                else if (GenericArguments == 7)
                {
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                }
                else if (GenericArguments == 8)
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
                yield break;
            }
        }
        internal static IEnumerable<object?> GetTupleEnumerable(this object? Values)
        {
            if (Values is null)
                throw new ArgumentNullException($"{nameof(Values)} is null", nameof(Values));
            return GetEnumerable(Values.GetType(), Values);
            static IEnumerable<object?> GetEnumerable(Type Type, object? Source)
            {
                var GenericArguments = Type.GetGenericArguments().Length;
                if (GenericArguments == 1)
                {
                    yield return Type.GetProperty(nameof(Tuple<int>.Item1)).GetValue(Source);
                }
                else if (GenericArguments == 2)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int>.Item2)).GetValue(Source);
                }
                else if (GenericArguments == 3)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int>.Item3)).GetValue(Source);
                }
                else if (GenericArguments == 4)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int>.Item4)).GetValue(Source);
                }
                else if (GenericArguments == 5)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int>.Item5)).GetValue(Source);
                }
                else if (GenericArguments == 6)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int>.Item6)).GetValue(Source);
                }
                else if (GenericArguments == 7)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                }
                else if (GenericArguments == 8)
                {
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                    var Rest = Type.GetProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Rest)).GetValue(Source);
                    foreach (var r in GetEnumerable(Rest.GetType(), Rest))
                        yield return r;
                }
                yield break;
            }
        }
#else
        internal static IEnumerable<object?> GetEnumerable(this ITuple Source)
        {
            for (var i = 0; i < Source.Length; i++)
                yield return Source[i];
        }
#endif
    }
}
