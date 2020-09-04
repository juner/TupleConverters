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
    public static class TupleUtil
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
        public static
#if ITUPLE_NOTSUPPORT
            object
#else
            ITuple
#endif
            To(Type TupleType, IEnumerable<object?> Values)
        {
            if (!IsTuple(TupleType))
                throw new ArgumentException($"TupleType is not Tuple Type. : {TupleType}", nameof(TupleType));
            var GenericArguments = TupleType.GetGenericArguments();
            var Constructor = TupleType.GetConstructor(GenericArguments)!;
            var Parameter = Values.Take(7);
            if (GenericArguments.Length == 8)
                Parameter = Parameter.Append(To(GenericArguments[8], Values.Skip(7)));
            else if (GenericArguments.Length != (Values is object?[] _Values ? _Values.Length : Values.Count()))
                throw new ArgumentException($"Type Argument length:{GenericArguments.Length} is not Value length:{Values.Count()}", nameof(Values));
            return
#if ITUPLE_NOTSUPPORT
#else
            (ITuple)
#endif
            Constructor.Invoke(Parameter.ToArray());
        }
        static Type MakeTupleType(IEnumerable<Type> Types) => (Types is Type[] __Types ? __Types.Length : Types.Count()) switch
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
        static Type MakeValueTupleType(IEnumerable<Type> Types) => (Types is Type[] __Types ? __Types.Length : Types.Count()) switch
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
        public static object?[] From(object?[] Source) => Source.GetEnumerable().ToArray();
    }
}
