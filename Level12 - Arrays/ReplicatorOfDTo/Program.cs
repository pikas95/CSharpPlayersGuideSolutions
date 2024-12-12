int[] firstArray = new int[5];
Console.WriteLine("User, type in 5 separate numbers");
for(int i=0; i<5; i++)
    firstArray[i] = Convert.ToInt32(Console.ReadLine());
int[] secondArray = new int[5];
for(int i=0; i<5; i++)
{
    secondArray[i] = firstArray[i];
    Console.WriteLine($"Index - {i}, First Array - {firstArray[i]}, Second Array - {secondArray[i]}");
}