using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using static TupleConverters.TupleUtility;

namespace TupleConverters.WPF
{
    /// <summary>
    /// inject any position Command Argument.
    /// </summary>
    public class CommandArgumentInjector : IMultiValueConverter
    {
        public object Convert(object?[] Values, Type TargetType, object Parameter, CultureInfo Culture)
        {
            var Command = Values.OfType<ICommand>().ToArray() switch
            {
                ICommand[] c when c.Length <= 0 => throw new NotSupportedException("support values type has ICommand one."),
                ICommand[] c when c.Length > 1 => throw new NotSupportedException($"support command type has one. count:{c.Length}"),
                ICommand[] c => c.First(),
            };
            var Index = Array.IndexOf(Values, Command);
            var BaseTupleType = ((Parameter, Values) switch
            {
                (IEnumerable<Type> p, { } v) when p.Count() != v.Length => throw new NotSupportedException($"Type Array Parameter Length:{p.Count()} is Values Length:{v.Length} is must be the same. "),
                (IEnumerable<Type> p, { } v) => p,
                (_, { } v) when v.OfType<object>().Count() != v.Length => throw new NotSupportedException($"Values cannot contain null. "),
                (_, { } v) => v.Select(v => v!.GetType()),
                _ => throw new NotSupportedException("not support type pattern."),
            }).ToArray();
            var BaseValues = Values.Select(v => new WeakReference<object>(v!)).ToArray();
            var CommandType = typeof(ICommand);
            {
                InternalCommand Exector = new InternalCommand(CanExecute, Execute);
                Command.CanExecuteChanged += CanExecuteChanged;
                return Exector;
                // Type:InjectType -> Type:TupleType
                Type CreateTupleType(Type InjectType) => MakeTupleType(BaseTupleType.Select((v, i) => i == Index ? InjectType : v));
                // object?:Parameter -> TupleType:Parameter
                object CreateParameter(object Parameter) => To(CreateTupleType(Parameter?.GetType()!), BaseValues.Select((v, i) => i == Index ? Parameter : v));
                bool CanExecute(object _Parameter) => Command.CanExecute(CreateParameter(_Parameter));
                void Execute(object _Parameter) => Command.Execute(CreateParameter(_Parameter));
                void CanExecuteChanged(object? sender, EventArgs argments) => Exector?.RaiseCanExecuteChanged(sender, argments);
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();

        internal class InternalCommand : ICommand
        {
            readonly Func<object, bool> CanExecute;
            readonly Action<object> Execute;
            public InternalCommand(Func<object, bool> CanExecute, Action<object> Execute)
                => (this.CanExecute, this.Execute) = (CanExecute, Execute);
#pragma warning disable                   
            public event EventHandler? CanExecuteChanged;
#pragma warning restore
            bool ICommand.CanExecute(object parameter) => CanExecute(parameter);

            void ICommand.Execute(object parameter) => Execute(parameter);
            public void RaiseCanExecuteChanged(object? sender, EventArgs e) => CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
