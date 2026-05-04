new Map().Display();

internal class Map
{
    private readonly Room[,] _rooms;
    public Map()
    {
        int dungeonSize = new Random().Next(8, 16);
        _rooms = RoomGenerator.Generate(dungeonSize);
    }

    public void Display()
    {
        for (int row = 0; row < _rooms.GetLength(0); row++)
        {
            for (int col = 0; col < _rooms.GetLength(1); col++)
            {
                DisplayRoomInstance(row, col);
                Console.WriteLine();
                ShowRoomsNearby(row, col);
            }
        }
    }

    public void ShowRoomsNearby(int row, int col)
    {
        int[] dRow = { -1, 1, 0, 0 };
        int[] dCol = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newRow = row + dRow[i];
            int newCol = col + dCol[i];

            if (newRow >= 0 && newRow < _rooms.GetLength(0) &&
                newCol >= 0 && newCol < _rooms.GetLength(1))
            {
                DisplayRoomInstance(newRow, newCol);
            }
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public void DisplayRoomInstance(int row, int col) => Console.WriteLine($"{_rooms[row, col].Name} {_rooms[row, col].Description}");

}
internal class Room
{
    public string Name { get; }
    public string Description { get; }
    public Enemy? _enemy = null;
    public Item? _item = null;
    public Room(Enemy enemy)
    {
        _enemy = enemy;
        Name = $"{enemy.Name} cave";
        Description = $"This room is a {Name}";
    }
    public Room(Item item)
    {
        _item = item;
        Name = $"{item.Name} tomb";
        Description = $"Forgotten treasure tomb of {item.Name}";
    }
    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }

}

internal static class RoomGenerator
{
    public static Room[,] Generate(int dungeonSize)
    {
        Random random = new Random();
        Room[,] rooms = new Room[dungeonSize / 2, dungeonSize / 2];

        for (int row = 0; row < dungeonSize / 2; row++)
        {
            for (int col = 0; col < dungeonSize / 2; col++)
            {
                switch (random.Next(0, 5))
                {
                    case 0: rooms[row, col] = SetEnemy(); break;
                    case 1: rooms[row, col] = SetItem(); break;
                    default: rooms[row, col] = SetEmpty(); break;
                }
            }

        }

        Room SetEnemy()
        {
            return random.Next(0, 3) switch
            {
                0 => new Room(new Enemy("Troll", 20, 4, 8)),
                1 => new Room(new Enemy("Zombie", 12, 3, 6)),
                _ => new Room(new Enemy("Skeleton", 8, 1, 4))
            };
        }

        Room SetItem()
        {
            return random.Next(0, 3) switch
            {
                0 => new Room(new Item("Legendary Katana", 6, 12)),
                1 => new Room(new Item("Epic Bloody Dagger", 5, 8)),
                _ => new Room(new Item("Steel Sword", 3, 7))
            };
        }

        Room SetEmpty() => new Room("Empty room", "This room is empty. You got lucky?");

        return rooms;
    }
}

internal class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    private int _damageMin;
    private int _damageMax;
    public bool IsDead { get; private set; } = false;

    public Enemy(string name, int health, int damageMin, int damageMax)
    {
        Name = name;
        Health = health;
        _damageMin = damageMin;
        _damageMax = damageMax;
    }
}

internal class Item
{
    public string Name { get; }
    public int DamageMin { get; }
    public int DamageMax { get; }

    public Item(string name, int damageMin, int damageMax)
    {
        Name = name;
        DamageMin = damageMin;
        DamageMax = damageMax;
    }
}