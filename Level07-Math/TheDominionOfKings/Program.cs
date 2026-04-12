Console.WriteLine("How many provinces, duchies and estates you have?");
int score = Convert.ToInt32(Console.ReadLine()) * 6; // 6 points per province
score += Convert.ToInt32(Console.ReadLine()) * 3; // 3 points per duchie
score += Convert.ToInt32(Console.ReadLine()); // 1 point per estate
Console.WriteLine("Player's total score: " +  score);