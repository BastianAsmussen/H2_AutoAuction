namespace Data.Classes.Vehicles;

public abstract class HeavyVehicle : Vehicle
{
    public HeavyVehicle(
        string name,
        double km,
        string registrationNumber,
        ushort year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelType fuelType,
        Dimensions vehicleDimensionses) : base(name, km, registrationNumber, year, newPrice, hasTowbar,
        engineSize, kmPerLiter, fuelType)
    {
        VehicleDimensionses = vehicleDimensionses;
    }

    /// <summary>
    ///     Vehicle dimensions property and struct
    /// </summary>
    public Dimensions VehicleDimensionses { get; set; }

    /// <summary>
    ///     Returns the HeavyVehicle in a string with relevant information.
    /// </summary>
    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"Vehicle dimensions: {VehicleDimensionses}\n";
    }
}