using System;
using System.Data;
using System.Windows.Input;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class BuyerViewModel : ViewModelBase
{
    public BuyerViewModel(Auction auction)
    {
        Auction = auction;
        Vehicle = Auction.Vehicle;

        BackCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new HomeScreenView()));
        PlaceBidCommand = ReactiveCommand.Create(() =>
        {
            var placeBidWindow = new PlaceBidWindow();

            var placeBidWindowViewModel = new PlaceBidWindowViewModel(Auction);

            placeBidWindow.DataContext = placeBidWindowViewModel;
            placeBidWindow.Show();
        });
    }

    public Auction Auction { get; }
    public Vehicle Vehicle { get; }
    public string FormattedRegistrationNumber => Vehicle.GetObfuscatedRegistrationNumber();
    public string FormattedHasTowbar => Vehicle.HasTowbar ? "Ja" : "Nej";

    public string FormattedFuelType => Vehicle.FuelType switch
    {
        FuelType.Benzine => "Benzin",
        FuelType.Diesel => "Diesel",
        _ => throw new ArgumentOutOfRangeException()
    };

    public string FormattedEnergyType => Vehicle.EnergyClass switch
    {
        EnergyType.A => "A",
        EnergyType.B => "B",
        EnergyType.C => "C",
        EnergyType.D => "D",
        _ => throw new ArgumentOutOfRangeException()
    };

    public string FormattedLicenseType => Vehicle.LicenseType switch
    {
        LicenseType.A => "A",
        LicenseType.B => "B",
        LicenseType.C => "C",
        LicenseType.D => "D",
        LicenseType.BE => "BE",
        LicenseType.CE => "CE",
        LicenseType.DE => "DE",
        _ => throw new ArgumentOutOfRangeException()
    };

    public string FormattedPrice => Vehicle.NewPrice.ToString("C");

    public ICommand BackCommand { get; }

    public string FormattedAuctionEnd => Auction.EndDate.ToString("dd/MM/yyyy HH:mm");
    public string FormattedCurrentBid => Auction.CurrentPrice.ToString("C");
    public string FormattedBidCount => GetBidsCount().ToString("N0");

    public ICommand PlaceBidCommand { get; }

    public static void Update(Auction auction)
    {
        var vm = new BuyerViewModel(auction);
        var view = new BuyerView
        {
            DataContext = vm
        };

        ContentArea.Navigate(view);
    }

    private int GetBidsCount()
    {
        try
        {
            var bids = DatabaseManager.GetBidsByAuction(Auction);

            return bids.Count;
        }
        catch (Exception e) when (e is DataException)
        {
            Console.WriteLine($"Warning: {e.Message}");

            return 0;
        }
    }
}