using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia.Data;
using Data.Classes.Vehicles;
using Data.Classes.Vehicles.HeavyVehicles;
using Data.Classes.Vehicles.PersonalCars;
using Data.DatabaseManager;
using GUI.Views.UserControls;
using ReactiveUI;

namespace GUI.ViewModels;

public class VehicleBlueprintViewModel : ViewModelBase
{
    // Personal Car
    private int _drivenKilometers;
    private double _engineSize;

    // Private Personal Car
    private bool _hasIsoFix;

    // Professoinal Personal Car
    private bool _hasSafetyBar;
    private bool _hasToilet;
    private bool _hasTowBar;
    private int _height;
    private bool _isBus;
    private bool _isPrivatePersonalCar;
    private bool _isProfessionalPersonalCar;
    private bool _isTruck;
    private int _loadCapacity;
    private double _mileage;
    private string _name;
    private int _numberOfSeats;

    // Bus
    private int _numberOfSleepingSpaces;
    private string _regNumber;
    private FuelType _selectedFuelType;
    private LicenseType _selectedLicenseType;

    private string? _selectedVehicleType;
    private int _weight;
    private int _width;

    public VehicleBlueprintViewModel()
    {
        YesIsoFixCommand = ReactiveCommand.Create(YesIsoFix);
        NoIsoFixCommand = ReactiveCommand.Create(NoIsoFix);
        YesSafetyBarCommand = ReactiveCommand.Create(YesSafetyBar);
        NoSafetyBarCommand = ReactiveCommand.Create(NoSafetyBar);
        YesToiletCommand = ReactiveCommand.Create(YesToilet);
        NoToiletCommand = ReactiveCommand.Create(NoToilet);
        YesTowBarCommand = ReactiveCommand.Create(YesTowBar);
        NoTowBarCommand = ReactiveCommand.Create(NoTowBar);
    }

    public Vehicle VehicleData
    {
        get
        {
            return SelectedVehicleType switch
            {
                "Privat Personbil" => GetPrivatePersonalCar(),
                "Erhvervs Personbil" => GetProfessionalCar(),
                "Bus" => GetBus(),
                "Lastbil" => GetTruck(),
                _ => throw new InvalidDataException("Vehicle type is empty!")
            };
        }
    }

    public void SetRegistrationNumber(string regNumber)
    {
        _regNumber = regNumber;
    }

    public void SetMileage(double mileage)
    {
        _mileage = mileage;
    }

    public void SetLicenseType(LicenseType licenseType)
    {
        _selectedLicenseType = licenseType;
    }

    public void SetVehicleName(string vehicleName)
    {
        _name = vehicleName;
    }

    public Vehicle GetVehicleBlueprint()
    {
        return VehicleData;
    }


    #region Properties

    public List<FuelType> FuelTypes { get; } = Enum.GetValues(typeof(FuelType)).Cast<FuelType>().ToList();

    public List<string> VehicleType { get; } = new()
    {
        "Privat Personbil",
        "Erhvervs Personbil",
        "Bus",
        "Lastbil"
    };

    public string? SelectedVehicleType
    {
        get => _selectedVehicleType;
        set
        {
            switch (value)
            {
                case
                    "Privat Personbil":
                    IsPrivatePersonalCar = true;
                    IsProfessionalPersonalCar = false;
                    IsBus = false;
                    IsTruck = false;
                    this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
                    break;
                case "Erhvervs Personbil":
                    IsProfessionalPersonalCar = true;
                    IsPrivatePersonalCar = false;
                    IsBus = false;
                    IsTruck = false;
                    this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
                    break;
                case "Bus":
                    IsBus = true;
                    IsPrivatePersonalCar = false;
                    IsProfessionalPersonalCar = false;
                    IsTruck = false;
                    this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
                    break;
                case "Lastbil":
                    IsTruck = true;
                    IsBus = false;
                    IsPrivatePersonalCar = false;
                    IsProfessionalPersonalCar = false;
                    this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
                    break;
                default:
                    throw new DataException("Vehicle type is empty!");
            }
        }
    }

    public bool IsPrivatePersonalCar
    {
        get => _isPrivatePersonalCar;
        set => this.RaiseAndSetIfChanged(ref _isPrivatePersonalCar, value);
    }

    public FuelType SelectedFuelType
    {
        get => _selectedFuelType;
        set => this.RaiseAndSetIfChanged(ref _selectedFuelType, value);
    }

    public bool IsProfessionalPersonalCar
    {
        get => _isProfessionalPersonalCar;
        set => this.RaiseAndSetIfChanged(ref _isProfessionalPersonalCar, value);
    }

    public bool IsBus
    {
        get => _isBus;
        set => this.RaiseAndSetIfChanged(ref _isBus, value);
    }

    public bool IsTruck
    {
        get => _isTruck;
        set => this.RaiseAndSetIfChanged(ref _isTruck, value);
    }

