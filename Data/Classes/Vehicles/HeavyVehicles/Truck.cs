namespace Data.Classes.Vehicles.HeavyVehicles;

/// <summary>
///     The truck class is the base class for all trucks.
/// </summary>
public class Truck : HeavyVehicle
{
    /// <summary>
    ///     The ID of the truck in the database.
    /// </summary>
    public int TruckId { get; set; }

    /// <summary>
    ///     The load capacity of the truck in kilograms.
    /// </summary>
    public double LoadCapacity { get; set; }

    /// <summary>
    ///     The heavy vehicle class is the base class for all heavy vehicles.
    /// </summary>
    /// <param name="id">The ID of the truck in the database.</param>
    /// <param name="loadCapacity">The load capacity of the truck in kilograms.</param>
    /// <param name="heavyVehicle">The heavy vehicle the truck is based on.</param>
    public Truck(int id, double loadCapacity, HeavyVehicle heavyVehicle) : base(heavyVehicle.HeavyVehicleId, heavyVehicle.Dimensions, heavyVehicle)
    {
        TruckId = id;
        LoadCapacity = loadCapacity;
    }

    public override string ToString() =>
        $"{base.ToString()}\n" +
        $"TruckId: {TruckId}\n" +
        $"LoadCapacity: {LoadCapacity}";
}
