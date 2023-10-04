using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Avalonia.Data;
using Data.Classes.Vehicles;
using Data.Classes.Vehicles.HeavyVehicles;
using Data.Classes.Vehicles.PersonalCars;
using ReactiveUI;

namespace GUI.ViewModels;

public class VehicleBlueprintViewModel : ViewModelBase
{
    private string _name;
    private double _mileage;
    private string _regNumber;
    private int _manufacturingyear;
    private object _licenseType;


    private string? _selectedVehicleType;
    private bool _isPrivatePersonalCar;
    private bool _isProfessionalPersonalCar;
    private bool _isBus;
    private bool _isTruck;
    private int _height;
    private int _width;
    private int _weight;

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

    public int Manufacturingyear
    {
        get => _manufacturingyear;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _manufacturingyear, value);
        }
    }

    public double Mileage
    {
        get => _mileage;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _mileage, value);
        }
    }

    public string RegNumber
    {
        get => _regNumber;
        set
        {
            NumberOnly(value);

            this.RaiseAndSetIfChanged(ref _regNumber, value);
        }
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public object LisenceType
    {
        get => _licenseType;
        set => this.RaiseAndSetIfChanged(ref _licenseType, value);
    }

    public Vehicle VehicleData
    {
        get
        {
            switch (SelectedVehicleType)
            {
                case
                    "Private Personal Car":
                    return GetPrivatePersonalCar();
                case "Professional Personal Car":
                    return GetProfessionalCar();
                case "Bus":
                    return GetBus();
                case "Truck":
                    return GetBus();
                default:
                    throw new("Vehicle Type is empty");
            }
        }
    }

    public List<string> VehicleType { get; } = new()
    {
        "Private Personal Car",
        "Professional Personal Car",
        "Bus",
        "Truck"
    };

    public string? SelectedVehicleType
    {
        get => _selectedVehicleType;
        set
        {
            switch (value)
            {
                case
                    "Private Personal Car":
                    IsPrivatePersonalCar = true;
                    IsProfessionalPersonalCar = false;
                    IsBus = false;
                    IsTruck = false;
                    this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
                    break;
                case "Professional Personal Car":
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
                case "Truck":
                    IsTruck = true;
                    IsBus = false;
                    IsPrivatePersonalCar = false;
                    IsProfessionalPersonalCar = false;
                    this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
                    break;
                default:
                    throw new("Vehicle Type is empty");
            }
        }
    }

    public bool IsPrivatePersonalCar
    {
        get => _isPrivatePersonalCar;
        set => this.RaiseAndSetIfChanged(ref _isPrivatePersonalCar, value);
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
    public ICommand YesIsoFixCommand { get; set; } = null!;
    public ICommand NoIsoFixCommand { get; set; } = null!;
    public ICommand YesSafetyBarCommand { get; set; } = null!;
    public ICommand NoSafetyBarCommand { get; set; } = null!;
    public ICommand YesToiletCommand { get; set; } = null!;
    public ICommand NoToiletCommand { get; set; } = null!;
    public ICommand YesTowBarCommand { get; set; } = null!;
    public ICommand NoTowBarCommand { get; set; } = null!;
#pragma warning restore

    #endregion

    public VehicleBlueprintViewModel()
    {
        try
        {
            InitCommands();
        }

        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error : {e.Message}");
            Console.ResetColor();
        }
    }

    #region Methods

    public void SetRegNumber(string regNumber)
    {
        RegNumber = regNumber;
    }

    public void SetMileAge(double mileage)
    {
        Mileage = mileage;
    }

    public void SetLicenseType(LicenseType licenseType)
    {
        LisenceType = licenseType;
    }

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

            var vehicle = new Vehicle()
            {
                VehicleId = 0,
                Km = DrivenKilometers,
                HasTowbar = HasTowBar,
                EngineSize = EngineSize,
                Name = Name,
                KmPerLiter = Mileage,
                RegistrationNumber = this.RegNumber,
                Year = (short)Manufacturingyear,
                LicenseType = (LicenseType)LisenceType,
            };

            return new PersonalCar(0, seatNumber, GetDimensions(), vehicle);
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
            return new HeavyVehicle(0, GetDimensions(),
                new Vehicle() { EngineSize = EngineSize, HasTowbar = HasTowBar });
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Bus Object")]
    public Bus GetBus()
    {
        // if (string.IsNullOrEmpty(NumberOfSeats) || string.IsNullOrEmpty(NumberOfSLeeepingSpaces))
        // throw new("Numbers of seats or sleeping spaces is empty");
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
        // if (string.IsNullOrEmpty(LoadCapacity))
        //     throw new("Load Capacity is empty");
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

    private void InitCommands()
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

    public Vehicle GetVehicleBlueprint() => VehicleData;
}