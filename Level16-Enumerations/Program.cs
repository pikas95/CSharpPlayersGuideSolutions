ChestState chestState = ChestState.Locked;
while (true)
{
    Console.Write($"The chest is {chestState}. What do you want to do? ");
    chestState = PlayerChoice(chestState);
}

ChestState PlayerChoice(ChestState state)
{
    string choice = Console.ReadLine();
    while (choice != "unlock" && choice != "lock" && choice != "open" && choice != "close")
    {
        Console.WriteLine("No such command. Try lock, unlock, open or close.");
        Console.Write("What do you want to do? ");
        choice = Console.ReadLine();
    }
    return ChangeChestState(state, choice);
}

ChestState ChangeChestState(ChestState state, string choice)
{
    if (choice == "unlock" && state == ChestState.Locked) return ChestState.Unlocked;
    if (choice == "lock" && state == ChestState.Unlocked) return ChestState.Locked;
    if (choice == "open" && state == ChestState.Unlocked) return ChestState.Opened;
    if (choice == "close" && state == ChestState.Opened) return ChestState.Unlocked;

    Console.Write("You can't do that! ");
    return state;
}

enum ChestState { Locked, Unlocked, Opened };