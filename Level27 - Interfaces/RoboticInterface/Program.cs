Robot robot = new Robot();
for (int i = 0; i < 3; i++)
{
    robot.Commands[i] = Console.ReadLine() switch
    {
        "on" => robot.Commands[i] = new OnCommand(),
        "off" => robot.Commands[i] = new OffCommand(),
        "north" => robot.Commands[i] = new NorthCommand(),
        "south" => robot.Commands[i] = new SouthCommand(),
        "west" => robot.Commands[i] = new WestCommand(),
        "east" => robot.Commands[i] = new EastCommand()
    };
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
    public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];
    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}