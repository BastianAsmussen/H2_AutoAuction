namespace Data.Classes.Vehicles.PersonalCars;

/// <summary>
///     The professional personal car class is the base class for all professional personal cars.
/// </summary>
public class ProfessionalPersonalCar : PersonalCar
{
    public ProfessionalPersonalCar(int id, bool hasSafetyBar, double loadCapacity, PersonalCar personalCar) : base(
        personalCar.PersonalCarId, personalCar.NumberOfSeats, personalCar.TrunkDimensions, personalCar)
    {
        ProfessionalPersonalCarId = id;
        HasSafetyBar = hasSafetyBar;
        LoadCapacity = loadCapacity;
    }

    /// <summary>
    ///     The ID of the professional personal car in the database.
    /// </summary>
    public int ProfessionalPersonalCarId { get; set; }

    /// <summary>
    ///     Whether the professional personal car has a safety bar or not.
    /// </summary>
    public bool HasSafetyBar { get; set; }

    /// <summary>
    ///     The load capacity of the professional personal car in kilograms.
    /// </summary>
    public double LoadCapacity { get; set; }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"ProfessionalPersonalCarId: {ProfessionalPersonalCarId}\n" +
               $"HasSafetyBar: {HasSafetyBar}\n" +
               $"LoadCapacity: {LoadCapacity}";
    }
}