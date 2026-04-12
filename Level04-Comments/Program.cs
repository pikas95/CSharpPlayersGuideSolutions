Console.WriteLine("What kind of thing are we talking about?");
string a = Console.ReadLine(); // a stores thing name
Console.WriteLine("How would you describe it? Big? Azure? Tattered?");
string b = Console.ReadLine(); // b stores description
string c = " of Doom"; // c stores static description
string d = "3000"; /* d stores static version of the thing */
Console.WriteLine("The " + b + " " + a + c + " " + d + "!");