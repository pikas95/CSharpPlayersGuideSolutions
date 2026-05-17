internal class InputHandler
{
    public int AskContractorToHire(Contractor[] contractorStore)
    {
        Console.Write("Which contractor you wish to hire? ");

        // TODO: implement TryParse
        int input = Convert.ToInt32(Console.ReadLine());

        while (!InputValid(input))
        {
            NoSuchOption();
            input = Convert.ToInt32(Console.ReadLine());
        }

        return input;

        bool InputValid(int input)
        {
            if (input >= 0 && input <= contractorStore.Length)
                return true;
            return false;
        }
    }
    public void WaitForEnter(string text)
    {
        Console.Write(text);
        Console.ReadLine();
    }

    private void NoSuchOption() => Console.Write("No such option. Again: ");
}