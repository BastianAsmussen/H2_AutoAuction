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
    private ObservableCollection<Bid> _userBids;

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

    public ObservableCollection<Bid> UserBids
    {
        get => _userBids;
        set => this.RaiseAndSetIfChanged(ref _userBids, value);
    }

    public ICommand CancelCommand { get; }

    private void GetYourBids()
    {
        UserBids = new ObservableCollection<Bid>(DatabaseManager.GetBidsByUser(UserInstance.GetCurrentUser()));
    }
}