using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using static TupleConverters.TupleUtility;

namespace TupleConverters.WPF
{
    public class TupleConverter : IMultiValueConverter
    {
        public object Convert(object?[] Values, Type TargetType, object? Parameter, CultureInfo culture) 
            => (Values, TargetType, Parameter) switch
        {
            // Parameter :Type[] Pattern
            ({ } v,_, Type[] TupleTypes) => To(MakeTupleType(TupleTypes), v),
            // Parameter Is TupleType Pattern
            ({ } v,_, Type TupleType) when TupleType.IsTuple() || TupleType.IsValueTuple()
                => To(TupleType, v),
            // TargetType Is TupleType Pattern
            ({ } _Values, Type TupleType, _) when TupleType.IsTuple() || TupleType.IsValueTuple()
                => To(TupleType, _Values),
            // Values guess Tuple Type Pattern
            ({ } _Values,_, _) => To(MakeTupleType(_Values.Select(v => v?.GetType() ?? throw new ArgumentException("values has null value.", nameof(Values)))), _Values),
            _ => throw new NotSupportedException("not support pattern."),
        };

        public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => From(value);
    }
}
