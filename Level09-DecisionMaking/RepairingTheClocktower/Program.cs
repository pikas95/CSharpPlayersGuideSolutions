Console.Write("What's the number? ");
if (Convert.ToInt32(Console.ReadLine()) % 2 == 0) // exceptions comes later in the book
    Console.WriteLine("Tick");
else
    Console.WriteLine("Tock");
