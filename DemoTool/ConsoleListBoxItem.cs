using PS3Lib2;

namespace DemoTool;

internal sealed class ConsoleListBoxItem
{
    public ConsoleInfo ConsoleInfo { get; set; }

    public override string ToString() =>
        $"{ConsoleInfo.ConsoleName} : {ConsoleInfo.ConsoleIp}";
}
