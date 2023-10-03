using System;
using System.Reactive;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Data.Classes.Auctions;
using Data.DatabaseManager;
using GUI.Utilities;
using ReactiveUI;

namespace GUI.ViewModels;

public class PlaceBidWindowViewModel : ViewModelBase
{
    public Auction Auction { get; set; }

    public decimal Amount { get; set; }

    public ReactiveCommand<object, Unit> PlaceBidCommand { get; }

    public PlaceBidWindowViewModel(Auction auction)
    {
        Auction = auction;
        Amount = auction.CurrentPrice;

        PlaceBidCommand = ReactiveCommand.Create<object>(PlaceBidClicked);
    }

    private void PlaceBidClicked(object window)
    {
        DatabaseManager.CreateBid(new Bid(0, DateTime.Now, Amount, UserInstance.GetCurrentUser(), Auction));

        Auction = DatabaseManager.GetAuctionById(Auction.AuctionId);
        BuyerViewModel.Update(Auction);

        ((Window)window).Close();
    }
}