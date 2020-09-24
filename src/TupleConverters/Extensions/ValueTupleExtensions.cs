#if NETSTANDARD1_0 || NETSTANDARD2_0
#define ITUPLE_NOTSUPPORT
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if !ITUPLE_NOTSUPPORT
using System.Runtime.CompilerServices;
#endif
namespace TupleConverters.Extensions
{
    public static partial class ValueTupleExtensions
    {
        public static ValueTuple1Enumerable<T> GetEnumerable<T>(this ValueTuple<T> self) => new ValueTuple1Enumerable<T>(self);     
        public readonly struct ValueTuple1Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 1;
            readonly ValueTuple<T> Value;
            public ValueTuple1Enumerable(ValueTuple<T> Value) => this.Value = Value;
            public ValueTuple1Enumerator<T> GetEnumerator() => new ValueTuple1Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple1Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple1Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T> Value) => (Index,Value) switch {
                (0,var (_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple1Enumerator<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T> Value;
            public ValueTuple1Enumerator(ValueTuple<T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple1Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple1Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        internal class ValueTuple1Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T> Value;
            public ValueTuple1Enumerator2(ValueTuple<T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple1Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple1Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple2Enumerable<T> GetEnumerable<T>(this ValueTuple<T,T> self) => new ValueTuple2Enumerable<T>(self);     
        public readonly struct ValueTuple2Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 2;
            readonly ValueTuple<T,T> Value;
            public ValueTuple2Enumerable(ValueTuple<T,T> Value) => this.Value = Value;
            public ValueTuple2Enumerator<T> GetEnumerator() => new ValueTuple2Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple2Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple2Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T> Value) => (Index,Value) switch {
                (0,var (_Value,_)) => _Value,
                (1,var (_,_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple2Enumerator<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T> Value;
            public ValueTuple2Enumerator(ValueTuple<T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple2Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple2Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        internal class ValueTuple2Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T> Value;
            public ValueTuple2Enumerator2(ValueTuple<T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple2Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple2Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple3Enumerable<T> GetEnumerable<T>(this ValueTuple<T,T,T> self) => new ValueTuple3Enumerable<T>(self);     
        public struct ValueTuple3Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 3;
            readonly ValueTuple<T,T,T> Value;
            public ValueTuple3Enumerable(ValueTuple<T,T,T> Value) => this.Value = Value;
            public ValueTuple3Enumerator<T> GetEnumerator() => new ValueTuple3Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple3Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple3Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T,T> Value) => (Index,Value)switch {
                (0,var (_Value,_,_)) => _Value,
                (1,var (_,_Value,_)) => _Value,
                (2,var (_,_,_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple3Enumerator<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T> Value;
            public ValueTuple3Enumerator(ValueTuple<T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple3Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple3Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        internal class ValueTuple3Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T> Value;
            public ValueTuple3Enumerator2(ValueTuple<T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple3Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple3Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple4Enumerable<T> GetEnumerable<T>(this ValueTuple<T,T,T,T> self) => new ValueTuple4Enumerable<T>(self);     
        public readonly struct ValueTuple4Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 4;
            readonly ValueTuple<T,T,T,T> Value;
            public ValueTuple4Enumerable(ValueTuple<T,T,T,T> Value) => this.Value = Value;
            public ValueTuple4Enumerator<T> GetEnumerator() => new ValueTuple4Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple4Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple4Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T,T,T> Value) => (Index,Value)switch {
                (0,var (_Value,_,_,_)) => _Value,
                (1,var (_,_Value,_,_)) => _Value,
                (2,var (_,_,_Value,_)) => _Value,
                (3,var (_,_,_,_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple4Enumerator<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T> Value;
            public ValueTuple4Enumerator(ValueTuple<T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple4Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple4Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public class ValueTuple4Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T> Value;
            public ValueTuple4Enumerator2(ValueTuple<T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple4Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple4Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple5Enumerable<T> GetEnumerable<T>(this ValueTuple<T,T,T,T,T> self) => new ValueTuple5Enumerable<T>(self);     
        public readonly struct ValueTuple5Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 5;
            readonly ValueTuple<T,T,T,T,T> Value;
            public ValueTuple5Enumerable(ValueTuple<T,T,T,T,T> Value) => this.Value = Value;
            public ValueTuple5Enumerator<T> GetEnumerator() => new ValueTuple5Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple5Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple5Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T,T,T,T> Value) => (Index,Value)switch {
                (0,var (_Value,_,_,_,_)) => _Value,
                (1,var (_,_Value,_,_,_)) => _Value,
                (2,var (_,_,_Value,_,_)) => _Value,
                (3,var (_,_,_,_Value,_)) => _Value,
                (4,var (_,_,_,_,_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple5Enumerator<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T> Value;
            public ValueTuple5Enumerator(ValueTuple<T,T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple5Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple5Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        internal class ValueTuple5Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T> Value;
            public ValueTuple5Enumerator2(ValueTuple<T,T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple5Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple5Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple6Enumerable<T> GetEnumerable<T>(this ValueTuple<T,T,T,T,T,T> self) => new ValueTuple6Enumerable<T>(self);     
        public readonly struct ValueTuple6Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 6;
            readonly ValueTuple<T,T,T,T,T,T> Value;
            public ValueTuple6Enumerable(ValueTuple<T,T,T,T,T,T> Value) => this.Value = Value;
            public ValueTuple6Enumerator<T> GetEnumerator() => new ValueTuple6Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple6Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple6Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T,T,T,T,T> Value) => (Index,Value)switch {
                (0,var (_Value,_,_,_,_,_)) => _Value,
                (1,var (_,_Value,_,_,_,_)) => _Value,
                (2,var (_,_,_Value,_,_,_)) => _Value,
                (3,var (_,_,_,_Value,_,_)) => _Value,
                (4,var (_,_,_,_,_Value,_)) => _Value,
                (5,var (_,_,_,_,_,_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple6Enumerator<T> : IEnumerator<T>
        {
            int Index;
            internal const int Length = 6;
            readonly ValueTuple<T,T,T,T,T,T> Value;
            public ValueTuple6Enumerator(ValueTuple<T,T,T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple6Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple6Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public class ValueTuple6Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T,T> Value;
            public ValueTuple6Enumerator2(ValueTuple<T,T,T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple6Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple6Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple7Enumerable<T> GetEnumerable<T>(this ValueTuple<T,T,T,T,T,T,T> self) => new ValueTuple7Enumerable<T>(self);     
        public readonly struct ValueTuple7Enumerable<T> : IEnumerable<T>
        {
            internal const int Length = 7;
            readonly ValueTuple<T,T,T,T,T,T,T> Value;
            public ValueTuple7Enumerable(ValueTuple<T,T,T,T,T,T,T> Value) => this.Value = Value;
            public ValueTuple7Enumerator<T> GetEnumerator() => new ValueTuple7Enumerator<T>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple7Enumerator2<T>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple7Enumerator2<T>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T,T,T,T,T,T> Value) => (Index,Value)switch {
                (0,var (_Value,_,_,_,_,_,_)) => _Value,
                (1,var (_,_Value,_,_,_,_,_)) => _Value,
                (2,var (_,_,_Value,_,_,_,_)) => _Value,
                (3,var (_,_,_,_Value,_,_,_)) => _Value,
                (4,var (_,_,_,_,_Value,_,_)) => _Value,
                (5,var (_,_,_,_,_,_Value,_)) => _Value,
                (6,var (_,_,_,_,_,_,_Value)) => _Value,
                _ => throw new IndexOutOfRangeException()
            };
        }
        public struct ValueTuple7Enumerator<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T,T,T> Value;
            public ValueTuple7Enumerator(ValueTuple<T,T,T,T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple7Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple7Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public class ValueTuple7Enumerator2<T> : IEnumerator<T>
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T,T,T> Value;
            public ValueTuple7Enumerator2(ValueTuple<T,T,T,T,T,T,T> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple7Enumerable<T>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple7Enumerable<T>.Length;
            void IEnumerator.Reset() => Index = -1;
        }
        public static ValueTuple8Enumerable<T,TRest> GetEnumerable<T,TRest>(this ValueTuple<T,T,T,T,T,T,T,TRest> self) where TRest: struct => new ValueTuple8Enumerable<T,TRest>(self);     
        public readonly struct ValueTuple8Enumerable<T,TRest> : IEnumerable<T>
            where TRest: struct
        {
            readonly ValueTuple<T,T,T,T,T,T,T,TRest> Value;
            public ValueTuple8Enumerable(ValueTuple<T,T,T,T,T,T,T,TRest> Value) => this.Value = Value;
            public ValueTuple8Enumerator<T,TRest> GetEnumerator() => new ValueTuple8Enumerator<T,TRest>(Value);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new ValueTuple8Enumerator2<T,TRest>(Value);
            IEnumerator IEnumerable.GetEnumerator() => new ValueTuple8Enumerator2<T,TRest>(Value);
            internal static T GetCurrent(int Index, ValueTuple<T,T,T,T,T,T,T,TRest> Value) => (Index,Value)switch {
                (0,var (_Value,_,_,_,_,_,_,_)) => _Value,
                (1,var (_,_Value,_,_,_,_,_,_)) => _Value,
                (2,var (_,_,_Value,_,_,_,_,_)) => _Value,
                (3,var (_,_,_,_Value,_,_,_,_)) => _Value,
                (4,var (_,_,_,_,_Value,_,_,_)) => _Value,
                (5,var (_,_,_,_,_,_Value,_,_)) => _Value,
                (6,var (_,_,_,_,_,_,_Value,_)) => _Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T> __Value
                    && ValueTuple1Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T,T> __Value 
                    && ValueTuple2Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T,T,T> __Value 
                    && ValueTuple3Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T,T,T,T> __Value 
                    && ValueTuple4Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T,T,T,T,T> __Value 
                    && ValueTuple5Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T,T,T,T,T,T> __Value 
                    && ValueTuple6Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is ValueTuple<T,T,T,T,T,T,T> __Value 
                    && ValueTuple7Enumerable<T>.GetCurrent(Index -7,__Value) is T ___Value => ___Value,
                (_,var (_,_,_,_,_,_,_,_Value)) 
                    when _Value is TRest RestValue
                    && GetNestedCurrent(Index, RestValue) is T __Value => __Value,
                _ => throw new IndexOutOfRangeException()
            };
            internal static T GetNestedCurrent(int Index, TRest Rest)
            {
                if(!(GetNestedType(Rest).GetMethod(nameof(GetCurrent), BindingFlags.Static | BindingFlags.NonPublic) is MethodInfo Method))
                    throw new InvalidOperationException();
                if(!(Method.Invoke(null, new object[] { Index - 7, Rest }) is T Value))
                    throw new InvalidOperationException();
                return Value;
}

            internal static Type GetNestedType(TRest Value) 
                => Value.GetType() is Type Type
                    && Type.GetGenericTypeDefinition() == typeof(Tuple<,,,,,,,>) 
                    && Type.GetGenericArguments() is Type[] Types
                    && typeof(ValueTuple8Enumerable<,>).MakeGenericType(Types.First(), Types.Last()) is Type NestedType 
                    ? NestedType : throw new InvalidOperationException();
            internal static int GetLength(ValueTuple<T,T,T,T,T,T,T,TRest> Value) => Value switch {
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple1Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple2Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple3Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T,T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple4Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T,T,T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple5Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T,T,T,T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple6Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T,T,T,T,T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple7Enumerable<T>.Length,
                (var (_,_,_,_,_,_,_,_Value)) when _Value is ValueTuple<T,T,T,T,T,T,T,T> __Value => ValueTuple7Enumerable<T>.Length + ValueTuple8Enumerable<T>.Length,
            };
        }
        public struct ValueTuple8Enumerator<T,TRest> : IEnumerator<T>
            where TRest: struct
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T,T,T,TRest> Value;
            public ValueTuple8Enumerator(ValueTuple<T,T,T,T,T,T,T,TRest> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple8Enumerable<T,TRest>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < 7;
            void IEnumerator.Reset() => Index = -1;
        }
        public class ValueTuple8Enumerator2<T,TRest> : IEnumerator<T>
            where TRest: struct
        {
            int Index;
            readonly ValueTuple<T,T,T,T,T,T,T,TRest> Value;
            public ValueTuple8Enumerator2(ValueTuple<T,T,T,T,T,T,T,TRest> Value)
                => (this.Value,Index) = (Value,-1);
            public T Current => ValueTuple8Enumerable<T,TRest>.GetCurrent(Index,Value);
            T IEnumerator<T>.Current => Current;
            object? IEnumerator.Current => Current;
            void IDisposable.Dispose(){}
            bool IEnumerator.MoveNext() => ++Index < ValueTuple8Enumerable<T,TRest>.GetLength(Value);
            void IEnumerator.Reset() => Index = -1;
        }
    }
}