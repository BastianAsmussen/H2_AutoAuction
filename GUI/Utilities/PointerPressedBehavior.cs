using Avalonia.Input;

namespace GUI.Utilities;

using Avalonia;
using Avalonia.Controls;
using System.Windows.Input;

/// <summary>
/// Provides a behavior for a <see cref="Control"/> that executes a <see cref="ICommand"/> when the <see cref="InputElement.PointerPressedEvent"/> is raised on any element.
/// </summary>
public static class PointerPressedBehavior
{
    public static ICommand GetCommand(Control control)
    {
        return (ICommand)control.GetValue(CommandProperty);
    }

    public static void SetCommand(Control control, ICommand value)
    {
        control.SetValue(CommandProperty, value);
    }

    public static readonly AttachedProperty<ICommand> CommandProperty =
        AvaloniaProperty.RegisterAttached<Control, ICommand>("Command", typeof(PointerPressedBehavior), null, true);

    static PointerPressedBehavior()
    {
        InputElement.PointerPressedEvent.AddClassHandler<Control>((sender, e) =>
        {
            var command = GetCommand(sender);
            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        });
    }
}