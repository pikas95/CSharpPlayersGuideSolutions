internal class Map
{
    private readonly Room[,] _rooms;

    public Map()
    {
        int dungeonSize = new Random().Next(8, 16);
        _rooms = RoomGenerator.Generate(dungeonSize);
    }

    public Room GetRoom(int col, int row) => _rooms[col, row];

    public int EdgeNumber() => _rooms.GetLength(0);
}