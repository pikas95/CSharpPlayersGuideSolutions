// FIRST
Chest x = Chest.Locked;
while (true)
{
    if (x == Chest.Locked)
        Console.Write("The chest is locked. What do you want to do? ");
    else if (x == Chest.Closed)
        Console.Write("The chest is unlocked. What do you want to do? ");
    else
        Console.Write("The chest is open. What do you want to do? ");
    string input = Console.ReadLine();
    if (input == "lock" && x == Chest.Closed)
        x = Chest.Locked;
    else if (input == "unlock" && x == Chest.Locked)
        x = Chest.Closed;
    else if (input == "open" && x == Chest.Closed)
        x = Chest.Open;
    else if (input == "close" && x == Chest.Open)
        x = Chest.Closed;
    else
        Console.WriteLine("You can't do that!");
}
enum Chest { Open, Closed, Locked };

/* SECOND
ChestState chest = ChestState.Locked;
while (true)
{
    if (chest == ChestState.Locked)
        Console.Write("The chest is locked. What do you want to do? ");
    if (chest == ChestState.Closed)
        Console.Write("The chest is unlocked. What do you want to do? ");
    if (chest == ChestState.Open)
        Console.Write("The chest is open. What do you want to do? ");
    string input = Console.ReadLine();
    if (input == "lock" && chest == ChestState.Closed)
        chest = ChestState.Locked;
    else if ((input == "unlock" || input == "close") && (chest == ChestState.Locked || chest == ChestState.Open))
        chest = ChestState.Closed;
    else if (input == "open" && chest == ChestState.Closed)
        chest = ChestState.Open;
    else
        Console.WriteLine("Your command is unclear. Try again");

}
enum ChestState { Locked, Closed, Open }*/