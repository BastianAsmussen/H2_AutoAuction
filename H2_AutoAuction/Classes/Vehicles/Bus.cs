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
        VehicleDimensionsStruct vehicleDimentions,
        ushort numberOfSeats,
        ushort numberOfSleepingSpaces,
        bool hasToilet) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
        fuelType, vehicleDimentions)
    {
        NumberOfSeats = numberOfSeats;
        NumberOfSleepingSpaces = numberOfSleepingSpaces;
        HasToilet = hasToilet;
        //TODO: V7 - set contructor and DriversLisence to DE if the car has a towbar or D if not.
        //TODO: V8 - Add to database and set ID
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Engine size proberty
    ///     must be between 4.2 and 15.0 L or cast an out of range exection.
    /// </summary>
    public override double EngineSize
    {
        get => EngineSize;
        set
        {
            //V7 - TODO value must be between 4.2 and 15.0 L or cast an out of range exection.
            throw new NotImplementedException();
            EngineSize = value;
        }
    }

    /// <summary>
    ///     NumberOfSeats proberty
    /// </summary>
    public ushort NumberOfSeats { get; set; }

    /// <summary>
    ///     NumberOfSeats proberty
    /// </summary>
    public ushort NumberOfSleepingSpaces { get; set; }

    /// Towbar proberty
    /// </summary>
    public bool HasToilet { get; set; }

    /// <summary>
    ///     Returns the Bus in a string with relivant information.
    /// </summary>
    public override string ToString()
    {
        //TODO: V9 - Tostring for bus
        throw new NotImplementedException();
    }
}