namespace Data.Classes.Vehicles;

/// <summary>
///     The dimensions of a vehicle.
/// </summary>
public class Dimensions
{
    /// <summary>
    ///     The dimensions of a vehicle.
    /// </summary>
    /// <param name="id">The ID of the dimensions in the database.</param>
    /// <param name="length">The length of the vehicle in meters.</param>
    /// <param name="width">The width of the vehicle in meters.</param>
    /// <param name="height">The height of the vehicle in meters.</param>
    public Dimensions(int id, double length, double width, double height)
    {
        DimensionsId = id;
        Length = length;
        Width = width;
        Height = height;
    }

    /// <summary>
    ///     The ID of the dimensions in the database.
    /// </summary>
    public int DimensionsId { get; set; }

    /// <summary>
    ///     The length of the vehicle in meters.
    /// </summary>
    public double Length { get; set; }

    /// <summary>
    ///     The width of the vehicle in meters.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    ///     The height of the vehicle in meters.
    /// </summary>
    public double Height { get; set; }

    public override string ToString()
    {
        return $"DimensionsId: {DimensionsId}\n" +
               $"Length: {Length}\n" +
               $"Width: {Width}\n" +
               $"Height: {Height}";
    }
}