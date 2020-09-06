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
            ({ } _Values,_, Type[] TupleTypes) => To(MakeTupleType(TupleTypes), _Values),
            ({ } _Values, Type _TargetType, _) when _TargetType.IsTuple() || _TargetType.IsValueTuple()
                => To(_TargetType, _Values),
            ({ } _Values,_, _) => To(MakeTupleType(_Values.Select(v => v?.GetType() ?? throw new ArgumentException("values has null value.", nameof(Values)))), _Values),
            _ => throw new NotSupportedException("not support pattern."),
        };

        public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => From(value);
    }
}
