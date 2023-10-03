using System;
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
        Navigate(new CreateUserView());
    }

    public static void Navigate(UserControl? userControl)
    {
        if (_instance == null) return;
        if (userControl == null) return;
        try
        {
            _instance.Content = userControl;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}