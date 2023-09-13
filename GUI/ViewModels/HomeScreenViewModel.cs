using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Data.Classes;
using Data.Classes.Vehicles;
using Data.Interfaces;
using ReactiveUI;

namespace GUI.ViewModels;

public class HomeScreenViewModel : ViewModelBase
{
    private ObservableCollection<DemoAuction> _userAuctions;
    private ObservableCollection<DemoAuction> _auctions;
    public ICommand SetForSaleCommand { get; }
    public ICommand UserProfileCommand { get; }
    public ICommand BidHistoryCommand { get; }


    public ObservableCollection<DemoAuction> UserAuctions
    {
        get => _userAuctions;
        set => this.RaiseAndSetIfChanged(ref _userAuctions, value);
    }

    public ObservableCollection<DemoAuction> Auctions
    {
        get => _auctions;
        set => this.RaiseAndSetIfChanged(ref _auctions, value);
    }


    public HomeScreenViewModel()
    {
        UserProfileCommand = ReactiveCommand.Create(ShowUserProfile);
        SetForSaleCommand = ReactiveCommand.Create(ShowBidHistory);
        BidHistoryCommand = ReactiveCommand.Create(ShowSetForSale);

        Auctions = DemoAuction.DemoAuctions();
    }


    #region Methods

    private void ShowUserProfile()
    {
        Console.WriteLine("ShowUserProfile");
    }


    private void ShowBidHistory()
    {
        Console.WriteLine("ShowBidHistory");
    }

    private void ShowSetForSale()
    {
        Console.WriteLine("ShowSetForSale");
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
        for (int i = 0; i < 10; i++)
        {
            DemoAuction d = new()
            {
                Name = $"Auction {i}",
                Year = new Random().Next(0, int.MaxValue) / i,
                StandingBid = new Random().Next(0, int.MaxValue)
            };

            auctions.Add(d);
        }

        return auctions;
    }
}

#endregion