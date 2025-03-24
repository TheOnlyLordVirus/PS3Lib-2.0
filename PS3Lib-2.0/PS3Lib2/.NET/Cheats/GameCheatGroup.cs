using System;
using System.Collections.Generic;

namespace PS3Lib2.Cheats;

public sealed class GameCheatGroup : IGameCheat
{
    private readonly Guid id = Guid.NewGuid();
    public Guid Id => id;

    private readonly IPlaystationApi _playstationConsole;
    private readonly IEnumerable<IGameCheat> _gameCheats;

    private bool isEnabled = false;
    public bool GetValue() => isEnabled;

    public GameCheatGroup(IPlaystationApi playstationConsole, IEnumerable<IGameCheat> gameCheats)
    {
        _playstationConsole = playstationConsole;
        _gameCheats = gameCheats;
    }

    public void Enable()
    {
        foreach (var cheat in _gameCheats)
            cheat.Enable();
    }

    public void Disable()
    {
        foreach (var cheat in _gameCheats)
            cheat.Disable();
    }

    public void Toggle()
    {
        isEnabled = !isEnabled;

        if (isEnabled)
            Enable();
        else
            Disable();
    }
}
