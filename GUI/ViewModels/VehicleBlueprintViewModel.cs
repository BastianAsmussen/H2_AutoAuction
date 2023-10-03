using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    private string? _selectedVehicleType;

    private bool _isPrivatePersonalCar;
    private bool _isProfessionalPersonalCar;
    private bool _isBus;
    private bool _isTruck;
    private string? _height;
    private string? _width;
    private string? _weight;


    // Private Personal Car
    private bool _hasIsoFix;
    private string? _numberOfSeats;

    // Professoinal Personal Car
    private bool _hasSafetyBar;
    private string? _loadCapacity;

    // Personal Car
    private string? _drivenKilometers;
    private string? _engineSize;
    private bool _hasTowBar;

    // Bus
    private string? _numberOfSleepingSpaces;
    private bool _hasToilet;

    public List<string> VehicleType { get; } = new()
    {
        "Private Personal Car",
        "Professional Personal Car",
        "Bus",
        "Truck"
    };

    #region Properties

    public string? SelectedVehicleType
    {
        get => _selectedVehicleType;
        set => this.RaiseAndSetIfChanged(ref _selectedVehicleType, value);
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
    public string? Height
    {
        get => _height;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _height, value);
        }
    }

    public string? Width
    {
        get => _width;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _width, value);
        }
    }

    public string? Weight
    {
        get => _weight;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _weight, value);
        }
    }

    // Private Personal Car Properties

    public bool HasIsoFix
    {
        get => _hasIsoFix;
        set => this.RaiseAndSetIfChanged(ref _hasIsoFix, value);
    }

    public string? NumberOfSeats
    {
        get => _numberOfSeats;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _numberOfSeats, value);
        }
    }


    // Professional Personal Car Properties
    public bool HasSafetyBar
    {
        get => _hasSafetyBar;
        set => this.RaiseAndSetIfChanged(ref _hasSafetyBar, value);
    }

    public string? LoadCapacity
    {
        get => _loadCapacity;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _loadCapacity, value);
        }
    }

    // Personal Car Properties

    public string? DrivenKilometers
    {
        get => _drivenKilometers;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _drivenKilometers, value);
        }
    }

    public string? EngineSize
    {
        get => _engineSize;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _engineSize, value);
        }
    }

    public bool HasTowBar
    {
        get => _hasTowBar;
        set => this.RaiseAndSetIfChanged(ref _hasTowBar, value);
    }

    // Bus Properties
    public string? NumberOfSLeeepingSpaces
    {
        get => _numberOfSleepingSpaces;
        set
        {
            OnlyNumbers(value);

            this.RaiseAndSetIfChanged(ref _numberOfSleepingSpaces, value);
        }
    }

    public bool HasToilet
    {
        get => _hasToilet;
        set => this.RaiseAndSetIfChanged(ref _hasToilet, value);
    }

    #endregion

    public ICommand YesCommand { get; }
    public ICommand NoCommand { get; }

    public VehicleBlueprintViewModel()
    {
        YesCommand = ReactiveCommand.Create(DoesHaveTowBar);
        NoCommand = ReactiveCommand.Create(DoesNotHaveTowBar);
    }

    #region Methods

    private void DoesHaveTowBar() => HasTowBar = true;
    private void DoesNotHaveTowBar() => HasTowBar = false;

    private void OnlyNumbers(string? value)
    {
        if (!string.IsNullOrEmpty(value))
            if (!value.All(char.IsDigit))
                throw new DataValidationException("Numbers only");
    }
    //Personal Vehicles

    #region Personal Vehicles

    [Description("It Returns the Dimensions of the vehicle")]
    public Dimensions GetDimensions()
    {
        if (string.IsNullOrEmpty(Height) || string.IsNullOrEmpty(Width) || string.IsNullOrEmpty(Weight))
            throw new("Please fill out all fields");

        return new Dimensions(0, int.Parse(Height), int.Parse(Width), int.Parse(Weight));
    }

    [Description("It Returns a PersonalCar Object")]
    public PersonalCar GetPersonalCar()
    {
        if (string.IsNullOrEmpty(NumberOfSeats))
            throw new("Please fill out all fields");

        try
        {
            return new PersonalCar(0, Byte.Parse(NumberOfSeats), GetDimensions(), new Vehicle());
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
        if (string.IsNullOrEmpty(LoadCapacity))
            throw new("Load Capacity is empty");
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

    //Heavy Vehicles

    #region Heavy Vehicles

    [Description("It Returns a Heavy Vehicle Object")]
    public HeavyVehicle GetHeavyVehicle()
    {
        try
        {
            return new HeavyVehicle(0, GetDimensions(), new Vehicle());
        }
        catch (Exception e)
        {
            throw new($"Error: {e.Message}");
        }
    }

    [Description("It Returns a Bus Object")]
    public Bus GetBus()
    {
        if (string.IsNullOrEmpty(NumberOfSeats) || string.IsNullOrEmpty(NumberOfSLeeepingSpaces))
            throw new("Numbers of seats or sleeping spaces is empty");
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
        if (string.IsNullOrEmpty(LoadCapacity))
            throw new("Load Capacity is empty");
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

    #endregion
}