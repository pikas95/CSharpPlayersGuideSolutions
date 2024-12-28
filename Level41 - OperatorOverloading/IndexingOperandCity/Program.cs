public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate a, BlockOffset b) =>
        new BlockCoordinate(a.Row + b.RowOffset, a.Column + b.ColumnOffset);
    public static BlockCoordinate operator +(BlockCoordinate a, Direction b)
    {
        return b switch
        {
            Direction.North => new BlockCoordinate(a.Row - 1, a.Column),
            Direction.South => new BlockCoordinate(a.Row + 1, a.Column),
            Direction.West  => new BlockCoordinate(a.Row, a.Column - 1),
            Direction.East  => new BlockCoordinate(a.Row, a.Column + 1)
        };
    }
    public int this[int index]
    {
        get
        {
            if (index == 0) return Row;
            else return Column;
        }
    }
}
public record BlockOffset(int RowOffset, int ColumnOffset);
public enum Direction { North, East, South, West }