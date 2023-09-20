﻿namespace Data.Classes.Vehicles;

/// <summary>
///     The vehicle class is the base class for all vehicles.
/// </summary>
public class Vehicle
{
    /// <summary>
    ///     The ID of the vehicle in the database.
    /// </summary>
    public uint VehicleId { get; set; }

    /// <summary>
    ///     The name of the vehicle.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The number of kilometers the vehicle has driven.
    /// </summary>
    public float Km { get; set; }

    /// <summary>
    ///     The registration number of the vehicle.
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    ///     The year the vehicle was manufactured.
    /// </summary>
    public ushort Year { get; set; }

    /// <summary>
    ///     Whether the vehicle has a towbar or not.
    /// </summary>
    public bool HasTowbar { get; set; }

    /// <summary>
    ///     The vehicles type of drivers license.
    /// </summary>
    public LicenseType LicenseType { get; set; }

    /// <summary>
    ///     The size of the engine.
    /// </summary>
    public float EngineSize { get; set; }

    /// <summary>
    ///     How many kilometers the vehicle drives per liter.
    /// </summary>
    public float KmPerLiter { get; set; }

    /// <summary>
    ///     What type of fuel the vehicle uses.
    /// </summary>
    public FuelType FuelType { get; set; }

    /// <summary>
    ///     The energy class of a vehicle.
    /// </summary>
    public EnergyType EnergyClass { get; set; }

    /// <summary>
    ///     The constructor for the vehicle class.
    /// </summary>
    /// <param name="id">The ID of the vehicle in the database.</param>
    /// <param name="name">The name of the vehicle.</param>
    /// <param name="km">The number of kilometers the vehicle has driven.</param>
    /// <param name="registrationNumber">The registration number of the vehicle.</param>
    /// <param name="year">The year the vehicle was manufactured.</param>
    /// <param name="hasTowbar">Whether the vehicle has a towbar or not.</param>
    /// <param name="licenseType">The vehicles type of drivers license.</param>
    /// <param name="engineSize">The size of the engine.</param>
    /// <param name="kmPerLiter">How many kilometers the vehicle drives per liter.</param>
    /// <param name="fuelType">What type of fuel the vehicle uses.</param>
    /// <param name="energyClass">The energy class of a vehicle.</param>
    public Vehicle(uint id, string name, float km, string registrationNumber, ushort year, bool hasTowbar, LicenseType licenseType, float engineSize, float kmPerLiter, FuelType fuelType, EnergyType energyClass)
    {
        VehicleId = id;
        Name = name;
        Km = km;
        RegistrationNumber = registrationNumber;
        Year = year;
        HasTowbar = hasTowbar;
        LicenseType = licenseType;
        EngineSize = engineSize;
        KmPerLiter = kmPerLiter;
        FuelType = fuelType;
        EnergyClass = energyClass;
    }

    /// <summary>
    ///     Calculates the energy class of a vehicle based on the year and fuel type.
    /// </summary>
    /// <returns>The energy class of the vehicle.</returns>
    public EnergyType GetEnergyClass()
    {
        if (Year < 2010)
        {
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

        // Return a default value if none of the conditions match.
        return EnergyType.D;
    }

    public override string ToString() =>
        $"Id: {VehicleId}\n" +
        $"Name: {Name}\n" +
        $"Km: {Km}\n" +
        $"Registration Number: {RegistrationNumber}\n" +
        $"Year: {Year}\n" +
        $"Has Towbar: {HasTowbar}\n" +
        $"License Type: {LicenseType}\n" +
        $"Engine Size: {EngineSize}\n" +
        $"Km Per Liter: {KmPerLiter}\n" +
        $"Fuel Type: {FuelType}\n" +
        $"Energy Class: {EnergyClass}";
}
