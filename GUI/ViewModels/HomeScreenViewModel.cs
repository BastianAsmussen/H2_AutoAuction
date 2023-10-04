using System;
using System.Collections.Generic;
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

    public ICommand? SetForSaleCommand { get; set; }
    public ICommand? UserProfileCommand { get; set; }
    public ICommand? BidHistoryCommand { get; set; }
    public ICommand? SignOutCommand { get; set; }

    public HomeScreenViewModel()
    {
        CommandsLoader();

        try
        {
            GetAuctions();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading auctions: {e.Message}");
        }
    }


    #region Methods

    /// <summary>
    /// Asynchronously gets all auctions and user's auctions. 
    /// </summary>
    public async Task GetAuctions()
    {
        // a Task to load all auctions
        Task allAuctions = LoadAllAuctions();
        // a Task to load user's auctions
        Task usersAuctions = LoadUsersAuctions();

        // Await the completion of both tasks.
        await Task.WhenAll(allAuctions, usersAuctions);
    }

    private async Task LoadAllAuctions()
    {
        var allAuctions = DatabaseManager.GetAllAuctions();
        CurrentAuctions = new(allAuctions);
        Console.WriteLine($"ALl auctions: Done loading");
    }

    private async Task LoadUsersAuctions()
    {
        var auctionsByThisUser = DatabaseManager.GetAuctionsByUser(UserInstance.GetCurrentUser());
        UserAuctions = new(auctionsByThisUser);

        Console.WriteLine($"User auctions: Done loading");
    }

    private void CommandsLoader()
    {
        UserProfileCommand = ReactiveCommand.Create(ShowUserProfile);
        SetForSaleCommand = ReactiveCommand.Create(ShowSetForSale);
        BidHistoryCommand = ReactiveCommand.Create(ShowBidHistory);
        SignOutCommand = ReactiveCommand.Create(SignOut);
    }

    private void ShowUserProfile() => ContentArea.Navigate(new UserProfileView());
    private void ShowBidHistory() => ContentArea.Navigate(new UserProfileView());
    private void ShowSetForSale() => ContentArea.Navigate(new SetForSaleView());

    private void SignOut()
    {
        UserInstance.LogOut();
        ContentArea.Navigate(new LoginView());
    }

    #endregion
}