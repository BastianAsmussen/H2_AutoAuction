using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Data;
using Data.Classes.Auctions;
using Data.Classes.Vehicles;
using Data.DatabaseManager;
using GUI.Utilities;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class SetForSaleViewModel : ViewModelBase
{
    private string _name = null!;
    private double _mileage;
    private string _regNumber = null!;
    private decimal _startingBid;
    private DateTime _startDate;
    private DateTime _endDate;
    private UserControl _vehicleBlueprintControl = null!;
    private LicenseType _licenseType;
    private FuelType _fuelType;
    private EnergyType _energyType;
    private VehicleBlueprintViewModel _vehiclebpVm = new();
    private object _selectedLicenseType;

    #region Properties

    public List<LicenseType> LicenseTypes { get; } = Enum.GetValues(typeof(LicenseType)).Cast<LicenseType>().ToList();

    public object SelectedLicenseType
    {
        get => _selectedLicenseType;
        set
        {
            _vehiclebpVm.SetLicenseType((LicenseType)SelectedLicenseType);

            this.RaiseAndSetIfChanged(ref _selectedLicenseType, value);
        }
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public double Mileage
    {
        get => _mileage;
        set
        {
            _vehiclebpVm.SetMileAge(Mileage);
            this.RaiseAndSetIfChanged(ref _mileage, value);
        }
    }

    public string RegNumber
    {
        get => _regNumber;
        set
        {
            _vehiclebpVm.SetRegNumber(RegNumber);

            this.RaiseAndSetIfChanged(ref _regNumber, value);
        }
    }

    public decimal StartingBid
    {
        get => _startingBid;
        set => this.RaiseAndSetIfChanged(ref _startingBid, value);
    }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value < Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")))
                this.RaiseAndSetIfChanged(ref _startDate, Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")));

            this.RaiseAndSetIfChanged(ref _startDate, value);
        }
    }

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (value < StartDate)
                this.RaiseAndSetIfChanged(ref _startDate,
                    Convert.ToDateTime(DateTime.Now.AddDays(+1).ToString("dd-MM-yyyy")));

            this.RaiseAndSetIfChanged(ref _endDate, value);
        }
    }

    public UserControl VehicleBlueprintControl
    {
        get => _vehicleBlueprintControl;
        set => this.RaiseAndSetIfChanged(ref _vehicleBlueprintControl, value);
    }

    #endregion

    public ICommand CreateSaleCommand { get; set; } = null!;
    public ICommand CancelCommand { get; set; } = null!;

    public SetForSaleViewModel()
    {
        InitVehicleBlueprint();
        InitCommands();
        InitDateTimes();
    }

    #region Methods

    private void InitCommands()
    {
        CreateSaleCommand = ReactiveCommand.Create(CreateSale);
        CancelCommand = ReactiveCommand.Create(() => ContentArea.Navigate(new HomeScreenView()));
    }

    private void InitVehicleBlueprint()
    {
        _vehiclebpVm = new VehicleBlueprintViewModel();
        var blueprintView = new VehicleBlueprintView
        {
            DataContext = _vehiclebpVm
        };

        VehicleBlueprintControl = blueprintView;
    }

    private void InitDateTimes()
    {
        StartDate = Convert.ToDateTime(DateTime.Now.ToString("hh:mm:ss"));
        EndDate = Convert.ToDateTime(DateTime.Now.AddDays(+1).ToString("hh:mm:ss"));
    }

    private void CreateSale()
    {
        try
        {
            var vehicle = _vehiclebpVm.GetVehicleBlueprint();
            
            Auction auction = new(0, StartingBid, StartingBid, StartDate, EndDate, vehicle, UserInstance.GetCurrentUser(),
                null);

            var aa = auction;

            // DatabaseManager.CreateAuction(auction);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error : {e.Message}");
        }

        Console.WriteLine("Sale created");
    }

    #endregion
}