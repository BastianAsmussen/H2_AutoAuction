﻿namespace Data.Classes.Vehicles;

public abstract class Vehicle
{
    /// <summary>
    ///     ID field and property.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    ///     Name field and property.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Km field and property.
    /// </summary>
    public double Km { get; set; }

    /// <summary>
    ///     Registration number field and property.
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    ///     Year field and property.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    ///     New price field and property.
    /// </summary>
    public decimal NewPrice { get; set; }

    /// <summary>
    ///     Towbar field and property.
    /// </summary>
    public bool HasTowbar { get; set; }

    /// <summary>
    ///     Engine size field and property.
    /// </summary>
    public virtual double EngineSize { get; set; }

    /// <summary>
    ///     Km per liter field and property.
    /// </summary>
    public double KmPerLiter { get; set; }

    /// <summary>
    ///     Drivers license Enum, field and property.
    /// </summary>
    public LicenseType DriversLicense { get; set; }

    /// <summary>
    ///     NFuel type Enum, field and property.
    /// </summary>
    public static FuelType FuelType { get; set; }

    /// <summary>
    ///     Energy class Enum, field and property.
    /// </summary>
    public EnergyType EnergyType
    {
        get => EnergyType;
        set => GetEnergyType();
    }
    
    
    # region Constructors
    protected Vehicle(string name,
        double km,
        string registrationNumber,
        int year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelType fuelType)
    {
        // If the engine size is not between 0.7 and 10.0 L, throw an exception.
        if (engineSize is < 0.7 or > 10.0)
            throw new ArgumentOutOfRangeException(nameof(engineSize), engineSize, "Engine size must be between 0.7 and 10.0 L!");

        Name = name;
        Km = km;
        RegistrationNumber = registrationNumber;
        Year = year;
        NewPrice = newPrice;
        HasTowbar = hasTowbar;
        EngineSize = engineSize;
        KmPerLiter = kmPerLiter;
        FuelType = fuelType;

        // TODO: V2 - Add to database and set ID
    }

    # endregion


    /// <summary>
    ///     Energy class is calculated based on year of the car and the efficiency in km/L.
    /// </summary>
    /// <returns>
    ///     Returns the energy class in EnergyType (A,B,C,D)
    /// </returns>
    private EnergyType GetEnergyType()
    {
        // If the car is older than 2010, the energy class is calculated differently.
        if (Year < 2010)
        {
            // If the car is diesel, the energy class is calculated differently.
            if (FuelType == FuelType.Diesel)
            {
                return KmPerLiter switch
                {
                    >= 23 => EnergyType.A,
                    >= 18 => EnergyType.B,
                    >= 13 => EnergyType.C,
                    _ => EnergyType.D
                };
            }
            else
            {
                return KmPerLiter switch
                {
                    >= 18 => EnergyType.A,
                    >= 14 => EnergyType.B,
                    >= 10 => EnergyType.C,
                    _ => EnergyType.D
                };
            }
        }
        else
        {
            if (FuelType == FuelType.Diesel)
            {
                return KmPerLiter switch
                {
                    >= 25 => EnergyType.A,
                    >= 20 => EnergyType.B,
                    >= 15 => EnergyType.C,
                    _ => EnergyType.D
                };
            }
            else
            {
                return KmPerLiter switch
                {
                    >= 20 => EnergyType.A,
                    >= 16 => EnergyType.B,
                    >= 12 => EnergyType.C,
                    _ => EnergyType.D
                };
            }
        }
    }

    /// <summary>
    ///     Returns the vehicle in a string with relevant information.
    /// </summary>
    /// <returns>The Vehicle as a string.</returns>
    public new virtual string ToString()
    {
        return $"Name: {Name}\n" +
               $"Km: {Km}\n" +
               $"Registration number: {RegistrationNumber}\n" +
               $"Year: {Year}\n" +
               $"New price: {NewPrice}\n" +
               $"Has towbar: {HasTowbar}\n" +
               $"Engine size: {EngineSize}\n" +
               $"Km per liter: {KmPerLiter}\n" +
               $"Drivers license: {DriversLicense}\n" +
               $"Fuel type: {FuelType}\n" +
               $"Energy type: {EnergyType}\n";
    }
}