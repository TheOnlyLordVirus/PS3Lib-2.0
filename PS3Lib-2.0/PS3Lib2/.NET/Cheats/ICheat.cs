using System;

namespace PS3Lib2.Cheats;

public interface IGameCheat
{
    Guid Id { get; }

    bool GetValue();

    void Enable();
    void Disable();
    void Toggle();
}