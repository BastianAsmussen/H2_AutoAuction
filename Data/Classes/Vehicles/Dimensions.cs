namespace Data.Classes.Vehicles;

public class Dimensions
{
    public Dimensions(double height, double width, double depth)
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