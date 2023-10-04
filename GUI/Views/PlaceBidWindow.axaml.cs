using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Data.Classes.Auctions;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.ViewModels;

namespace GUI.Views;

public partial class PlaceBidWindow : Window
{
    public PlaceBidWindow()
    {
        InitializeComponent();
    }

}