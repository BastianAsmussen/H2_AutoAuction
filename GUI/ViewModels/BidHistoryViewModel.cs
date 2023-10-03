using System;
using System.Collections.ObjectModel;
using Data.Classes.Auctions;
using Data.DatabaseManager;
using GUI.Utilities;
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

    public BidHistoryViewModel()
    {
        try
        {
            LoadBids();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading auctions: {e.Message}");
        }
    }
    
    
    private void LoadBids()
    {
            var allAuctions = DatabaseManager.GetBidsByUser(UserInstance.GetCurrentUser());
            _bidHistory = new(allAuctions);
    }

}