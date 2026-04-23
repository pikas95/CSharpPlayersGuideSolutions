(Food Food, MainIngredient MainIngredient, Seasoning Seasoning) userSoup;
userSoup = (GetFood(), GetMainIngredient(), GetSeasoning());
Console.WriteLine($"{userSoup.Seasoning} {userSoup.MainIngredient} {userSoup.Food}");

Food GetFood()
{
    Console.WriteLine("What food you want to make (Gumbo, Soup, Stew)?");
    while (true)
    {
        string input = Console.ReadLine();
        if (input == "gumbo" || input == "Gumbo") return Food.Gumbo;
        if (input == "soup" || input == "Soup") return Food.Soup;
        if (input == "stew" || input == "Stew") return Food.Stew;
        Console.WriteLine("You don't know how to make that.");
    }
}

MainIngredient GetMainIngredient()
{
    Console.WriteLine("What should be the main ingredient (Mushrooms, Chicken, Carrots, Potatoes)?");
    while (true)
    {
        string input = Console.ReadLine();
        if (input == "mushrooms" || input == "Mushrooms") return MainIngredient.Mushrooms;
        if (input == "chicken" || input == "Chicken") return MainIngredient.Chicken;
        if (input == "carrots" || input == "Carrots") return MainIngredient.Carrots;
        if (input == "potatoes" || input == "Potatoes") return MainIngredient.Potatoes;
        Console.WriteLine("You don't have such ingridient.");
    }
}

Seasoning GetSeasoning()
{
    Console.WriteLine("Lastly, what seasoning (Spicy, Salty, Sweet)?");
    while (true)
    {
        string input = Console.ReadLine();
        if (input == "spicy" || input == "Spicy") return Seasoning.Spicy;
        if (input == "salty" || input == "Salty") return Seasoning.Salty;
        if (input == "sweet" || input == "Sweet") return Seasoning.Sweet;
        Console.WriteLine("You don't have such seasoning");
    }
}

enum Food { Soup, Stew, Gumbo}
enum MainIngredient { Mushrooms, Chicken, Carrots, Potatoes }
enum Seasoning { Spicy, Salty, Sweet}