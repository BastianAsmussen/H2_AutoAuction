namespace H2_AutoAuction.Classes.Vehicles;

public abstract class HeavyVehicle : Vehicle
{
    public HeavyVehicle(
        string name,
        double km,
        string registrationNumber,
        ushort year,
        decimal newPrice,
        bool hasTowbar,
        double engineSize,
        double kmPerLiter,
        FuelTypeEnum fuelType,
        VehicleDimensionsStruct vehicleDimensions) : base(name, km, registrationNumber, year, newPrice, hasTowbar,
        engineSize, kmPerLiter, fuelType)
    {
        VehicleDimensions = vehicleDimensions;
    }

    /// <summary>
    ///     Vehicle dimensions property and struct
    /// </summary>
    public VehicleDimensionsStruct VehicleDimensions { get; set; }

    /// <summary>
    ///     Returns the HeavyVehicle in a string with relevant information.
    /// </summary>
    public override string ToString()
    {
        return @$"
            {base.ToString()}
            Vehicle dimensions: {VehicleDimensions}";
    }

    /// <summary>
    ///     The dimensions of the vehicle i meters.
    /// </summary>
    public readonly struct VehicleDimensionsStruct
    {
        public VehicleDimensionsStruct(double height, double weight, double length)
        {
            Height = height;
            Weight = weight;
            Length = length;
        }

        public double Height { get; }
        public double Weight { get; }
        public double Length { get; }

        public override string ToString()
        {
            return $"(Height: {Height}, Weight: {Weight}, Depth: {Length})";
        }
    }
}