new LedgerProgram().Run();

internal class LedgerProgram
{
    private Ledger _ledger = new Ledger();

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            switch (UserInput())
            {
                case 1: AddTransaction(); break;
                case 2: ViewAll(); break;
                case 3: Summary(); break;
                case 4: BiggestTransaction(); break;
                case 5: return;
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("[1] Add Transaction   " +
                          "[2] View All   " +
                          "[3] Summary   " +
                          "[4] Biggest Transaction   " +
                          "[5] Quit");
    }

    private static int UserInput()
    {
        string input = Console.ReadLine()!;
        while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5")
        {
            Console.WriteLine("There is no such option. Try again.");
            input = Console.ReadLine()!;
        }
        return Convert.ToInt32(input);
    }

    private void AddTransaction()
    {
        Console.Clear();

        if (_ledger.Add(new Transaction(GetTransactionType(), GetDescription(), GetAmount())))
            Console.WriteLine("Transaction added successfully!");
        else 
            Console.WriteLine("Transaction can not be added. Contact support.");
        BackToMenu();

        TransactionType GetTransactionType()
        {
            Console.WriteLine($"[1] {TransactionType.Income} [2] {TransactionType.Expense} [3] {TransactionType.Transfer}");
            Console.Write("Pick a transaction type: ");
            string input = Console.ReadLine()!;

            while (input != "1" && input != "2" && input != "3")
            {
                Console.WriteLine("There's no such option.");
                Console.Write("Pick a transaction type:");
                input = Console.ReadLine()!;
            }

            return input switch
            {
                "1" => TransactionType.Income,
                "2" => TransactionType.Expense,
                "3" => TransactionType.Transfer
            };
        }

        string GetDescription()
        {
            Console.WriteLine();
            Console.Write("Transaction description: ");
            string input = Console.ReadLine()!;
            
            while (input == "" || input == null)
            {
                Console.WriteLine("Description can not be empty.");
                Console.Write("Transaction description: ");
                input = Console.ReadLine()!;
            }
            return input;
        }

        decimal GetAmount()
        {
            Console.WriteLine();
            Console.Write("Transaction amount (gp): ");
            decimal input = Convert.ToDecimal(Console.ReadLine() ?? "0"); // I see the critical issue, it will be addressed in later chapters
            
            while (input <= 0)
            {
                Console.WriteLine("Amount must be a positive number.");
                Console.Write("Transaction amount (gp): ");
                input = Convert.ToDecimal(Console.ReadLine() ?? "0"); // I see the critical issue, it will be addressed in later chapters
            }

            Console.Clear(); // clears the console before showing if transaction added or not

            return input;
        }
    }

    private void ViewAll()
    {
        Console.Clear();

        if (_ledger.Transactions.Count > 0)
        {
            Console.WriteLine("Transaction history:");
            for (int i = 0; i < _ledger.Transactions.Count; i++)
                Console.WriteLine(_ledger.Transactions[i]);
        }
        else
            Console.WriteLine("No transaction history.");
        BackToMenu();
    }

    private void Summary()
    {
        Console.Clear();

        (decimal income, decimal expenses, decimal balance) = _ledger.GetSummary();
        Console.WriteLine("All time summary:");
        Console.WriteLine($"[Income]: {income} gp, [Expenses]: {expenses} gp, [Balance]: {balance} gp.");
        BackToMenu();
    }

    private void BiggestTransaction()
    {
        Console.Clear();

        if (_ledger.Transactions.Count > 0)
        {
            Transaction largest = _ledger.GetLargest();
            Console.WriteLine("Biggest transaction:");
            Console.WriteLine(largest);
        }
        else 
            Console.WriteLine("No transaction history.");
        BackToMenu();
    }

    private static void BackToMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Press enter to go back to menu.");
        Console.ReadLine();
        Console.Clear();
    }
}

internal class Ledger
{
    public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public bool Add(Transaction newTransaction)
    {
        if (newTransaction.Amount > 0)
        {
            Transactions.Add(newTransaction);
            return true;
        }
        return false;
    }

    public (decimal income, decimal expenses, decimal balance) GetSummary() // all transactions summary
    {
        decimal income = 0, expenses = 0;

        for (int i = 0; i < Transactions.Count; i++)
        {
            if (Transactions[i].TransactionType == TransactionType.Income)
                income += Transactions[i].Amount;
            else
                expenses += Transactions[i].Amount;
        }

        decimal balance = income - expenses;
        return (income, expenses, balance);
    }

    public decimal GetSummary(TransactionType filter) // selected summary
    {
        decimal filteredSummary = 0;

        for (int i = 0; i < Transactions.Count; i++)
            if (Transactions[i].TransactionType == filter)
                filteredSummary += Transactions[i].Amount;

        return filteredSummary;
    }

    public Transaction GetLargest()
    {
        Transaction largest = new Transaction(); // largest.Amount is 0

        for (int i = 0; i < Transactions.Count; i++)
            if (largest.Amount < Transactions[i].Amount)
                largest = Transactions[i];

        return largest;
    }
}

internal class Transaction
{
    public TransactionType TransactionType { get; }
    public string Description { get; } = "None";
    public decimal Amount { get; } = 0;

    public Transaction() { }

    public Transaction(TransactionType transactionType, string description, decimal amount)
    {
        TransactionType = transactionType;
        Description = description;
        Amount = amount;
    }

    public override string ToString()
    {
        string sign = TransactionType == TransactionType.Income ? "+" : "-";
        return $"[{TransactionType}] {Description}: {sign}{Amount} gp.";
    }
}
internal enum TransactionType { Income, Expense, Transfer }