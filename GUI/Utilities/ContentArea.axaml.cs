using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUI.Views.UserControls;

namespace GUI.Utilities;

public partial class ContentArea : UserControl
{
    private static ContentArea? _instance;

    public ContentArea()
    {
        InitializeComponent();
        _instance ??= this;
        Home();
    }

    private void Home()
    {
        Navigate(new HomeScreenView());
    }

    public static void Navigate(UserControl? userControl)
    {
        if (_instance == null) return;
        if (userControl == null) return;

        _instance.Content = userControl;
    }
}