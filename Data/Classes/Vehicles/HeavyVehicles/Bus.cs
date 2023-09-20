namespace Data.Classes.Vehicles.HeavyVehicles;

/// <summary>
///     The bus class is the base class for all buses.
/// </summary>
public class Bus : HeavyVehicle
{
    /// <summary>
    ///    The ID of the bus in the database.
    /// </summary>
    public uint BusId { get; set; }

    /// <summary>
    ///     The number of seats in the bus.
    /// </summary>
    public byte NumberOfSeats { get; set; }

    /// <summary>
    ///     The number of sleeping places in the bus.
    /// </summary>
    public byte NumberOfSleepingSpaces { get; set; }

    /// <summary>
    ///     Whether the bus has a toilet or not.
    /// </summary>
    public bool HasToilet { get; set; }

    /// <summary>
    ///     The bus class is the base class for all buses.
    /// </summary>
    /// <param name="id">The ID of the bus in the database.</param>
    /// <param name="numberOfSeats">The number of seats in the bus.</param>
    /// <param name="numberOfSleepingSpaces">The number of sleeping places in the bus.</param>
    /// <param name="hasToilet">Whether the bus has a toilet or not.</param>
    /// <param name="heavyVehicle">The heavy vehicle the bus is based on.</param>
    public Bus(uint id, byte numberOfSeats, byte numberOfSleepingSpaces, bool hasToilet, HeavyVehicle heavyVehicle) : base(heavyVehicle.HeavyVehicleId, heavyVehicle.Dimensions, heavyVehicle)
    {
        BusId = id;
        NumberOfSeats = numberOfSeats;
        NumberOfSleepingSpaces = numberOfSleepingSpaces;
        HasToilet = hasToilet;
    }

    public override string ToString() =>
        $"{base.ToString()}\n" +
        $"BusId: {BusId}\n" +
        $"NumberOfSeats: {NumberOfSeats}\n" +
        $"NumberOfSleepingPlaces: {NumberOfSleepingSpaces}\n" +
        $"HasToilet: {HasToilet}";
}
