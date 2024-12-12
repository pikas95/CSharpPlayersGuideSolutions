while (true)
{
    Console.Write("Enter a new password: ");
    string? input = Console.ReadLine();
    if (PasswordValidator.Validation(input))
        Console.WriteLine("Password is allowed");
    else
        Console.WriteLine("Password is incorrect");
    
}
class PasswordValidator
{
    public static bool Validation(string userInput)
    {
        byte length = 0, upper = 0, lower = 0, number = 0;
        foreach (char letter in userInput)
        {
            if (char.IsUpper(letter))
            {
                if (letter == 'T')
                    return false;
                upper++;
            }
            else if (char.IsLower(letter))
                lower++;
            else if (char.IsDigit(letter))
                number++;
            if (letter == '&')
                return false;
            length++;
        }
        if (length >= 6 && length <= 13 && upper != 0 && lower != 0 && number != 0)
        {
            return true;
        }
        return false;
    }
}