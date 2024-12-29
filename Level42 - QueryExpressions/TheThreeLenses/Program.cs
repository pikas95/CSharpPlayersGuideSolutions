int[] array = [1, 9, 2, 8, 3, 7, 4, 6, 5];

var procedural = EvenProcedural(array);
foreach (int item in procedural)
    Console.WriteLine(item);
Console.WriteLine();

var keywordQuery = EvenKeywordQuery(array);
foreach (int item in keywordQuery)
    Console.WriteLine(item);
Console.WriteLine();

var methodQuery = EvenMethodQuery(array);
foreach (int item in methodQuery)
    Console.WriteLine(item);

IEnumerable<int> EvenProcedural(int[] array)
{
    Array.Sort(array);
    int evenNumberCount = 0;
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] % 2 != 0)
        {
            for (int j = i; j < array.Length - 1; j++)
                array[j] = array[j + 1];
            if (array[i] % 2 == 0)
                evenNumberCount++;
        }
    }
    int[] fixedArray = new int[evenNumberCount];
    for (int i = 0; i < evenNumberCount; i++)
        fixedArray[i] = array[i] * 2;
    return fixedArray;
}
IEnumerable<int> EvenKeywordQuery(int[] array)
{
    return from a in array
           where a % 2 == 0
           orderby a
           select a * 2;
}
IEnumerable<int> EvenMethodQuery(int[] array)
{
    return array
                .Where(a => a % 2 == 0)
                .OrderBy(a => a)
                .Select(a => a * 2);
}