namespace Data.Classes.Vehicles;

public class Bus : HeavyVehicle
{
    public Bus(
        string name,
        double km,
        string registrationNumber,
        ushort year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelType fuelType,
        Dimensions vehicleDimensionses,
        byte numberOfSeats,
        byte numberOfSleepingSpaces,
        bool hasToilet) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
        fuelType, vehicleDimensionses)
    {
        NumberOfSeats = numberOfSeats;
        NumberOfSleepingSpaces = numberOfSleepingSpaces;
        HasToilet = hasToilet;
        DriversLicense = HasTowbar ? LicenseType.DE : LicenseType.D;

        // TODO: V8 - Add to database and set ID
    }

    /// <summary>
    ///     Engine size property.
    ///     must be between 4.2 and 15.0 L or cast an out of range execution.
    /// </summary>
    public override double EngineSize
    {
        get => base.EngineSize;
        set
        {
            if (value is < 4.2 or > 15.0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Engine size must be between 4.2 and 15.0 L!");

            base.EngineSize = value;
        }
    }

    /// <summary>
    ///     NumberOfSeats property.
    /// </summary>
    public byte NumberOfSeats { get; set; }

    /// <summary>
    ///     NumberOfSleepingSpaces property.
    /// </summary>
    public byte NumberOfSleepingSpaces { get; set; }

    /// <summary>
    ///     Towbar property.
    /// </summary>
    public bool HasToilet { get; set; }

    /// <summary>
    ///     Returns the Bus in a string with relevant information.
    /// </summary>
    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"NumberOfSeats: {NumberOfSeats}\n" +
               $"NumberOfSleepingSpaces: {NumberOfSleepingSpaces}\n" +
               $"HasToilet: {HasToilet}\n";
    }
}