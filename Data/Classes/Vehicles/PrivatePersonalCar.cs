namespace Data.Classes.Vehicles;

public class PrivatePersonalCar : PersonalCar
{
    public PrivatePersonalCar(
        string name,
        double km,
        string registrationNumber,
        ushort year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelType fuelType,
        ushort numberOfSeat,
        Dimensions trunkDimensionses,
        bool hasIsofixFittings)
        : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeat,
            trunkDimensionses, LicenseType.B)
    {
        if (DriversLicense != LicenseType.B && !hasIsofixFittings)
            throw new ArgumentOutOfRangeException(nameof(hasIsofixFittings), hasIsofixFittings,
                "Drivers license must be B and hasIsofixFittings must be true!");

        // TODO: V20 - Add to database and set ID

        HasIsofixFittings = hasIsofixFittings;
    }

    /// <summary>
    ///     Isofix Fittings property.
    /// </summary>
    public bool HasIsofixFittings { get; set; }

    /// <summary>
    ///     Returns the PrivatePersonalCar in a string with relevant information.
    /// </summary>
    public override string ToString()
    {
        return $"{base.ToString()}" +
               $"HasIsofixFittings: {HasIsofixFittings}\n";
    }
}