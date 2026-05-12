internal class Map
{
    private readonly Room[,] _rooms;

    public Map()
    {
        int dungeonSize = new Random().Next(8, 16);
        _rooms = RoomGenerator.Generate(dungeonSize);
    }

    public Room GetRoom(int row, int col) => _rooms[row, col];
    
    public Room[] GetRoomsNearby(int row, int col)
    {
        int[] dRow = { -1, 1, 0, 0 };
        int[] dCol = { 0, 0, -1, 1 };
        Room[] roomsNearby = new Room[4];

        for (int i = 0; i < 4; i++)
        {
            int newRow = row + dRow[i];
            int newCol = col + dCol[i];

            if (newRow >= 0 && newRow < _rooms.GetLength(0) &&
                newCol >= 0 && newCol < _rooms.GetLength(1))
            {
                roomsNearby[i] = GetRoom(newRow, newCol);
            }
        }

        return roomsNearby;
    }

    public int EdgeNumber() => _rooms.GetLength(0);
}