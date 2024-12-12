while (true)
{
    Console.Write("Enter first room coordinates (x, y): ");
    Coordinate first = new Coordinate(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
    Console.Write("Enter second room coordinates (x, y): ");
    Coordinate second = new Coordinate(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
    if (first.IsAdjacent(second._row, second._column))
        Console.WriteLine("Rooms are Adjacent");
    else if (first._row == second._row && first._column == second._column)
        Console.WriteLine("IT'S THE SAME DAMN ROOM!");
    else
        Console.WriteLine("Rooms are not Adjacent");
    Console.WriteLine();
}
public struct Coordinate
{
    public readonly int _row;
    public readonly int _column;
    public Coordinate (int row, int column) { _row = row; _column = column; }
    public bool IsAdjacent(int row, int column)
    {
        if (_row == row && (_column == column + 1 || _column == column - 1))
            return true;
        if (_column == column && (_row == row + 1 || _row == row -1)) 
            return true;
        return false;
    }
}