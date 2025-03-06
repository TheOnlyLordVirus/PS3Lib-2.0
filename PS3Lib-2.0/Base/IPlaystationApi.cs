using System;

namespace PS3Lib2.Interfaces;

internal interface IPlaystationApi : IMemoryApi, IDisposable
{
    public bool IsConnected { get; }

    public bool Connect();

    public bool Connect(string ip);

    public bool Disconnect();

    public void ShutDown();

    public void RingBuzzer();

    public void VshNotify(string message);

    public void SetIdps(string consoleId);

    public void SetPsid(string psid);

    public void GetTemprature(ref int cell, ref int rsx);
}
