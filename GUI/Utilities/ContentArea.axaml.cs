using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUI.Utilities;

public partial class ContentArea : UserControl
{
    private static ContentArea? _instance;

    public ContentArea()
    {
        InitializeComponent();
        _instance ??= this;
    }

    public static void Navigate(UserControl? userControl)
    {
        if (_instance == null) return;
        if (userControl == null) return;

        _instance.Content = userControl;
    }
}