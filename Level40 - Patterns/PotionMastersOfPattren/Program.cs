new PotionMaker().Run();
public class PotionMaker
{
    private PotionType potion;
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine($"Current Potion: {potion}");
            Console.WriteLine($"Choose Ingredient: Stardust, Snake Venom, Dragon Breath, Shadow Glass, Eyeshine Gem.");

            Ingredient ingredient = PickIngredient();
            potion = MakePotion(ingredient);

            Console.WriteLine($"You made {potion} potion.");
            if (potion == PotionType.Ruined)
            {
                Console.WriteLine("You ruined the potion.. Start over with water.");
                potion = PotionType.Water;
                continue;
            }
            Console.WriteLine("Press Enter to continue brewing potion.");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
                return;
        }
    }
    private Ingredient PickIngredient()
    {
        Ingredient ingredient;
        while (true)
        {
            string choice = Console.ReadLine()!;
            ingredient = choice switch
            {
                "Stardust" => Ingredient.Stardust,
                "Snake Venom" => Ingredient.SnakeVenom,
                "Dragon Breath" => Ingredient.DragonBreath,
                "Shadow Glass" => Ingredient.ShadowGlass,
                "Eyeshine Gem" => Ingredient.EyeshineGem,
                _ => Ingredient.None
            };
            if (ingredient == Ingredient.None)
                Console.Write("There is no such Ingredient or you typed it wrong. Try again: ");
            else return ingredient;
        }
    }
    private PotionType MakePotion(Ingredient ingredient)
    {
        return (potion, ingredient) switch
        {
            (PotionType.Water, Ingredient.Stardust) => PotionType.Elixir,
            (PotionType.Elixir, Ingredient.SnakeVenom) => PotionType.Poison,
            (PotionType.Elixir, Ingredient.DragonBreath) => PotionType.Flying,
            (PotionType.Elixir, Ingredient.ShadowGlass) => PotionType.Invisibility,
            (PotionType.Elixir, Ingredient.EyeshineGem) => PotionType.NightSight,
            (PotionType.NightSight, Ingredient.ShadowGlass) => PotionType.Cloudy,
            (PotionType.Invisibility, Ingredient.EyeshineGem) => PotionType.Cloudy,
            (PotionType.Cloudy, Ingredient.Stardust) => PotionType.Wraith,
            _ => PotionType.Ruined
        };
    }
}
public enum PotionType { Water, Elixir, Poison, Flying, Invisibility, NightSight, Cloudy, Wraith, Ruined }
public enum Ingredient { None, Stardust, SnakeVenom, DragonBreath, ShadowGlass, EyeshineGem }