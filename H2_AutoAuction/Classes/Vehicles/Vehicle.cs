namespace H2_AutoAuction.Classes.Vehicles;

public abstract class Vehicle
{
    public enum DriversLicenseEnum
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
        Benzine
    }

    # region Constructors
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

    # endregion


    /// <summary>
    ///     ID field and property.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    ///     Name field and property.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Km field and property.
    /// </summary>
    public double Km { get; set; }

    /// <summary>
    ///     Registration number field and property.
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    ///     Year field and property.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    ///     New price field and property.
    /// </summary>
    public decimal NewPrice { get; set; }

    /// <summary>
    ///     Towbar field and property.
    /// </summary>
    public bool HasTowbar { get; set; }

    /// <summary>
    ///     Engine size field and property.
    /// </summary>
    public virtual double EngineSize { get; set; }

    /// <summary>
    ///     Km per liter field and property.
    /// </summary>
    public double KmPerLiter { get; set; }

    /// <summary>
    ///     Drivers license Enum, field and property.
    /// </summary>
    public DriversLicenseEnum DriversLicense { get; set; }

    /// <summary>
    ///     NFuel type Enum, field and property.
    /// </summary>
    public FuelTypeEnum FuelType { get; set; }

    /// <summary>
    ///     Energy class Enum, field and property.
    /// </summary>
    public EnergyClassEnum EnergyClass
    {
        get => EnergyClass;
        set => GetEnergyClass();
    }

    /// <summary>
    ///     Energy class is calculated based on year of the car and the efficiency in km/L.
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
    ///     Returns the vehicle in a string with relevant information.
    /// </summary>
    /// <returns>The Vehicle as a string</returns>
    public new virtual string ToString()
    {
        //TODO: V3 - Vehicle tostring
        throw new NotImplementedException();
    }
}