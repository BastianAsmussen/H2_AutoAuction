using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Data.Classes;
using Data.Classes.Vehicles;
using Data.Interfaces;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class HomeScreenViewModel : ViewModelBase
{
    #region Properties

    private ObservableCollection<DemoAuction> _userAuctions;
    private ObservableCollection<DemoAuction> _currentAuctions;

    public ObservableCollection<DemoAuction> UserAuctions
    {
        get => _userAuctions;
        set => this.RaiseAndSetIfChanged(ref _userAuctions, value);
    }

    public ObservableCollection<DemoAuction> CurrentAuctions
    {
        get => _currentAuctions;
        set => this.RaiseAndSetIfChanged(ref _currentAuctions, value);
    }

    #endregion

    public ICommand SetForSaleCommand { get; }
    public ICommand UserProfileCommand { get; }
    public ICommand BidHistoryCommand { get; }

    public HomeScreenViewModel()
    {
        UserProfileCommand = ReactiveCommand.Create(ShowUserProfile);
        SetForSaleCommand = ReactiveCommand.Create(ShowBidHistory);
        BidHistoryCommand = ReactiveCommand.Create(ShowSetForSale);

        CurrentAuctions = new(DemoAuction.DemoAuctions());
        UserAuctions = new(DemoAuction.DemoAuctions());
    }


    #region Methods

    private void ShowUserProfile()
    {
        Utilities.ContentArea.Navigate(new UserProfileView());
    }


    private void ShowBidHistory()
    {
        Utilities.ContentArea.Navigate(new UserProfileView());
    }

    private void ShowSetForSale()
    {
        Utilities.ContentArea.Navigate(new SetForSaleView());
    }

    #endregion
}

#region Demo Data

public class DemoAuction
{
    public string Name { get; set; }
    public int Year { get; set; }
    public int StandingBid { get; set; }

    public static ObservableCollection<DemoAuction> DemoAuctions()
    {
        ObservableCollection<DemoAuction> auctions = new();
        for (int i = 0; i < 30; i++)
        {
            DemoAuction d = new()
            {
                Name = $"Auction {i}",
                Year = new Random().Next(1800, 2023) + i,
                StandingBid = new Random().Next(0, int.MaxValue)
            };

            auctions.Add(d);
        }

        return auctions;
    }
}

#endregion