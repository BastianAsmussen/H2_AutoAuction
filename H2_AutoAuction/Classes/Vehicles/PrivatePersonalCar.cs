namespace H2_AutoAuction.Classes.Vehicles;

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
        FuelTypeEnum fuelType,
        ushort numberOfSeat,
        TrunkDimensionsStruct trunkDimensions,
        bool hasIsofixFittings)
        : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeat,
            trunkDimensions)
    {
        if (DriversLicense != DriversLicenseEnum.B && !hasIsofixFittings)
            throw new NotImplementedException("DriversLicense is not of type'B' or Doesn't have IsofixFittings ");
        
        HasIsofixFittings = hasIsofixFittings;


        //TODO: V20 - Add to database and set ID
        // throw new NotImplementedException();
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