namespace Data.Classes.Vehicles;

/// <summary>
///     The dimensions of a vehicle.
/// </summary>
public class Dimensions
{
    /// <summary>
    ///     The ID of the dimensions in the database.
    /// </summary>
    public uint DimensionsId { get; set; }

    /// <summary>
    ///     The length of the vehicle in meters.
    /// </summary>
    public float Length { get; set; }

    /// <summary>
    ///     The width of the vehicle in meters.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    ///     The height of the vehicle in meters.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    ///     The dimensions of a vehicle.
    /// </summary>
    /// <param name="id">The ID of the dimensions in the database.</param>
    /// <param name="length">The length of the vehicle in meters.</param>
    /// <param name="width">The width of the vehicle in meters.</param>
    /// <param name="height">The height of the vehicle in meters.</param>
    public Dimensions(uint id, float length, float width, float height)
    {
        DimensionsId = id;
        Length = length;
        Width = width;
        Height = height;
    }

    public override string ToString() =>
        $"DimensionsId: {DimensionsId}\n" +
        $"Length: {Length}\n" +
        $"Width: {Width}\n" +
        $"Height: {Height}";
}
