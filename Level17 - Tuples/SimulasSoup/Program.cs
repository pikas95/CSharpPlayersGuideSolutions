/*Console.WriteLine("Type Options:");
Console.Write("1 - soup\n2 - stew\n3 - gumbo\nPick a number ");
int type = Convert.ToInt32(Console.ReadLine()) - 1;

Console.WriteLine("Main Ingredient Options:");
Console.Write("1 - muschrooms\n2 - chicken\n3 - carrots\n4 - potatoes\nPick a number ");
int mainIngredient = Convert.ToInt32(Console.ReadLine()) - 1;

Console.WriteLine("Seasoning Options:");
Console.Write("1 - spicy\n2 - salty\n3 - sweet\nPick a number ");
int seasoning = Convert.ToInt32(Console.ReadLine()) - 1;
Console.Write($"{(Seasoning)seasoning} {(MainIngredient)mainIngredient} {(Type)type}");
enum Type { Soup, Stew, Gumbo }
enum MainIngredient { Mushrooms, Chicken, Carrots, Potatoes }
enum Seasoning { Spicy, Salty, Sweet }*/

//OLD CODE

(Type type, MainIngredient ingredient, Seasoning seasoning) soup = GetSoup();
Console.WriteLine($"{soup.seasoning} {soup.ingredient} {soup.type}");

(Type, MainIngredient, Seasoning) GetSoup()
{
    Type type = GetType();
    MainIngredient ingredient = GetIngredient();
    Seasoning seasoning = GetSeasoning();
    return (type, ingredient, seasoning);
}
Type GetType()
{
    Console.WriteLine("Available Types: Soup, Stew, Gumbo. Pick one");
    string text = Console.ReadLine();
    return text switch
    {
        "Soup" => Type.Soup,
        "Stew" => Type.Stew,
        "Gumbo" => Type.Gumbo
    };
}

MainIngredient GetIngredient()
{
    Console.WriteLine("Available Ingredients: Mushrooms, Chicken, Carrots, Potatoes. Pick one");
    string text = Console.ReadLine();
    return text switch
    {
        "Mushrooms" => MainIngredient.Mushrooms,
        "Chicken" => MainIngredient.Chicken,
        "Carrots" => MainIngredient.Carrots,
        "Potatoes" => MainIngredient.Potatoes
    };
}

Seasoning GetSeasoning()
{
    Console.WriteLine("Available Seasoning: Spicy, Salty, Sweet. Pick one");
    string text = Console.ReadLine();
    return text switch
    {
        "Spicy" => Seasoning.Spicy,
        "Salty" => Seasoning.Salty,
        "Sweet" => Seasoning.Sweet
    };
}
enum Type { Soup, Stew, Gumbo }
enum MainIngredient { Mushrooms, Chicken, Carrots, Potatoes }
enum Seasoning { Spicy, Salty, Sweet }

/* THIRD
(Type type, MainIngredient mainIngredient, Seasoning seasoning) soup = GetSoup();
Console.WriteLine($"{soup.seasoning} {soup.mainIngredient} {soup.type}");
(Type type, MainIngredient mainIngredient, Seasoning seasoning) GetSoup() => (GetType(), GetMainIngredient(), GetSeasoning());

Type GetType()
{
    Console.WriteLine("Pick type (Soup, Stew, Gumbo): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Soup" => Type.Soup,
        "Stew" => Type.Stew,
        "Gumbo" => Type.Gumbo
    };
}
MainIngredient GetMainIngredient()
{
    Console.WriteLine("Pick main ingredient (Mushrooms, Chicken, Carrots, Potatoes): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Mushrooms" => MainIngredient.Mushrooms,
        "Chicken" => MainIngredient.Chicken,
        "Carrots" => MainIngredient.Carrots,
        "Potatoes" => MainIngredient.Potatoes
    };
}
Seasoning GetSeasoning()
{
    Console.WriteLine("Pick seasoning (Spicy, Salty, Sweet): ");
    string input = Console.ReadLine();
    return input switch
    {
        "Spicy" => Seasoning.Spicy,
        "Salty" => Seasoning.Salty,
        "Sweet" => Seasoning.Sweet
    };
}
enum Type { Soup, Stew, Gumbo }
enum MainIngredient { Mushrooms, Chicken, Carrots, Potatoes }
enum Seasoning { Spicy, Salty, Sweet }*/