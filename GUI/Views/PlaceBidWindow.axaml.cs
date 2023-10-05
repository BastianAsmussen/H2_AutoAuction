using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GUI.Views;

public partial class PlaceBidWindow : Window
{
    public PlaceBidWindow()
    {
        InitializeComponent();
    }

    private void OnCancelButtonClicked(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}