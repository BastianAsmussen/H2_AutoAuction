namespace Data.Classes.Vehicles.PersonalCars;

/// <summary>
///     The private personal car class is the base class for all private personal cars.
/// </summary>
public class PrivatePersonalCar : PersonalCar
{
    /// <summary>
    ///     The ID of the private personal car in the database.
    /// </summary>
    public uint PrivatePersonalCarId { get; set; }

    /// <summary>
    ///     Whether the private personal car has ISO fittings or not.
    /// </summary>
    public bool HasIsoFittings { get; set; }

    /// <summary>
    ///     The private personal car class is the base class for all private personal cars.
    /// </summary>
    /// <param name="id">The ID of the private personal car in the database.</param>
    /// <param name="hasIsoFittings">Whether the private personal car has ISO fittings or not.</param>
    /// <param name="personalCar">The personal car the private personal car is based on.</param>
    public PrivatePersonalCar(uint id, bool hasIsoFittings, PersonalCar personalCar) : base(personalCar.PersonalCarId, personalCar.NumberOfSeats, personalCar.TrunkDimensions, personalCar)
    {
        PrivatePersonalCarId = id;
        HasIsoFittings = hasIsoFittings;
    }

    public override string ToString() =>
        $"{base.ToString()}\n" +
        $"PrivatePersonalCarId: {PrivatePersonalCarId}\n" +
        $"HasIsoFittings: {HasIsoFittings}";
}
