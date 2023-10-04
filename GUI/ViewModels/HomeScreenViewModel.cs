using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            LoadAuctions();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading auctions: {e.Message}");
        }
    }


    #region Methods

    private void LoadAuctions()
    {
        var allAuctions = DatabaseManager.GetAllAuctions();
        CurrentAuctions = new(allAuctions);
       
        var auctionsByThisUser = DatabaseManager.GetAuctionsByUser(UserInstance.GetCurrentUser());
        UserAuctions = new(auctionsByThisUser);
    }

    private void CommandsLoader()
    {
        UserProfileCommand = ReactiveCommand.Create(ShowUserProfile);
        SetForSaleCommand = ReactiveCommand.Create(ShowSetForSale);
        BidHistoryCommand = ReactiveCommand.Create(ShowBidHistory);
        SignOutCommand = ReactiveCommand.Create(SignOut);
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
    private void ShowBidHistory() => ContentArea.Navigate(new UserProfileView());
    private void ShowSetForSale() => ContentArea.Navigate(new SetForSaleView());

    private void SignOut()
    {
        UserInstance.LogOut();
        Utilities.ContentArea.Navigate(new LoginView());
    }

    #endregion
}