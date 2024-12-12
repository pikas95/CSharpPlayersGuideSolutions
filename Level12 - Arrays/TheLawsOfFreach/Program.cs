int[] array = new int[] { 4, 51, -7, 13, -99, 15, -8, 45, 90 };
int currentSmallest = int.MaxValue; // Start higher than anything in the array.
int total = 0;
foreach (int index in array)
{
    if (index < currentSmallest)
        { currentSmallest = index; }
    total += index;
}
Console.WriteLine(currentSmallest);
float average = (float)total / array.Length;
Console.WriteLine(average);