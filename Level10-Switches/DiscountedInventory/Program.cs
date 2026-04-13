Console.Write(
    "The following items are available:\n" +
    "1 - Rope\n" +
    "2 - Torches\n" +
    "3 - Climbing Equipment\n" +
    "4 - Clean Water\n" +
    "5 - Machete\n" +
    "6 - Canoe\n" +
    "7 - Food Supplies\n" +
    "What number do you want to see the price of? ");
int number = Convert.ToInt32(Console.ReadLine()); // exceptions are later in this book
Console.Write("What's your name? ");
string userName = Console.ReadLine();
if (number > 0 && number <= 7)
{
    string itemName = number switch
    {
        1 => "Rope",
        2 => "Torches",
        3 => "Climbing Equipment",
        4 => "Clean Water",
        5 => "Machete",
        6 => "Canoe",
        7 => "Food Supplies",
    };
    int itemPrice = itemName switch
    {
        "Rope" => 10,
        "Torches" => 16,
        "Climbing Equipment" => 24,
        "Clean Water" => 2,
        "Machete" => 20,
        "Canoe" => 200,
        "Food Supplies" => 2,
    };
    Console.WriteLine($"{itemName} cost {(userName == "Alex" ? itemPrice / 2 : itemPrice)} gold.");
}
else Console.WriteLine("No such item.");