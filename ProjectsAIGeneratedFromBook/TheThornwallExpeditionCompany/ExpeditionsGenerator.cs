internal static class ExpeditionsGenerator
{
    private static readonly Random Random = new Random();

    private static readonly string[] Names =
    {
        "The Sunken Vault", "Ashfall Descent", "The Hollow Crown", "Ruins of Vethspire",
        "The Gilded Tomb", "Mistwarden Hold", "The Cinder Expanse", "Drownspire Deep"
    };

    private static readonly string[] Destinations =
    {
        "the Eastern Reaches", "the Frostbound Pass", "the Underkeep", "the Shattered Coast",
        "the Blackroot Forest", "the Forgotten Catacombs", "the Emberlands", "the Veiled Marsh"
    };

    public static List<Expedition> Generate(int size)
    {
        List<Expedition> expeditions = new List<Expedition>();

        for (int i = 0; i < size; i++)
        {
            Difficulty difficulty = (Difficulty)Random.Next(0, 3);
            string name = Names[Random.Next(Names.Length)];
            string destination = Destinations[Random.Next(Destinations.Length)];
            
            expeditions.Add(new Expedition(name, destination, difficulty, GenerateEvents(difficulty)));
        }

        return expeditions;
    }

    private static ExpeditionEvent[] GenerateEvents(Difficulty difficulty)
    {
        int eventCount = difficulty switch
        {
            Difficulty.Easy => 3,
            Difficulty.Medium => 5,
            Difficulty.Hard => 7,
            _ => 3
        };

        ExpeditionEvent[] events = new ExpeditionEvent[eventCount];

        for (int i = 0; i < eventCount; i++)
            events[i] = GenerateEvent(difficulty);

        return events;
    }

    private static ExpeditionEvent GenerateEvent(Difficulty difficulty)
    {
        return (EventType)Random.Next(0, 4) switch
        {
            EventType.Assault => new AssaultEvent(GenerateEnemies(difficulty)),
            EventType.Trap => new TrapEvent(GenerateTrapDamage(difficulty)),
            EventType.Puzzle => new PuzzleEvent(),
            EventType.Treasure => new TreasureEvent(),
            _ => new PuzzleEvent()
        };
    }

    private static Enemy[] GenerateEnemies(Difficulty difficulty)
    {
        int enemyCount = difficulty switch
        {
            Difficulty.Easy => Random.Next(1, 3),
            Difficulty.Medium => Random.Next(2, 4),
            Difficulty.Hard => Random.Next(3, 5),
            _ => 1
        };

        Enemy[] enemies = new Enemy[enemyCount];

        for (int i = 0; i < enemyCount; i++)
            enemies[i] = GenerateEnemy(difficulty);

        return enemies;
    }

    private static Enemy GenerateEnemy(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => Random.Next(0, 2) == 0 ? new Goblin() : new Wolf(),
            Difficulty.Medium => Random.Next(0, 3) switch
            {
                0 => new Goblin(),
                1 => new Wolf(),
                _ => new Thief()
            },
            Difficulty.Hard => Random.Next(0, 4) switch
            {
                0 => new Wolf(),
                1 => new Thief(),
                2 => new Thief(),
                _ => new Wizard()
            },
            _ => new Goblin()
        };
    }

    private static int GenerateTrapDamage(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => Random.Next(5, 11),
            Difficulty.Medium => Random.Next(10, 21),
            Difficulty.Hard => Random.Next(20, 31),
            _ => 10
        };
    }
}