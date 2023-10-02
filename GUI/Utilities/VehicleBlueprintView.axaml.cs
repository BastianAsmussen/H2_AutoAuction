using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUI.Views.UserControls;

namespace GUI.Utilities;

public partial class VehicleBlueprintView : UserControl
{
    private static VehicleBlueprintView? _instance;

    public VehicleBlueprintView()
    {
        InitializeComponent();
        _instance = this;

    }
}