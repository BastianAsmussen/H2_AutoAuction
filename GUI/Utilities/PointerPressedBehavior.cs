using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace GUI.Utilities;

/// <summary>
///     Provides a behavior for a <see cref="Control" /> that executes a <see cref="ICommand" /> when the
///     <see cref="InputElement.PointerPressedEvent" /> is raised on any element.
/// </summary>
public static class PointerPressedBehavior
{
    public static readonly AttachedProperty<ICommand> CommandProperty =
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        AvaloniaProperty.RegisterAttached<Control, ICommand>("Command", typeof(PointerPressedBehavior), null, true);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

    static PointerPressedBehavior()
    {
        InputElement.PointerPressedEvent.AddClassHandler<Control>((sender, e) =>
        {
            var command = GetCommand(sender);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (command != null && command.CanExecute(null)) command.Execute(null);
        });
    }

    public static ICommand GetCommand(Control control)
    {
        return control.GetValue(CommandProperty);
    }

    public static void SetCommand(Control control, ICommand value)
    {
        control.SetValue(CommandProperty, value);
    }
}