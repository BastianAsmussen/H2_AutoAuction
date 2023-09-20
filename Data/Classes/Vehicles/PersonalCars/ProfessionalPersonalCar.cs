namespace Data.Classes.Vehicles.PersonalCars;

/// <summary>
///     The professional personal car class is the base class for all professional personal cars.
/// </summary>
public class ProfessionalPersonalCar : PersonalCar
{
    /// <summary>
    ///     The ID of the professional personal car in the database.
    /// </summary>
    public uint ProfessionalPersonalCarId { get; set; }

    /// <summary>
    ///     Whether the professional personal car has a safety bar or not.
    /// </summary>
    public bool HasSafetyBar { get; set; }

    /// <summary>
    ///     The load capacity of the professional personal car in kilograms.
    /// </summary>
    public float LoadCapacity { get; set; }

    public ProfessionalPersonalCar(uint id, bool hasSafetyBar, float loadCapacity, PersonalCar personalCar) : base(personalCar.PersonalCarId, personalCar.NumberOfSeats, personalCar.TrunkDimensions, personalCar)
    {
        ProfessionalPersonalCarId = id;
        HasSafetyBar = hasSafetyBar;
        LoadCapacity = loadCapacity;
    }

    public override string ToString() =>
        $"{base.ToString()}\n" +
        $"ProfessionalPersonalCarId: {ProfessionalPersonalCarId}\n" +
        $"HasSafetyBar: {HasSafetyBar}\n" +
        $"LoadCapacity: {LoadCapacity}";
}
