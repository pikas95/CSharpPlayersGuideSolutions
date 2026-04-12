Console.WriteLine("How many chocolate eggs gethered today?");
int eggs = Convert.ToInt32(Console.ReadLine());
int perSister = eggs / 4;
int forDuckbear = eggs % 4;
Console.WriteLine("Each sister gets " + perSister + ".");
Console.WriteLine("Duckbear gets " + forDuckbear + ".");