namespace Linqin.Api.Models;

public class ShapeModel
{
  public string Shape { get; set; }
  public string Color { get; set; }
  public int PriorityValue { get; set; }

  public override int GetHashCode()
  {
    // return Shape.GetHashCode() ^ Color.GetHashCode() ^ PriorityValue.GetHashCode();
    return HashCode.Combine(Shape.GetHashCode(), Color.GetHashCode(), PriorityValue.GetHashCode());
  }

  public override bool Equals(object? obj)
  {
    if (obj is ShapeModel other)
    {
      return this.Shape == other.Shape && this.Color == other.Color && this.PriorityValue == other.PriorityValue;
    }

    return false;
  }

  // public override bool ==()
  // {

  // }


}

