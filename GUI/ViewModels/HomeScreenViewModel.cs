using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Data.Classes.Auctions;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class HomeScreenViewModel : ViewModelBase
{
    public HomeScreenViewModel()
    {
        UserProfileCommand = ReactiveCommand.Create(ShowUserProfile);
        SetForSaleCommand = ReactiveCommand.Create(ShowSetForSale);
        BidHistoryCommand = ReactiveCommand.Create(ShowBidHistory);
        SignOutCommand = ReactiveCommand.Create(SignOut);

        try
        {
            GetAuctions();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading auctions: {e.Message}");
        }
    }

    public ICommand SetForSaleCommand { get; }
    public ICommand UserProfileCommand { get; }
    public ICommand BidHistoryCommand { get; }
    public ICommand SignOutCommand { get; }

    #region Properties

    private ObservableCollection<Auction> _userAuctions = new();
    private ObservableCollection<Auction> _currentAuctions = new();

    public ObservableCollection<Auction> UserAuctions
    {
        get => _userAuctions;
        set => this.RaiseAndSetIfChanged(ref _userAuctions, value);
    }

    public ObservableCollection<Auction> CurrentAuctions
    {
        get => _currentAuctions;
        set => this.RaiseAndSetIfChanged(ref _currentAuctions, value);
    }

    #endregion


    #region Methods

    /// <summary>
    ///     Asynchronously gets all auctions and user's auctions.
    /// </summary>
    public async Task GetAuctions()
    {
        // a Task to load all auctions
        var allAuctions = LoadAllAuctions();
        // a Task to load user's auctions
        var usersAuctions = LoadUsersAuctions();

        // Await the completion of both tasks.
        await Task.WhenAll(allAuctions, usersAuctions);
    }

    private async Task LoadAllAuctions()
    {
        var allAuctions = DatabaseManager.GetAllAuctions();
        CurrentAuctions = new ObservableCollection<Auction>(allAuctions);
        Console.WriteLine("ALl auctions: Done loading");
    }

    private async Task LoadUsersAuctions()
    {
        var auctionsByThisUser = DatabaseManager.GetAuctionsByUser(UserInstance.GetCurrentUser());
        UserAuctions = new ObservableCollection<Auction>(auctionsByThisUser);

        Console.WriteLine("User auctions: Done loading");
    }

    private void ShowUserProfile()
    {
        var user = UserInstance.GetCurrentUser();

        var vm = new UserProfileViewModel(user);
        var view = new UserProfileView
        {
            DataContext = vm
        };

        ContentArea.Navigate(view);
    }

    private void ShowBidHistory()
    {
        ContentArea.Navigate(new BidHistoryView());
    }

    private void ShowSetForSale()
    {
        ContentArea.Navigate(new SetForSaleView());
    }

    private void SignOut()
    {
        UserInstance.LogOut();
        ContentArea.Navigate(new LoginView());
    }

    #endregion
}