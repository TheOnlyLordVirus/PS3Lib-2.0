using System;

namespace PS3Lib2.Cheats;

public sealed class CheatActionHandler : IGameCheat
{
    public Guid Id => Guid.NewGuid();

    private bool isEnabled = false;
    public bool IsEnabled => isEnabled;

    public bool GetValue() => isEnabled;

    private readonly Action _enableAction;
    private readonly Action _disableAction;

    public CheatActionHandler(Action enableAction, Action disableAction)
    {
        _enableAction = enableAction;
        _disableAction = disableAction;
    }

    public void Enable() 
        => _enableAction.Invoke();

    public void Disable()
        => _disableAction.Invoke();

    public void Toggle()
    {
        isEnabled = !isEnabled;

        if (isEnabled)
            Enable();
        else
            Disable();
    }
}
