Robot robot = new Robot();
string input = Console.ReadLine()!;
while (input != "stop")
{
    switch (input)
    {
        case "on":
            robot.Commands.Add(new OnCommand());
            break;
        case "off":
            robot.Commands.Add(new OffCommand());
            break;
        case "north":
            robot.Commands.Add(new NorthCommand());
            break;
        case "south":
            robot.Commands.Add(new SouthCommand());
            break;
        case "west":
            robot.Commands.Add(new WestCommand());
            break;
        case "east":
            robot.Commands.Add(new EastCommand());
            break;
        default:
            Console.WriteLine("There is no such command.");
            break;
    }
    input = Console.ReadLine()!;
}
Console.WriteLine();
robot.Run();
public interface IRobotCommand
{
    void Run(Robot robot);
}
public class OnCommand : IRobotCommand
{
    public void Run(Robot robot) => robot.IsPowered = true;
}
public class OffCommand : IRobotCommand
{
    public void Run(Robot robot) => robot.IsPowered = false;
}
public class NorthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered == true)
            robot.Y++;
    }
}
public class SouthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered == true)
            robot.Y--;
    }
}
public class WestCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered == true)
            robot.X--;
    }
}
public class EastCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered == true)
            robot.X++;
    }
}
public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public List<IRobotCommand> Commands = new List<IRobotCommand>();
    public void Run()
    {
        foreach (IRobotCommand? command in Commands!)
        {
            command.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}