using System;
using System.Collections.Generic;
using System.Linq;

using PS3Lib2.Interfaces;

namespace PS3Lib2.Cheats;

#nullable enable

public sealed class PlaystationMemoryWriter : IGameCheat
{
    public Guid Id => Guid.NewGuid();
    private readonly IPlaystationApi _playstationConsole;

    private readonly uint _memoryAddress;
    private readonly uint _arrayLength;

    private readonly byte[]? _onBytes = null;
    private readonly byte[]? _offBytes = null;

    private bool isEnabled = false;

    public PlaystationMemoryWriter
    (
        IPlaystationApi playstationConsole,
        uint memoryAddress,
        byte onByte = 0x01,
        byte offByte = 0x00
    )
    {
        _playstationConsole = playstationConsole;
        _memoryAddress = memoryAddress;

        _onBytes = [onByte];
        _offBytes = [offByte];
    }

    public PlaystationMemoryWriter
    (
        IPlaystationApi playstationConsole,
        uint memoryAddress,
        byte[] onBytes,
        byte[] offBytes
    )
    {
        if (onBytes.Length != offBytes.Length)
            throw new ArgumentOutOfRangeException("Error: onBytes and offBytes must have the same number of bytes.");

        _arrayLength = (uint)onBytes.Length;

        _playstationConsole = playstationConsole;
        _memoryAddress = memoryAddress;

        _onBytes = onBytes;
        _offBytes = offBytes;
    }

    public void Enable()
    {
        try
        {
            _playstationConsole
                .WriteMemory
                (
                    _memoryAddress,
                    _onBytes
                );
        }

        catch
        {
            return;
        }

        isEnabled = true;
    }

    public void Disable()
    {
        try
        {
            _playstationConsole
                .WriteMemory
                (
                    _memoryAddress,
                    _offBytes
                );
        }

        catch { return; }

        isEnabled = false;
    }

    public bool GetValue()
    {
        try
        {
            return Enumerable.SequenceEqual
                (
                    Internal_GetBytes(),
                    _onBytes
                );
        }

        catch { return false; }
    }

    public void Toggle()
    {
        isEnabled = !(GetValue());

        if (isEnabled)
            Enable();
        else
            Disable();
    }

    private byte[] Internal_GetBytes()
        => _playstationConsole
                .ReadMemory
                (
                    _memoryAddress,
                    _arrayLength
                );
}