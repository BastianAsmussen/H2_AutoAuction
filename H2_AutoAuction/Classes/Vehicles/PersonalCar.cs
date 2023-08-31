namespace H2_AutoAuction.Classes.Vehicles;

public abstract class PersonalCar : Vehicle
{
    protected PersonalCar(
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
        TrunkDimensionsStruct trunkDimensions)
        : base(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType)
    {
        NumberOfSeat = numberOfSeat;
        TrunkDimensions = trunkDimensions;
    }

    /// <summary>
    ///     Number of seat property
    /// </summary>
    public ushort NumberOfSeat { get; set; }

    /// <summary>
    ///     Trunk dimensions property and struct
    /// </summary>
    public TrunkDimensionsStruct TrunkDimensions { get; set; }

    /// <summary>
    ///     Engine size property.
    ///     must be between 0.7 and 10.0 L or cast an out of range execution.
    /// </summary>
    public override double EngineSize
    {
        get => EngineSize;
        set
        {
            //TODO: V13 - EngineSize: must be between 0.7 and 10.0 L or cast an out of range execution.
            throw new NotImplementedException();

            EngineSize = value;
        }
    }

    /// <summary>
    ///     Returns the PersonalCar in a string with relevant information.
    /// </summary>
    public override string ToString()
    {
        //TODO: V15 - ToString for PersonalCar
        throw new NotImplementedException();
    }

    public readonly struct TrunkDimensionsStruct
    {
        public TrunkDimensionsStruct(double height, double width, double depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
        }

        public double Height { get; }
        public double Width { get; }
        public double Depth { get; }

        public override string ToString()
        {
            return $"(Height: {Height}, Width: {Width}, Depth: {Depth})";
        }
    }
}