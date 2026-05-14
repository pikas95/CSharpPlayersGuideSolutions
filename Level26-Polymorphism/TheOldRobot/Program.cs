RobotCommand?[] commands = new RobotCommand[3];
for (int i = 0; i < commands.Length; i++)
{
    commands[i] = Console.ReadLine() switch
    {
        "on" => new OnCommand(),
        "off" => new OffCommand(),
        "north" => new NorthCommand(),
        "south" => new SouthCommand(),
        "west" => new WestCommand(),
        "east" => new EastCommand(),
        _ => null
    };
}
new Robot(commands).Run();
public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public RobotCommand?[] Commands { get; }
    public Robot(RobotCommand?[] commands) { Commands = commands; }
    public void Run()
    {
        foreach (RobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}
public abstract class RobotCommand { public abstract void Run(Robot robot); }
public class OnCommand : RobotCommand { public override void Run(Robot robot) => robot.IsPowered = true; }
public class OffCommand : RobotCommand { public override void Run(Robot robot) => robot.IsPowered = false; }
public class NorthCommand : RobotCommand  { public override void Run(Robot robot) { if (robot.IsPowered) robot.Y++; } }
public class SouthCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.Y--; } }
public class WestCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.X--; } }
public class EastCommand : RobotCommand { public override void Run(Robot robot) { if (robot.IsPowered) robot.X++; } }