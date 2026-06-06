Coordinate one = new(0, 1);
Coordinate two = new(1, 1);
Coordinate three = new(0, 2);

Console.WriteLine(one.IsAdjacent(two));
Console.WriteLine(three.IsAdjacent(one));
Console.WriteLine(three.IsAdjacent(two));

internal struct Coordinate
{
    public int Row { get; }
    public int Col { get; }

    public Coordinate(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public bool IsAdjacent(Coordinate coordinate)
    {
        if ((coordinate.Row + 1 == Row && coordinate.Col == Col) ||
            (coordinate.Row - 1 == Row && coordinate.Col == Col) ||
            (coordinate.Row == Row && coordinate.Col + 1 == Col) ||
            (coordinate.Row == Row && coordinate.Col - 1 == Col))
            return true;

        return false;
    }
}