namespace Data.Classes.Vehicles;

public abstract class PersonalCar : Vehicle
{
    protected PersonalCar(
        string name,
        double km,
        string registrationNumber,
        ushort year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelType fuelType,
        ushort numberOfSeat,
        Dimensions trunkDimensionses,
        LicenseType driversLicense)
        : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType)
    {
        // If drivers license is not B or BE, throw an exception.
        if (driversLicense is not (LicenseType.B or LicenseType.BE))
            throw new ArgumentOutOfRangeException(nameof(driversLicense), driversLicense,
                "Drivers license must be B or BE!");

        DriversLicense = driversLicense;
        NumberOfSeat = numberOfSeat;
        TrunkDimensionses = trunkDimensionses;
    }

    /// <summary>
    ///     Number of seat property
    /// </summary>
    public ushort NumberOfSeat { get; set; }

    /// <summary>
    ///     Trunk dimensions property and struct
    /// </summary>
    public Dimensions TrunkDimensionses { get; set; }

    /// <summary>
    ///     Engine size property.
    ///     must be between 0.7 and 10.0 L or cast an out of range execution.
    /// </summary>
    public override double EngineSize
    {
        get => base.EngineSize;
        set
        {
            if (value is < 0.7 or > 10.0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Engine size must be between 0.7 and 10.0 L!");

            base.EngineSize = value;
        }
    }

    /// <summary>
    ///     Returns the PersonalCar in a string with relevant information.
    /// </summary>
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"NumberOfSeat: {NumberOfSeat}\n" +
               $"TrunkDimensions: {TrunkDimensionses}\n";
    }
}