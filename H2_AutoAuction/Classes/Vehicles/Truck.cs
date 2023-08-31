namespace H2_AutoAuction.Classes.Vehicles;

internal class Truck : HeavyVehicle
{
    public Truck(
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
        double LoadCapacity) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
        fuelType, vehicleDimentions)
    {
        //TODO: V10 - Constructor for Truck, DriversLisence should be CE if the truck has a towbar, otherwise it should be C
        //TODO: V11 - Add to database and set ID
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Engine size proberty
    ///     must be between 4.2 and 15.0 L or cast an out of range exection.
    /// </summary>
    /// <returns>The size the the engine as a double</returns>
    public override double EngineSize
    {
        get => EngineSize;
        set
        {
            //TODO: V10 - EngineSize must be between 4.2 and 15.0 L or cast an out of range exection.
            throw new NotImplementedException();

            EngineSize = value;
        }
    }

    /// <summary>
    ///     Load Capacity field and proberty
    /// </summary>
    public double LoadCapacity { get; set; }

    /// <summary>
    ///     Returns the Truck in a string with relivant information.
    /// </summary>
    public override string ToString()
    {
        //TODO: V12 - ToString for Truck
        throw new NotImplementedException();
    }
}