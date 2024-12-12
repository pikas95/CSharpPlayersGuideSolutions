Console.WriteLine("Write a number");
bool numberIsEven = Convert.ToInt32(Console.ReadLine()) % 2 == 0;
if (numberIsEven)
    Console.WriteLine("Tick");
else
    Console.WriteLine("Tock");