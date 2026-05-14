while (true)
{
    Console.Write("Password: ");

    if (PasswordValidator.IsValid(Console.ReadLine()))
        Console.WriteLine("That password is valid.");
    else 
        Console.WriteLine("That password is invalid.");
}

internal static class PasswordValidator
{

    public static bool IsValid(string password)
    {
        if (Length(password) &&
            OneUpperLowerNumber(password) &&
            !Contains(password, 'T') &&
            !Contains(password, '&'))
            return true;

        return false;
    }

    private static bool Length(string password)
    {
        if (password.Length >= 6 && password.Length <= 13)
            return true;
        return false;
    }

    private static bool OneUpperLowerNumber(string password)
    {
        bool upper = false, lower = false, number = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
                upper = true;
            else if (char.IsLower(c))
                lower = true;
            else if (char.IsDigit(c))
                number = true;
        }

        if (upper && lower && number)
            return true;
        return false;
    }

    private static bool Contains(string password, char letter)
    {
        foreach (char c in password)
            if (c == letter)
                return true;

        return false;
    }
}