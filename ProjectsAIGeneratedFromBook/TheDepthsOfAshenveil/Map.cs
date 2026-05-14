internal class Map
{
    private readonly Room[,] _rooms;
    public int ArraySize { get; }

    public Map()
    {
        int dungeonSize= new Random().Next(8, 16);
        _rooms = RoomGenerator.Generate(dungeonSize);
        ArraySize = dungeonSize / 2;
    }

    public Room GetRoom(int col, int row) => _rooms[col, row];
}