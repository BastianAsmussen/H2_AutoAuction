namespace H2_AutoAuction.Classes.Vehicles;

internal class Bus : HeavyVehicle
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
        FuelTypeEnum fuelType,
        VehicleDimensionsStruct vehicleDimensions,
        ushort numberOfSeats,
        ushort numberOfSleepingSpaces,
        bool hasToilet) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
        fuelType, vehicleDimensions)
    {
        NumberOfSeats = numberOfSeats;
        NumberOfSleepingSpaces = numberOfSleepingSpaces;
        HasToilet = hasToilet;
        //TODO: V7 - set constructor and DriversLicense to DE if the car has a towbar or D if not.
        //TODO: V8 - Add to database and set ID
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Engine size property.
    ///     must be between 4.2 and 15.0 L or cast an out of range execution.
    /// </summary>
    public override double EngineSize
    {
        get => EngineSize;
        set
        {
            //V7 - TODO value must be between 4.2 and 15.0 L or cast an out of range execution.
            throw new NotImplementedException();
            EngineSize = value;
        }
    }

    /// <summary>
    ///     NumberOfSeats property.
    /// </summary>
    public ushort NumberOfSeats { get; set; }

    /// <summary>
    ///     NumberOfSeats property.
    /// </summary>
    public ushort NumberOfSleepingSpaces { get; set; }

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