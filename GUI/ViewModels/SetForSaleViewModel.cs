using System;
using System.IO;
using System.Windows.Input;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class SetForSaleViewModel : ViewModelBase
{
    #region Properties

    private string _name = null!;
    private string _mileage = null!;
    private string _regNumber = null!;
    private string _startingBid = null!;
    private DateTime _year;
    private DateTime _startDate;
    private DateTime _endDate;

    public DateTime ThisYear
    {
        get => DateTime.Now;
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Mileage
    {
        get => _mileage;
        set => this.RaiseAndSetIfChanged(ref _mileage, value);
    }

    public string RegNumber
    {
        get => _regNumber;
        set => this.RaiseAndSetIfChanged(ref _regNumber, value);
    }

    public string StartingBid
    {
        get => _startingBid;
        set => this.RaiseAndSetIfChanged(ref _startingBid, value);
    }

    public DateTime Year
    {
        get => _year;
        set => this.RaiseAndSetIfChanged(ref _year, value);
    }

    public DateTime StartDate
    {
        get => _startDate;
        set => this.RaiseAndSetIfChanged(ref _startDate, value);
    }

    public DateTime EndDate
    {
        get => _endDate;
        set => this.RaiseAndSetIfChanged(ref _endDate, value);
    }

    #endregion


    public ICommand CreateSaleCommand { get; }
    public ICommand CancelCommand { get; }

    public SetForSaleViewModel()
    {
        CreateSaleCommand = ReactiveCommand.Create(CreateSale);
        CancelCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new HomeScreenView()));

        Year = DateTime.Now;
        StartDate = DateTime.Now;
        EndDate = DateTime.Now.AddDays(+1);
    }

    #region Methods

    private void CreateSale()
    {
        try
        {
            // Sale creation

            Auction newAuction = new(0, new Vehicle(0, Name, float.Parse(Mileage), RegNumber, Convert.ToUInt16(Year),
                true, Data.Classes.Vehicles.LicenseType.B, 22, 22, Data.Classes.Vehicles.FuelType.Diesel,
                Data.Classes.Vehicles.EnergyType.A
            ), UserInstance.GetCurrentUser(), null, 0, decimal.Parse(StartingBid));

            DatabaseManager.CreateAuction(newAuction);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error : {e.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Sale created");
        Console.ResetColor();
    }

    #endregion
}