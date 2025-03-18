using System;
using System.Threading;
using System.Threading.Tasks;

using PS3Lib2.Interfaces;

namespace PS3Lib2.Cheats;

#nullable enable

public sealed class PersistentPlaystationMemoryWriter : IGameCheat
{
    private Guid id = Guid.NewGuid();
    public Guid Id => id;

    private readonly IPlaystationApi _playstationConsole;
    private readonly TimeSpan _writeDelay = TimeSpan.FromSeconds(1);

    private readonly uint _memoryAddress;
    private readonly byte[] _onBytes;
    private readonly byte[] _offBytes;

    private bool isEnabled = false;
    public bool GetValue() => isEnabled;

    private CancellationTokenSource? updaterCancellationTokenSource = new();

    public PersistentPlaystationMemoryWriter
        (
            IPlaystationApi playstationConsole,
            uint memoryAddress,
            TimeSpan writeDelay,
            byte onByte = 0x01,
            byte offByte = 0x00
        )
    {
        _playstationConsole = playstationConsole;
        _memoryAddress = memoryAddress;

        _onBytes = [onByte];
        _offBytes = [offByte];

        _writeDelay = writeDelay;
    }

    public PersistentPlaystationMemoryWriter
        (
            IPlaystationApi playstationConsole,
            uint memoryAddress,
            TimeSpan writeDelay,
            byte[] onBytes,
            byte[] offBytes
        )
    {
        _playstationConsole = playstationConsole;
        _memoryAddress = memoryAddress;

        if (onBytes.Length != offBytes.Length)
            throw new ArgumentOutOfRangeException("Error: onBytes and offBytes must have the same number of bytes.");

        _onBytes = onBytes;
        _offBytes = offBytes;

        _writeDelay = writeDelay;
    }

    public async Task SetLoop(CancellationToken cancellationToken)
    {
        try
        {
            do
            {
                _playstationConsole
                    .WriteMemory
                    (
                        _memoryAddress,
                        _onBytes
                    );


                await Task
                    .Delay(_writeDelay, cancellationToken)
                    .ConfigureAwait(false);
            }
            while (!cancellationToken.IsCancellationRequested);
        }

        finally
        {
            _playstationConsole
                .WriteMemory
                (
                    _memoryAddress,
                    _offBytes!
                );

            isEnabled = false;
        }
    }

    public void Enable()
    {
        try
        {
            if (updaterCancellationTokenSource is null)
                updaterCancellationTokenSource = new CancellationTokenSource();

            _ = SetLoop(updaterCancellationTokenSource.Token)
                .ConfigureAwait(false);
        }

        catch { return; }

        isEnabled = true;
    }

    public void Disable()
    {
        updaterCancellationTokenSource?.Cancel();
        updaterCancellationTokenSource = null;
        isEnabled = false;
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