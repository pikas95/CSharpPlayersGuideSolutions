int[] array = new int[] { 4, 51, -7, 13, -99, 15, -8, 45, 90 };
int currentSmallest = int.MaxValue;

foreach(int value in array)
    if (value < currentSmallest)
        currentSmallest = value;

Console.WriteLine($"Smallest value: {currentSmallest}");

int total = 0;
for (int index = 0; index < array.Length; index++)
    total += array[index];

float average = (float)total / array.Length;
Console.WriteLine($"Average: {average}");