    //Dimensions Properties
    public int Height
    {
        get => _height;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _height, value);
        }
    }

    public int Width
    {
        get => _width;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _width, value);
        }
    }

    public int Weight
    {
        get => _weight;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _weight, value);
        }
    }

    // Private Personal Car Properties

    public bool HasIsoFix
    {
        get => _hasIsoFix;
        set => this.RaiseAndSetIfChanged(ref _hasIsoFix, value);
    }

    public int NumberOfSeats
    {
        get => _numberOfSeats;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _numberOfSeats, value);
        }
    }

    // Professional Personal Car Properties
    public bool HasSafetyBar
    {
        get => _hasSafetyBar;
        set => this.RaiseAndSetIfChanged(ref _hasSafetyBar, value);
    }

    public int LoadCapacity
    {
        get => _loadCapacity;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _loadCapacity, value);
        }
    }

    // Personal Car Properties

    public int DrivenKilometers
    {
        get => _drivenKilometers;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _drivenKilometers, value);
        }
    }

    public double EngineSize
    {
        get => _engineSize;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _engineSize, value);
        }
    }

    public bool HasTowBar
    {
        get => _hasTowBar;
        set => this.RaiseAndSetIfChanged(ref _hasTowBar, value);
    }

    // Bus Properties
    public int NumberOfSLeeepingSpaces
    {
        get => _numberOfSleepingSpaces;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _numberOfSleepingSpaces, value);
        }
    }

    public bool HasToilet
    {
        get => _hasToilet;
        set => this.RaiseAndSetIfChanged(ref _hasToilet, value);
    }

    #endregion

    #region Icommands

#pragma warning disable
    public ICommand YesIsoFixCommand { get; }
    public ICommand NoIsoFixCommand { get; }
    public ICommand YesSafetyBarCommand { get; }
    public ICommand NoSafetyBarCommand { get; }
    public ICommand YesToiletCommand { get; }
    public ICommand NoToiletCommand { get; }
    public ICommand YesTowBarCommand { get; }
    public ICommand NoTowBarCommand { get; }
#pragma warning restore

    #endregion

    #region Methods

    private void NumberOnly(object? value)
    {
        if (value is string val)
            if (!string.IsNullOrEmpty(val))
                if (!val.All(char.IsDigit))
                    throw new DataValidationException("Numbers only");
    }

    #region Personal Vehicles

    [Description("It Returns the Dimensions of the vehicle")]
    public Dimensions GetDimensions()
    {
        return DatabaseManager.CreateDimensions(new Dimensions(0, Height, Width, Weight));
    }

    [Description("It Returns a PersonalCar Object")]
    public PersonalCar GetPersonalCar()
    {
        try
        {
            var seatNumber = Convert.ToByte(NumberOfSeats);

            var vehicle = new Vehicle
            {
                VehicleId = 0,
                Km = DrivenKilometers,
                NewPrice = 0,
                HasTowbar = HasTowBar,
                EngineSize = EngineSize,
                RegistrationNumber = _regNumber,
                Year = (short)SetForSaleView.Year,
                LicenseType = _selectedLicenseType,
                FuelType = SelectedFuelType,
                KmPerLiter = _mileage,
                Name = _name
            };

            // Vehicle vehicle = 
            var personalCar = new PersonalCar(0, seatNumber, GetDimensions(), DatabaseManager.CreateVehicle(vehicle));


            return DatabaseManager.CreatePersonalCar(personalCar);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Private Personal Car Object")]
    public PrivatePersonalCar GetPrivatePersonalCar()
    {
        try
        {
            return DatabaseManager.CreatePrivatePersonalCar(new PrivatePersonalCar(0, HasIsoFix, GetPersonalCar()));
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}");
        }
    }


    [Description("It Returns a Professional Personal Car Object")]
    public ProfessionalPersonalCar GetProfessionalCar()
    {
        try
        {
            return DatabaseManager.CreateProfessionalPersonalCar(
                new ProfessionalPersonalCar(0, HasSafetyBar, Convert.ToDouble(LoadCapacity), GetPersonalCar()));
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}");
        }
    }

    #endregion

    #region Heavy Vehicles

    [Description("It Returns a Heavy Vehicle Object")]
    public HeavyVehicle GetHeavyVehicle()
    {
        try
        {
            var vehicle = new Vehicle
            {
                VehicleId = 0,
                Km = DrivenKilometers,
                NewPrice = 0,
                HasTowbar = HasTowBar,
                EngineSize = EngineSize,
                RegistrationNumber = _regNumber,
                Year = (short)SetForSaleView.Year,
                LicenseType = _selectedLicenseType,
                FuelType = SelectedFuelType,
                KmPerLiter = _mileage,
                Name = _name
            };

            // Vehicle vehicle = 
            var heavyVehicle = new HeavyVehicle(0, GetDimensions(), DatabaseManager.CreateVehicle(vehicle));

            return DatabaseManager.CreateHeavyVehicle(heavyVehicle);
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Bus Object")]
    public Bus GetBus()
    {
        try
        {
            return DatabaseManager.CreateBus(new Bus(0, Convert.ToByte(NumberOfSeats),
                Convert.ToByte(NumberOfSLeeepingSpaces), HasToilet,
                GetHeavyVehicle()));
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Truck Object")]
    public Truck GetTruck()
    {
        try
        {
            return DatabaseManager.CreateTruck(new Truck(0, Convert.ToDouble(LoadCapacity), GetHeavyVehicle()));
        }
        catch (Exception e)
        {
            throw new Exception($"Error: {e.Message}");
        }
    }

    #endregion

    #region Command Methods

    private void NoToilet()
    {
        HasToilet = false;
    }

    private void YesToilet()
    {
        HasToilet = true;
    }

    private void NoSafetyBar()
    {
        HasSafetyBar = false;
    }

    private void YesSafetyBar()
    {
        HasSafetyBar = true;
    }

    private void NoIsoFix()
    {
        HasIsoFix = false;
    }

    private void YesIsoFix()
    {
        HasIsoFix = true;
    }


    private void YesTowBar()
    {
        HasTowBar = true;
    }

    private void NoTowBar()
    {
        HasTowBar = false;
    }

    #endregion

    #endregion
}