using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
    public Vehicle VehicleData
    {
        get
        {
            return SelectedVehicleType switch
            {
                "Private Personal Car" => GetPrivatePersonalCar(),
                "Professional Personal Car" => GetProfessionalCar(),
                "Bus" => GetBus(),
                "Truck" => GetTruck(),
                _ => throw new InvalidDataException("Vehicle type is empty!")
            };
        }
    }

    private string? _selectedVehicleType;
    private bool _isPrivatePersonalCar;
    private bool _isProfessionalPersonalCar;
    private bool _isBus;
    private bool _isTruck;
    private int _height;
    private int _width;
    private int _weight;
    private string _regNumber;
    private LicenseType _selectedLicenseType;
    private FuelType _selectedFuelType;
    private double _mileage;
    private string _name;

    // Private Personal Car
    private bool _hasIsoFix;
    private int _numberOfSeats;

    // Professoinal Personal Car
    private bool _hasSafetyBar;
    private int _loadCapacity;

    // Personal Car
    private int _drivenKilometers;
    private double _engineSize;
    private bool _hasTowBar;

    // Bus
    private int _numberOfSleepingSpaces;
    private bool _hasToilet;


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
        return new Dimensions(0, Height, Width, Weight);
    }

    [Description("It Returns a PersonalCar Object")]
    public PersonalCar GetPersonalCar()
    {
        try
        {
            var seatNumber = Convert.ToByte(NumberOfSeats);

            // Vehicle vehicle = 
            var personalCar = new PersonalCar(0, seatNumber, GetDimensions(), new Vehicle()
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
                Name = _name,
            });

            return personalCar;
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Private Personal Car Object")]
    public PrivatePersonalCar GetPrivatePersonalCar()
    {
        try
        {
            return new PrivatePersonalCar(0, HasIsoFix, GetPersonalCar());
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }


    [Description("It Returns a Professional Personal Car Object")]
    public ProfessionalPersonalCar GetProfessionalCar()
    {
        try
        {
            return new ProfessionalPersonalCar(0, HasSafetyBar, Convert.ToDouble(LoadCapacity), GetPersonalCar());
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    #endregion

    #region Heavy Vehicles

    [Description("It Returns a Heavy Vehicle Object")]
    public HeavyVehicle GetHeavyVehicle()
    {
        try
        {
            // Vehicle vehicle = 
            var heavyVehicle = new HeavyVehicle(0, GetDimensions(), new Vehicle()
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
                Name = _name,
            });

            return heavyVehicle;
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Bus Object")]
    public Bus GetBus()
    {
        try
        {
            return new Bus(0, Convert.ToByte(NumberOfSeats), Convert.ToByte(NumberOfSLeeepingSpaces), HasToilet,
                GetHeavyVehicle());
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Truck Object")]
    public Truck GetTruck()
    {
       
        try
        {
            return new Truck(0, Convert.ToDouble(LoadCapacity), GetHeavyVehicle());
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    #endregion

    #region Command Methods

    private void NoToilet() => HasToilet = false;

    private void YesToilet() => HasToilet = true;

    private void NoSafetyBar() => HasSafetyBar = false;

    private void YesSafetyBar() => HasSafetyBar = true;

    private void NoIsoFix() => HasIsoFix = false;

    private void YesIsoFix() => HasIsoFix = true;


    private void YesTowBar() => HasTowBar = true;
    private void NoTowBar() => HasTowBar = false;

    #endregion

    #endregion

    public void SetRegistrationNumber(string regNumber) => _regNumber = regNumber;
    public void SetMileage(double mileage) => _mileage = mileage;
    public void SetLicenseType(LicenseType licenseType) => _selectedLicenseType = licenseType;
    public void SetVehicleName(string vehicleName) => _name = vehicleName;

    public Vehicle GetVehicleBlueprint() => VehicleData;
}