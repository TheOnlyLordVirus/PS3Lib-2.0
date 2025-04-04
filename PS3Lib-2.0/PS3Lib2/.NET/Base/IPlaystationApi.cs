using System;
using System.Collections.Generic;

namespace PS3Lib2;

public interface IPlaystationApi : IMemoryApi, IDisposable
{
    public IEnumerable<ConsoleInfo> ConsolesInfo { get; }

    public IEnumerable<ProcessInfo> ProcessesInfo { get; }

    public bool IsConnected { get; }

    public bool Connect(in string ip);

    public bool Disconnect();

    public void ShutDown();

    public void RingBuzzer(in BuzzerMode buzzerMode);

    public void VshNotify(in string message);

    public void SetIdps(in string consoleId);

    public void SetPsid(in string psid);

    public void GetIdps(out string consoleId);

    public void GetPsid(out string psid);

    public void GetTemprature(ref uint cell, ref uint rsx);
}
