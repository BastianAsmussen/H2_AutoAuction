namespace Data.Classes.Vehicles;

public class ProfessionalPersonalCar : PersonalCar
{
    public ProfessionalPersonalCar(
        string name,
        double km,
        string registrationNumber,
        ushort year,
        decimal newPrice,
        double engineSize,
        double kmPerLiter,
        FuelType fuelType,
        ushort numberOfSeat,
        Dimensions trunkDimensionses,
        bool hasSafetyBar,
        double loadCapacity,
        LicenseType driversLicense) :
        base(name, km, registrationNumber, year, newPrice, false, engineSize, kmPerLiter, fuelType, numberOfSeat, trunkDimensionses, loadCapacity < 750 ? LicenseType.B : LicenseType.BE)
    {
        DriversLicense = driversLicense;
        HasSafetyBar = hasSafetyBar;
        LoadCapacity = loadCapacity;

        //TODO: V17 - Add to database and set ID
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Safety Bar proberty
    /// </summary>
    public bool HasSafetyBar { get; set; }

    /// <summary>
    ///     Load Capacity proberty
    /// </summary>
    public double LoadCapacity { get; set; }

    /// <summary>
    ///     Returns the ProfessionalPersonalCar in a string with relivant information.
    /// </summary>
    /// <returns>The Veihcle as a string</returns>
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"HasSafetyBar: {HasSafetyBar}\n" +
               $"LoadCapacity: {LoadCapacity}\n";
    }
}