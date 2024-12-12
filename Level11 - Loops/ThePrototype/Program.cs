Console.Title = "Pilot";
int user1;
do
{
    Console.Write("User 1, enter a number between 0 and 100: ");
    user1 = Convert.ToInt32(Console.ReadLine());
    if (user1 >= 0 && user1 <= 100)
        break;
    else
        Console.WriteLine("The number is not suitable.");
}
while (true);
Console.Clear();
Console.Title = "Hunter";
Console.WriteLine("User 2, guess the number.");
int user2 = user1 + 1;
while(user2 != user1)
{
    Console.Write("What is your next guess? ");
    user2 = Convert.ToInt32(Console.ReadLine());
    if (user2 > user1)
        Console.WriteLine(user2 + " is too high.");
    else if (user2 < user1)
        Console.WriteLine(user2 + " is too low.");
}
Console.WriteLine("You guessed the number!");