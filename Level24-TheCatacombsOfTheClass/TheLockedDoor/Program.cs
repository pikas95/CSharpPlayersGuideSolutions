Console.Write("What should be the passcode of the door? ");
string passcode = Console.ReadLine();
Door door = new Door(passcode);

internal class Door
{
    public DoorState State { get; private set; }
    private string _passcode;
    public Door(string passcode)
    {
        _passcode = passcode;
        State = DoorState.Closed;
    }

    public bool Open()
    {
        if (State != DoorState.Closed) return false;

        State = DoorState.Opened;
        return true;
    }

    public bool Close()
    {
        if (State != DoorState.Opened) return false;

        State = DoorState.Closed;
        return true;
    }
    
    public bool Lock()
    {
        if (State != DoorState.Closed) return false;

        State = DoorState.Locked;
        return true;
    }

    public bool Unlock(string passcode)
    {
        if (State == DoorState.Locked && passcode == _passcode)
        {
            State = DoorState.Closed;
            return true;
        }
        return false;
    }

    public bool ChangePasscode(string currentPasscode, string newPasscode)
    {
        if (currentPasscode != _passcode) return false;

        _passcode = newPasscode;
        return true;
    }
}
internal enum DoorState { Opened, Closed, Locked }