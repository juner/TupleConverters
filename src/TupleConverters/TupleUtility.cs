#if NETSTANDARD1_0 || NETSTANDARD2_0
#define ITUPLE_NOTSUPPORT
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public static object To(Type TupleType, IEnumerable<object?> Values)
        {
#if ITUPLE_NOTSUPPORT
            var Info = TupleType.GetTypeInfo();
            var GenericArguments = Info.GetGenericArguments();
            var Constructor = Info.DeclaredConstructors.First(v => v.IsPublic);
#else
            var GenericArguments = TupleType.GetGenericArguments();
            var Constructor = TupleType.GetConstructor(GenericArguments)!;
#endif
            var Parameter = Values.Take(7);
            if (GenericArguments.Length == 8)
                Parameter = Parameter.Append(To(GenericArguments[7], Values.Skip(7)));
            return
#if ITUPLE_NOTSUPPORT
#else
            (ITuple)
#endif
            Constructor.Invoke(Parameter.ToArray());
        }
        /// <summary>
        /// GetTypeArray from <see cref="TupleType"/> 
        /// </summary>
        /// <param name="TupleType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes(Type TupleType)
        {
            if (!TupleType.IsTuple() && !TupleType.IsValueTuple())
                yield break;
#if ITUPLE_NOTSUPPORT
            var Info = TupleType.GetTypeInfo();
            var GenericArguments = Info.GetGenericArguments();
#else
            var GenericArguments = TupleType.GetGenericArguments();
#endif
            foreach (var type in GenericArguments.Take(7))
                yield return type;
            if (GenericArguments.Length < 8)
                yield break;
            foreach (var type in GetTypes(GenericArguments[7]))
                yield return type;
        }
        public static Type MakeTupleType(IEnumerable<Type> Types) => (Types is Type[] __Types ? __Types.Length : Types.Take(8).Count()) switch
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
        public static Type MakeValueTupleType(IEnumerable<Type> Types) => (Types is Type[] __Types ? __Types.Length : Types.Take(8).Count()) switch
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
            var Info = Tuple.GetTypeInfo();
            if (!Info.IsGenericType)
                return false;
            if (!Info.IsClass)
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
                || (OpenType == typeof(Tuple<,,,,,,,>) && IsTuple(Info.GetGenericArguments()[7]));
#else
            return typeof(ITuple).IsAssignableFrom(Tuple);
#endif
        }
        public static bool IsValueTuple(this Type Tuple)
        {
            var Info = Tuple.GetTypeInfo();
            if (!Info.IsGenericType)
                return false;
            if (!Info.IsValueType)
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
                || (OpenType == typeof(ValueTuple<,,,,,,,>) && IsTuple(Info.GetGenericArguments()[7]));
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

#if NETSTANDARD1_0
            var Info = Type.GetTypeInfo();
            if (Info.IsValueType)
                return Source.GetValueTupleEnumerable();
            if (Info.IsClass)
                return Source.GetTupleEnumerable();
#else
            if (Type.IsValueType)
                return Source.GetValueTupleEnumerable();
            if (Type.IsClass)
                return Source.GetTupleEnumerable();
#endif
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
                var Info = Type.GetTypeInfo();
                var GenericArguments = (Info.IsGenericTypeDefinition ? Info.GenericTypeParameters : Info.GenericTypeArguments).Length;

                if (GenericArguments == 1)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int>.Item1)).GetValue(Source);
                }
                else if (GenericArguments == 2)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int>.Item2)).GetValue(Source);
                }
                else if (GenericArguments == 3)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int>.Item3)).GetValue(Source);
                }
                else if (GenericArguments == 4)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int>.Item4)).GetValue(Source);
                }
                else if (GenericArguments == 5)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int>.Item5)).GetValue(Source);
                }
                else if (GenericArguments == 6)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int>.Item6)).GetValue(Source);
                }
                else if (GenericArguments == 7)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                }
                else if (GenericArguments == 8)
                {
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                    var Rest = Info.GetDeclaredField(nameof(ValueTuple<int, int, int, int, int, int, int, int>.Rest)).GetValue(Source);
                    foreach (var r in GetEnumerable(Rest.GetType(), Rest))
                        yield return r;
                }
                yield break;
            }
        }
        internal static Type[] GetGenericArguments(this TypeInfo Info) => Info.IsGenericTypeDefinition ? Info.GenericTypeParameters : Info.GenericTypeArguments;
        internal static IEnumerable<object?> GetTupleEnumerable(this object? Values)
        {
            if (Values is null)
                throw new ArgumentNullException($"{nameof(Values)} is null", nameof(Values));
            return GetEnumerable(Values.GetType(), Values);
            static IEnumerable<object?> GetEnumerable(Type Type, object? Source)
            {
                var Info = Type.GetTypeInfo();
                var GenericArguments = Info.GetGenericArguments().Length;
                if (GenericArguments == 1)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int>.Item1)).GetValue(Source);
                }
                else if (GenericArguments == 2)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int>.Item2)).GetValue(Source);
                }
                else if (GenericArguments == 3)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int>.Item3)).GetValue(Source);
                }
                else if (GenericArguments == 4)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int>.Item4)).GetValue(Source);
                }
                else if (GenericArguments == 5)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int>.Item5)).GetValue(Source);
                }
                else if (GenericArguments == 6)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int>.Item6)).GetValue(Source);
                }
                else if (GenericArguments == 7)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                }
                else if (GenericArguments == 8)
                {
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item1)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item2)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item3)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item4)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item5)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item6)).GetValue(Source);
                    yield return Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Item7)).GetValue(Source);
                    var Rest = Info.GetDeclaredProperty(nameof(Tuple<int, int, int, int, int, int, int, int>.Rest)).GetValue(Source);
                    foreach (var r in GetEnumerable(Rest.GetType(), Rest))
                        yield return r;
                }
                yield break;
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
