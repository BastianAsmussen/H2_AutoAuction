using System;
using System.Reactive;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Data.Classes;
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
        Auction = DatabaseManager.GetAuctionById(Auction.AuctionId);

        var userId = UserInstance.GetCurrentUser().UserId;

        if (DatabaseManager.IsCorporateUser(userId))
        {
            var corporateUser = DatabaseManager.GetCorporateUserByUserId(userId);

            if (!corporateUser.PlaceBid(corporateUser, Auction, Amount))
            {
                Console.WriteLine("Bid was not placed!");

                return;
            }
        }
        else
        {
            var privateUser = DatabaseManager.GetPrivateUserByUserId(userId);

            if (!privateUser.PlaceBid(privateUser, Auction, Amount))
            {
                Console.WriteLine("Bid was not placed!");

                return;
            }
        }

        BuyerViewModel.Update(DatabaseManager.GetAuctionById(Auction.AuctionId));

        ((Window)window).Close();
    }
}