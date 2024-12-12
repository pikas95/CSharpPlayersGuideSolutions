Console.WriteLine("How many chocalate eggs were gathered today?");
int eggs = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Each sister would get " + eggs / 3);
Console.WriteLine("The Duckbear would get " + eggs % 3);