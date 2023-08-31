namespace H2_AutoAuction.Classes.Vehicles;

public abstract class Vehicle
{
    public enum DriversLisenceEnum
    {
        A,
        B,
        C,
        D,
        BE,
        CE,
        DE
    }

    public enum EnergyClassEnum
    {
        A,
        B,
        C,
        D
    }

    public enum FuelTypeEnum
    {
        Diesel,
        Benzin
    }

    protected Vehicle(string name,
        double km,
        string registrationNumber,
        int year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelTypeEnum fuelType)
    {
        Name = name;
        Km = km;
        RegistrationNumber = registrationNumber;
        Year = year;
        NewPrice = newPrice;
        HasTowbar = hasTowbar;
        EngineSize = engineSize;
        KmPerLiter = kmPerLiter;
        FuelType = fuelType;
        //TODO: V1 - Constructor for Vehicle
        //TODO: V2 - Add to database and set ID
    }

    /// <summary>
    ///     ID field and proberty
    /// </summary>
    public uint ID { get; }

    /// <summary>
    ///     Name field and proberty
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Km field and proberty
    /// </summary>
    public double Km { get; set; }

    /// <summary>
    ///     Registration number field and proberty
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    ///     Year field and proberty
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    ///     New price field and proberty
    /// </summary>
    public decimal NewPrice { get; set; }

    /// <summary>
    ///     Towbar field and proberty
    /// </summary>
    public bool HasTowbar { get; set; }

    /// <summary>
    ///     Engine size field and proberty
    /// </summary>
    public virtual double EngineSize { get; set; }

    /// <summary>
    ///     Km per liter field and proberty
    /// </summary>
    public double KmPerLiter { get; set; }

    /// <summary>
    ///     Drivers lisence Enum, field and proberty
    /// </summary>
    public DriversLisenceEnum DriversLisence { get; set; }

    /// <summary>
    ///     NFuel type Enum, field and proberty
    /// </summary>
    public FuelTypeEnum FuelType { get; set; }

    /// <summary>
    ///     Engery class Enum, field and proberty
    /// </summary>
    public EnergyClassEnum EnergyClass
    {
        get => EnergyClass;
        set => GetEnergyClass();
    }

    /// <summary>
    ///     Engery class is calculated bassed on year of the car and the efficiancy in km/L.
    /// </summary>
    /// <returns>
    ///     Returns the energy class in EnergyClassEnum (A,B,C,D)
    /// </returns>
    private EnergyClassEnum GetEnergyClass()
    {
        //TODO: V4 - Implement GetEnergyClass
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Returns the vehicle in a string with relivant information.
    /// </summary>
    /// <returns>The Veihcle as a string</returns>
    public new virtual string ToString()
    {
        //TODO: V3 - Vehicle tostring
        throw new NotImplementedException();
    }
}