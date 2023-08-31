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
        TrunkDimentionsStruct trunkDimentions,
        bool hasIsofixFittings)
        : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeat,
            trunkDimentions)
    {
        //TODO: V19 - PrivatePersonalCar constructor. DriversLicense should be 'B'
        //TODO: V20 - Add to database and set ID
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Isofix Fittings proberty
    /// </summary>
    public bool HasIsofixFittings { get; set; }

    /// <summary>
    ///     Returns the PrivatePersonalCar in a string with relivant information.
    /// </summary>
    public override string ToString()
    {
        //TODO: V21 - ToString for PrivatePersonalCar
        throw new NotImplementedException();
    }
}