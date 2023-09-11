namespace Data.Classes.Vehicles;

public class Truck : HeavyVehicle
{
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

    #region Constructor
    public Truck(
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
        double loadCapacity) : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter,
        fuelType, vehicleDimensionses)
    {
        LoadCapacity = loadCapacity;
        DriversLicense = hasTowbar ? LicenseType.CE : LicenseType.C;
        //TODO: V11 - Add to database and set ID
    }
    #endregion

    /// <summary>
    ///     Returns the Truck in a string with relivant information.
    /// </summary>
    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"LoadCapacity: {LoadCapacity}\n";
    }
}