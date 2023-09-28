namespace Data.Classes.Vehicles.HeavyVehicles;

/// <summary>
///     The heavy vehicle class is the base class for all heavy vehicles.
/// </summary>
public class HeavyVehicle : Vehicle
{
    /// <summary>
    ///     The ID of the heavy vehicle in the database.
    /// </summary>
    public int HeavyVehicleId { get; set; }

    /// <summary>
    ///     The dimensions of the vehicle.
    /// </summary>
    public Dimensions Dimensions { get; set; }

    /// <summary>
    ///     The heavy vehicle class is the base class for all heavy vehicles.
    /// </summary>
    /// <param name="id">The ID of the heavy vehicle in the database.</param>
    /// <param name="dimensions">The dimensions of the vehicle.</param>
    /// <param name="vehicle">The vehicle the heavy vehicle is based on.</param>
    public HeavyVehicle(int id, Dimensions dimensions, Vehicle vehicle)
        : base(vehicle.VehicleId, vehicle.Name, vehicle.Km, vehicle.RegistrationNumber, vehicle.Year, vehicle.NewPrice, vehicle.HasTowbar,
            vehicle.LicenseType, vehicle.EngineSize, vehicle.KmPerLiter, vehicle.FuelType, vehicle.EnergyClass)
    {
        HeavyVehicleId = id;
        Dimensions = dimensions;
    }

    public override string ToString() =>
        $"{base.ToString()}\n" +
        $"HeavyVehicleId: {HeavyVehicleId}\n" +
        $"Dimensions: {Dimensions}";
}
