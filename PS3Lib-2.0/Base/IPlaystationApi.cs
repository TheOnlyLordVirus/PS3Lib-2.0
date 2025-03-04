namespace PS3Lib2.Interfaces;

internal interface IPlaystationApi : IMemoryApi
{
    bool IsConnected { get; }

    bool Connect();

    bool Connect(string ip);

    bool Disconnect();

    void ShutDown();

    void RingBuzzer();

    void VshNotify(string message);

    string GetConsoleId();

    void SetIdps(string consoleId);

    void SetPsid(string psid);

    void GetTemprature(ref int cell, ref int rsx);
}
