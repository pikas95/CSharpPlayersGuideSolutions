internal class Map
{
    private readonly Room[,] _rooms;
    public int Size { get; }

    public Map()
    {
        Size = new Random().Next(8, 16);
        _rooms = RoomGenerator.Generate(Size);
    }

    public Room GetRoom(int col, int row) => _rooms[col, row];
}