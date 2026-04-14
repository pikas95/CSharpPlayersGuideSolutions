int[] original = new int[5];
Console.WriteLine("User, enter 5 integer numbers:");
for (int i = 0; i < original.Length; i++)
    original[i] = Convert.ToInt32(Console.ReadLine());

int[] copy = new int[original.Length];
for (int i = 0; i < copy.Length; i++)
    copy[i] = original[i];

for (int i = 0; i < copy.Length; i++)
    Console.WriteLine($"{original[i]} {copy[i]}");