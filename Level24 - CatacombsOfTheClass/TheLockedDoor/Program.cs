Console.Write("Enter passcode for the new door: ");
Door door = new(Console.ReadLine());
Console.Clear();
while (true)
{
    Console.Write($"Door is {door.State}. What do you want to do? (open, close, lock, unlock, change passcode): ");
    string input = Console.ReadLine();
    if (input == "change passcode")
    {
        Console.Write("Enter current passcode: ");
        string currentPasscode = Console.ReadLine();
        Console.Write("Enter new passcode: ");
        string newPasscode = Console.ReadLine();
        if (door.ChangePasscode(currentPasscode, newPasscode))
        {
            Console.Clear();
            Console.WriteLine("You changed the passcode.");
        }
        else
            Console.WriteLine("The current passcode is wrong.");
    }
    else if (input == "close")
    {
        if (!door.Close())
            Console.WriteLine("You can't do that.");
    }
    else if (input == "open")
    {
        if (!door.Open())
            Console.WriteLine("You can't do that.");
    }
    else if (input == "unlock")
    {
        if (door.State == DoorState.Locked)
        {
            Console.Write("Enter Passcode: ");
            string passcode = Console.ReadLine();
            if (door.AttemptUnlockDoor(passcode))
                Console.WriteLine("You unlocked the door.");
            else
                Console.WriteLine("The passcode is wrong.");
        }
        else 
            Console.WriteLine("You can't do that.");
    }
    else if (input == "lock")
    {
        if (!door.Lock())
            Console.WriteLine("You can't do that.");
    }
    else
        Console.WriteLine("Your instructions are unclear.");
}
class Door
{
    public DoorState State { get; private set; } = DoorState.Locked;
    private string Passcode;
    public Door(string passcode)
    {
        this.Passcode = passcode;
    }
    public bool Open()
    {
        if (State == DoorState.Closed)
        {
            State = DoorState.Open;
            return true;
        }
        return false;
    }
    public bool Close()
    {
        if (State == DoorState.Open)
        {
            State = DoorState.Closed;
            return true;
        }
        return false;
    }
    public bool Lock()
    {
        if (State == DoorState.Closed)
        {
            State = DoorState.Locked;
            return true;
        }
        return false;
    }
    public bool AttemptUnlockDoor(string userPasscode)
    {
        if (userPasscode == Passcode)
        {
            State = DoorState.Closed;
            return true;
        }
        return false;
    }
    public bool ChangePasscode(string currentPasscode, string newPasscode)
    {
        if (currentPasscode == Passcode)
        {
            Passcode = newPasscode;
            return true;
        }
        else
            return false;
    }
}
enum DoorState { Locked, Closed, Open }

/* OLD
Console.Write("Enter passcode for the new door: ");
Door door = new(Console.ReadLine());
Console.Clear();
while (true)
{
    Console.Write($"Door is {door.State}. What do you want to do? (open, close, lock, unlock, change passcode): ");
    string input = Console.ReadLine();
    if (input == "change passcode")
    {
        Console.Write("Enter current passcode: ");
        string currentPasscode = Console.ReadLine();
        Console.Write("Enter new passcode: ");
        string newPasscode = Console.ReadLine();
        if (door.ChangePasscode(currentPasscode, newPasscode))
        {
            Console.Clear();
            Console.WriteLine("You changed the passcode.");
        }
        else
            Console.WriteLine("The current passcode is wrong.");
    }
    else if (input == "close")
    {
        if (door.State == DoorState.Open)
            door.Close();
        else
            Console.WriteLine("You can't do that.");
    }
    else if (input == "open")
    {
        if (door.State == DoorState.Closed)
            door.Open();
        else
            Console.WriteLine("You can't do that.");
    }
    else if (input == "unlock")
    {
        if (door.State == DoorState.Locked)
        {
            Console.Write("Enter Passcode: ");
            string passcode = Console.ReadLine();
            if (door.AttemptUnlockDoor(passcode))
                Console.WriteLine("You unlocked the door.");
            else
                Console.WriteLine("The passcode is wrong.");
        }
        else 
            Console.WriteLine("You can't do that.");
    }
    else if (input == "lock")
    {
        if (door.State == DoorState.Closed)
            door.Lock();
        else
            Console.WriteLine("You can't do that.");
    }
    else
        Console.WriteLine("Your instructions are unclear.");
}
class Door
{
    public DoorState State { get; private set; } = DoorState.Locked;
    private string Passcode;
    public Door(string passcode)
    {
        this.Passcode = passcode;
    }
    public void Open() => State = DoorState.Open;
    public void Close() => State = DoorState.Closed;
    public void Lock() => State = DoorState.Locked;
    public bool AttemptUnlockDoor(string userPasscode)
    {
        if (userPasscode == Passcode)
        {
            State = DoorState.Closed;
            return true;
        }
        return false;
    }
    public bool ChangePasscode(string currentPasscode, string newPasscode)
    {
        if (currentPasscode == Passcode)
        {
            Passcode = newPasscode;
            return true;
        }
        else
            return false;
    }
}
enum DoorState { Locked, Closed, Open }*/