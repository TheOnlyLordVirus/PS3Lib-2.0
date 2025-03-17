using System;

namespace PS3Lib2.Interfaces;

public interface IPlaystationApi : IMemoryApi, IDisposable
{
    public bool IsConnected { get; }

    public bool Connect(in string ip);

    public bool Disconnect();

    public void ShutDown();

    public void RingBuzzer();

    public void VshNotify(in string message);

    public void SetIdps(in string consoleId);

    public void SetPsid(in string psid);

    public void GetTemprature(ref uint cell, ref uint rsx);
}
