using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Avalonia.Controls;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.Classes.Vehicles.PersonalCars;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class SetForSaleViewModel : ViewModelBase
{
    private string _name = null!;
    private string _mileage = null!;
    private string _regNumber = null!;
    private string _startingBid = null!;
    private DateTime _year;
    private DateTime _startDate;
    private DateTime _endDate;
    private UserControl _vehicleBlueprintControl;
    private LicenseType _licenseType;
    private FuelType _fuelType;
    private EnergyType _energyType;
    private VehicleBlueprintViewModel _vehiclebpVm = new();

    #region Properties

    public int ThisYear => DateTime.Now.Year;


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

    public UserControl VehicleBlueprintControl
    {
        get => _vehicleBlueprintControl;
        set => this.RaiseAndSetIfChanged(ref _vehicleBlueprintControl, value);
    }

    #endregion

    public ICommand CreateSaleCommand { get; }
    public ICommand CancelCommand { get; }

    public SetForSaleViewModel()
    {
        CreateSaleCommand = ReactiveCommand.Create(CreateSale);
        CancelCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new HomeScreenView()));

        var blueprintView = new VehicleBlueprintView
        {
            DataContext = _vehiclebpVm
        };

        VehicleBlueprintControl = blueprintView;

        DateOnly currentDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        try
        {
            StartDate = Convert.ToDateTime(currentDate);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error : {e.Message}");
            Console.ResetColor();
        }
        
        try
        {
            EndDate = Convert.ToDateTime(currentDate).AddDays(+1);
            var a = EndDate;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error : {e.Message}");
            Console.ResetColor();
        }
    }

    #region Methods

    private void CreateSale()
    {
        try
        {
            Auction auction = new(0, Convert.ToDecimal(StartingBid), Convert.ToDecimal(StartingBid), StartDate, EndDate,
                _vehiclebpVm.VehicleData, UserInstance.GetCurrentUser(), null);

            DatabaseManager.CreateAuction(auction);
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