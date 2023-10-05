using Avalonia.Controls;
using Data.Classes.Auctions;
using GUI.Utilities;
using GUI.ViewModels;

namespace GUI.Views.UserControls;

public partial class HomeScreenView : UserControl
{
    public HomeScreenView()
    {
        InitializeComponent();
    }

    private void OnAuctionSelectionChange(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not DataGrid dataGrid) return;

        var auction = dataGrid.SelectedItem as Auction;

        var vm = new BuyerViewModel(auction);

        var view = new BuyerView
        {
            DataContext = vm
        };

        ContentArea.Navigate(view);
    }
}