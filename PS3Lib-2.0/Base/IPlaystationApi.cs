using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3LibNew.Interfaces
{
    internal interface IPlaystationApi : IMemoryApi
    {
        bool Connect();

        bool Connect(string ip);

        bool Disconnect();

        bool Connected { get; }

        void RingBuzzer();

        void VshNotify(string message);

        string GetConsoleId();

        void SetConsoleId();

        void ShutDown();

        void GetTemprature();


    }
}
