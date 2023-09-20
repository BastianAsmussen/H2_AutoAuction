namespace Data.Classes.Vehicles.PersonalCars;

/// <summary>
///     The personal car class is the base class for all personal cars.
/// </summary>
public class PersonalCar : Vehicle
{
    /// <summary>
    ///     The ID of the personal car in the database.
    /// </summary>
    public uint PersonalCarId { get; set; }

    /// <summary>
    ///     The number of seats in the personal car.
    /// </summary>
    public byte NumberOfSeats { get; set; }

    /// <summary>
    ///     The dimensions of the trunk.
    /// </summary>
    public Dimensions TrunkDimensions { get; set; }

    /// <summary>
    ///     The personal car class is the base class for all personal cars.
    /// </summary>
    /// <param name="id">The ID of the personal car in the database.</param>
    /// <param name="numberOfSeats">The number of seats in the personal car.</param>
    /// <param name="trunkDimensions">The dimensions of the trunk.</param>
    /// <param name="vehicle">The vehicle the personal car is based on.</param>
    public PersonalCar(uint id, byte numberOfSeats, Dimensions trunkDimensions, Vehicle vehicle) : base(vehicle.VehicleId, vehicle.Name, vehicle.Km, vehicle.RegistrationNumber, vehicle.Year, vehicle.HasTowbar, vehicle.LicenseType, vehicle.EngineSize, vehicle.KmPerLiter, vehicle.FuelType, vehicle.EnergyClass)
    {
        PersonalCarId = id;
        NumberOfSeats = numberOfSeats;
        TrunkDimensions = trunkDimensions;
    }

    public override string ToString() =>
        $"{base.ToString()}\n" +
        $"PersonalCarId: {PersonalCarId}\n" +
        $"NumberOfSeats: {NumberOfSeats}\n" +
        $"TrunkDimensions: {TrunkDimensions}";
}
