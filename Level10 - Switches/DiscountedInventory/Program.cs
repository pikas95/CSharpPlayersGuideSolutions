Console.WriteLine("The following items are available:");
Console.WriteLine("1 – Rope\r\n2 – Torches\r\n3 – Climbing Equipment\r\n4 – Clean Water\r\n5 – Machete\r\n6 – Canoe\r\n7 – Food Supplies");
Console.Write("What number do you want to see the price of? ");
byte itemNumber = Convert.ToByte(Console.ReadLine());
string itemName;
int price;
switch (itemNumber)
{
    case 1:
        itemName = "Rope";
        price = 10;
        break;
    case 2:
        itemName = "Torches";
        price = 16;
        break;
    case 3:
        itemName = "Climbing Equipment";
        price = 24;
        break;
    case 4:
        itemName = "Clean Water";
        price = 2;
        break;
    case 5:
        itemName = "Machete";
        price = 20;
        break;
    case 6:
        itemName = "Canoe";
        price = 200;
        break;
    case 7:
        itemName = "Food Supplies";
        price = 2;
        break;
    default:
        itemName = "There is no such item";
        price = -1;
        break;
};
Console.Write("Who is the buyer? ");
bool vip = Console.ReadLine() == "RG";
if (price != -1)
{
    if (vip)
        Console.WriteLine($"{itemName} costs {price / 2} gold.");
    else
        Console.WriteLine($"{itemName} costs {price} gold.");
}