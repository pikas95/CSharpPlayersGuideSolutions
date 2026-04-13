Console.Write("User 1, enter a number between 0 and 100: ");
int user1 = Convert.ToInt32(Console.ReadLine());
while (user1 < 0 || user1 > 100)
{
    Console.Write("Bad number! Try again: ");
    user1 = Convert.ToInt32(Console.ReadLine());
}
Console.Clear();
Console.WriteLine("User 2, guess the number");
int user2;
do
{
    Console.Write("What is your next guess? ");
    user2 = Convert.ToInt32(Console.ReadLine());
    if (user2 > user1)
        Console.WriteLine($"{user2} is too high.");
    else if (user2 < user1)
        Console.WriteLine($"{user2} is too low.");
}
while (user2 != user1);
Console.WriteLine("You guessed the number!");