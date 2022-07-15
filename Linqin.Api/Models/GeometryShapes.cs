namespace Linqin.Api.Models;

public enum Shape
{
    Circle,
    Square,
    Triangle,
    Number,
    Letter
}

public enum Color
{
    Red,
    Green,
    Blue
}


public class GeometryShapes
{
    public int Id { get; set; }
    public Shape Shape { get; set; }
    public Color Color { get; set; }
    public int PriorityValue { get; set; }
    public string ImageUrl { get; set; }
}

