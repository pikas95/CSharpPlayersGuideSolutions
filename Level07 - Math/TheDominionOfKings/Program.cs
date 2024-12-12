Console.WriteLine("How many estates do you have?");
int estate = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("How may duchies do you have?");
int duchies = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("How many provinces do you have?");
int provinces = Convert.ToInt32(Console.ReadLine());
int totalScore = estate + duchies * 3 + provinces * 6;
Console.WriteLine("Total point score: " +  totalScore);