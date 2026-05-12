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
                rooms[row, col] = random.Next(0, 5) switch
                {
                    0 => SetEnemy(),
                    1 => SetWeapon(),
                    _ => SetEmpty()
                };
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

        Room SetWeapon()
        {
            return random.Next(0, 3) switch
            {
                0 => new Room(new Weapon("Legendary Katana", 6, 12)),
                1 => new Room(new Weapon("Epic Bloody Dagger", 5, 8)),
                _ => new Room(new Weapon("Steel Sword", 3, 7))
            };
        }

        Room SetEmpty() => new Room("Empty room", "Creepy empty room");

        return rooms;
    }
}