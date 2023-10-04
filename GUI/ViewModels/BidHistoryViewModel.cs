using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Data.Classes.Auctions;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class BidHistoryViewModel : ViewModelBase
{
    private ObservableCollection<Bid> _bidHistory;

    public ObservableCollection<Bid> BidHistory
    {
        get => _bidHistory;
        set => this.RaiseAndSetIfChanged(ref _bidHistory, value);
    }

    private ObservableCollection<Bid> _userBids;

    public ObservableCollection<Bid> UserBids
    {
        get => _userBids;
        set => this.RaiseAndSetIfChanged(ref _userBids, value);
    }

    private int _bidCount = 0;
    public string FinalPrice => DatabaseManager.GetBidById(BidHistory[_bidCount++].BidId).Auction.CurrentPrice.ToString();

    //     _finalPrices;
    // set => this.RaiseAndSetIfChanged(ref _finalPrices, value);
    public ICommand CancelCommand { get; }

    public BidHistoryViewModel()
    {
        CancelCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new HomeScreenView()));
        try
        {
            GetYourBids();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading auctions: {e.Message}");
        }
    }

    private void GetYourBids()
    {
        UserBids = new ObservableCollection<Bid>(DatabaseManager.GetBidsByUser(UserInstance.GetCurrentUser()));
    }


    // private void LoadBids()
    // {
    //     // var curentAuctions = DatabaseManager.get(bidder);
    //     // var auctionBid = DatabaseManager.GetBidsByAuction(curentAuctions);
    //     var allAuctions = DatabaseManager.GetBidsByUser(UserInstance.GetCurrentUser());
    //     _bidHistory = new(allAuctions);
    // }
}