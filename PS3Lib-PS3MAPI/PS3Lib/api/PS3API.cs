//// ************************************************* //
////    --- Copyright (c) 2015 iMCS Productions ---    //
//// ************************************************* //
////              PS3Lib v4 By FM|T iMCSx              //
////          PS3MAPI support added by: _NzV_          //
////     Decompiled and edited for 4k Form support     //
////                                                   //
//// Features v4.5 :                                   //
//// - Support CCAPI v2.60+ C# by iMCSx.               //
//// - Read/Write memory as 'double'.                  //
//// - Read/Write memory as 'float' array.             //
//// - Constructor overload for ArrayBuilder.          //
//// - Some functions fixes.                           //
////                                                   //
//// Credits : Enstone, Buc-ShoTz, _NzV_, Lord Virus   //
////                                                   //
//// Follow me :                                       //
////                                                   //
//// FrenchModdingTeam.com                             //
//// Twitter.com/iMCSx                                 //
//// Facebook.com/iMCSx                                //
////                                                   //
//// ************************************************* //

using System;
using System.Collections.Generic;
using System.Reflection;
using PS3ManagerAPI;

namespace PS3Lib
{
    public enum SelectAPI
    {
        ControlConsole,
        TargetManager,
        PS3Manager
    }

    public class PS3API
    {
        public PS3API(SelectAPI API = SelectAPI.TargetManager)
        {
            PS3API.SetAPI.API = API;
            this.MakeInstanceAPI(API);
        }

        public void setTargetName(string value)
        {
            PS3API.targetName = value;
        }

        private void MakeInstanceAPI(SelectAPI API)
        {
            if (API == SelectAPI.TargetManager)
            {
                if (PS3API.Common.TmApi == null)
                {
                    PS3API.Common.TmApi = new TMAPI();
                    return;
                }
            }
            else if (API == SelectAPI.ControlConsole)
            {
                if (PS3API.Common.CcApi == null)
                {
                    PS3API.Common.CcApi = new CCAPI();
                    return;
                }
            }
            else if (API == SelectAPI.PS3Manager && PS3API.Common.Ps3mApi == null)
            {
                PS3API.Common.Ps3mApi = new PS3MAPI();
            }
        }

        /// <summary>init again the connection if you use a Thread or a Timer.</summary>
        public void InitTarget()
        {
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                PS3API.Common.TmApi.InitComms();
            }
        }

        /// <summary>Connect your console with selected API.</summary>
        public bool ConnectTarget(int target = 0)
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool result = false;
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                result = PS3API.Common.TmApi.ConnectTarget(target);
            }
            else if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                result = ShowConsoles();
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                result = PS3API.Common.Ps3mApi.ConnectTarget();
            }
            return result;
        }

        /// <summary>Connect your console with selected API.</summary>
        public bool ConnectTarget(string ip)
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool flag = false;
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                if (PS3API.Common.CcApi.SUCCESS(PS3API.Common.CcApi.ConnectTarget(ip)))
                {
                    PS3API.targetIp = ip;
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                flag = PS3API.Common.Ps3mApi.ConnectTarget(ip);
                if (flag)
                {
                    PS3API.targetIp = ip;
                }
            }
            return flag;
        }

        /// <summary>Connect your console with PS3MAPI using ip and port.</summary>
        public bool ConnectTarget(string ip, int port)
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool flag = false;
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                flag = PS3API.Common.Ps3mApi.ConnectTarget(ip, port);
                if (flag)
                {
                    PS3API.targetIp = ip;
                }
            }
            return flag;
        }

        /// <summary>Disconnect Target with selected API.</summary>
        public void DisconnectTarget()
        {
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                PS3API.Common.TmApi.DisconnectTarget();
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.DisconnectTarget();
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.DisconnectTarget();
            }
        }

        /// <summary>Attach the current process (current Game) with selected API.</summary>
        public bool AttachProcess()
        {
            this.MakeInstanceAPI(this.GetCurrentAPI());
            bool result = false;
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                result = PS3API.Common.TmApi.AttachProcess();
            }
            else if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                result = PS3API.Common.CcApi.SUCCESS(PS3API.Common.CcApi.AttachProcess());
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                result = PS3API.Common.Ps3mApi.AttachProcess();
            }
            return result;
        }

        public string GetConsoleName()
        {
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                return PS3API.Common.TmApi.SCE.GetTargetName();
            }
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                if (PS3API.targetName != string.Empty)
                {
                    return PS3API.targetName;
                }
                if (PS3API.targetIp != string.Empty)
                {
                    List<CCAPI.ConsoleInfo> list = new List<CCAPI.ConsoleInfo>();
                    list = PS3API.Common.CcApi.GetConsoleList();
                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Ip == PS3API.targetIp)
                            {
                                return list[i].Name;
                            }
                        }
                    }
                }
                return PS3API.targetIp;
            }
            else
            {
                if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
                {
                    return "PS3 Manager API";
                }
                return "none";
            }
        }

        /// <summary>Set memory to offset with selected API.</summary>
        public void SetMemory(uint offset, byte[] buffer)
        {
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                PS3API.Common.TmApi.SetMemory(offset, buffer);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetMemory(offset, buffer);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.SetMemory(offset, buffer);
            }
        }

        /// <summary>Get memory from offset using the Selected API.</summary>
        public void GetMemory(uint offset, byte[] buffer)
        {
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                PS3API.Common.TmApi.GetMemory(offset, buffer);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.GetMemory(offset, buffer);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.GetMemory(offset, buffer);
            }
        }

        /// <summary>Get memory from offset with a length using the Selected API.</summary>
        public byte[] GetBytes(uint offset, int length)
        {
            byte[] array = new byte[length];
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                PS3API.Common.TmApi.GetMemory(offset, array);
            }
            else if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.GetMemory(offset, array);
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.GetMemory(offset, array);
            }
            return array;
        }

        public void Notify(string msg, CCAPI.NotifyIcon icon = CCAPI.NotifyIcon.INFO)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.Notify(icon, msg);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.Notify(msg);
            }
        }

        public void Power(PS3API.PowerFlags flag)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                if (flag == PS3API.PowerFlags.ShutDown)
                {
                    PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.ShutDown);
                    return;
                }
                if (flag == PS3API.PowerFlags.QuickReboot)
                {
                    PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.SoftReboot);
                    return;
                }
                if (flag == PS3API.PowerFlags.SoftReboot)
                {
                    PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.SoftReboot);
                    return;
                }
                if (flag == PS3API.PowerFlags.HardReboot)
                {
                    PS3API.Common.CcApi.ShutDown(CCAPI.RebootFlags.HardReboot);
                    return;
                }
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                if (flag == PS3API.PowerFlags.ShutDown)
                {
                    PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.ShutDown);
                    return;
                }
                if (flag == PS3API.PowerFlags.QuickReboot)
                {
                    PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.QuickReboot);
                    return;
                }
                if (flag == PS3API.PowerFlags.SoftReboot)
                {
                    PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.SoftReboot);
                    return;
                }
                if (flag == PS3API.PowerFlags.HardReboot)
                {
                    PS3API.Common.Ps3mApi.Power(PS3ManagerAPI.PS3MAPI.PS3_CMD.PowerFlags.ShutDown);
                }
            }
        }

        public void SetConsoleID(string consoleID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetConsoleID(consoleID);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.SetConsoleID(consoleID);
            }
        }

        public void SetConsoleID(byte[] consoleID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetConsoleID(consoleID);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.SetConsoleID(consoleID);
            }
        }

        public void SetPSID(string PSID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetPSID(PSID);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.SetPSID(PSID);
            }
        }

        public void SetPSID(byte[] PSID)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                PS3API.Common.CcApi.SetPSID(PSID);
                return;
            }
            if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                PS3API.Common.Ps3mApi.SetPSID(PSID);
            }
        }

        public void Buzzer(PS3API.BuzzerMode flag)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                if (flag == PS3API.BuzzerMode.Single)
                {
                    PS3API.Common.CcApi.RingBuzzer(CCAPI.BuzzerMode.Single);
                    return;
                }
                if (flag == PS3API.BuzzerMode.Double)
                {
                    PS3API.Common.CcApi.RingBuzzer(CCAPI.BuzzerMode.Double);
                    return;
                }
                if (flag == PS3API.BuzzerMode.Triple)
                {
                    PS3API.Common.CcApi.RingBuzzer(CCAPI.BuzzerMode.Continuous);
                    return;
                }
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                if (flag == PS3API.BuzzerMode.Single)
                {
                    PS3API.Common.Ps3mApi.RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode.Single);
                    return;
                }
                if (flag == PS3API.BuzzerMode.Double)
                {
                    PS3API.Common.Ps3mApi.RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode.Double);
                    return;
                }
                if (flag == PS3API.BuzzerMode.Triple)
                {
                    PS3API.Common.Ps3mApi.RingBuzzer(PS3ManagerAPI.PS3MAPI.PS3_CMD.BuzzerMode.Triple);
                }
            }
        }

        public void Led(PS3API.LedColor color, PS3API.LedMode mode)
        {
            if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.Off)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Off);
                    return;
                }
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.On)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.On);
                    return;
                }
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkFast)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                    return;
                }
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkSlow)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.Off)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.Off);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.On)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.On);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkFast)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.Blink);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkSlow)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Green, CCAPI.LedMode.Blink);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.Off)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Off);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.On)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.On);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkFast)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkSlow)
                {
                    PS3API.Common.CcApi.SetConsoleLed(CCAPI.LedColor.Red, CCAPI.LedMode.Blink);
                    return;
                }
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.Off)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.Off);
                    return;
                }
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.On)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.On);
                    return;
                }
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkFast)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkFast);
                    return;
                }
                if (color == PS3API.LedColor.Red && mode == PS3API.LedMode.BlinkSlow)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Red, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkSlow);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.Off)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.Off);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.On)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.On);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkFast)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkFast);
                    return;
                }
                if (color == PS3API.LedColor.Green && mode == PS3API.LedMode.BlinkSlow)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Green, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkSlow);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.Off)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.Off);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.On)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.On);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkFast)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkFast);
                    return;
                }
                if (color == PS3API.LedColor.Yellow && mode == PS3API.LedMode.BlinkSlow)
                {
                    PS3API.Common.Ps3mApi.SetConsoleLed(PS3ManagerAPI.PS3MAPI.PS3_CMD.LedColor.Yellow, PS3ManagerAPI.PS3MAPI.PS3_CMD.LedMode.BlinkSlow);
                }
            }
        }

        /// <summary>Change current API.</summary>
        public void ChangeAPI(SelectAPI API)
        {
            PS3API.SetAPI.API = API;
            this.MakeInstanceAPI(this.GetCurrentAPI());
        }

        /// <summary>Return selected API.</summary>
        public SelectAPI GetCurrentAPI()
        {
            return PS3API.SetAPI.API;
        }

        /// <summary>Return selected API into string format.</summary>
        public string GetCurrentAPIName()
        {
            string result = string.Empty;
            if (PS3API.SetAPI.API == SelectAPI.TargetManager)
            {
                result = Enum.GetName(typeof(SelectAPI), SelectAPI.TargetManager).Replace("Manager", " Manager");
            }
            else if (PS3API.SetAPI.API == SelectAPI.ControlConsole)
            {
                result = Enum.GetName(typeof(SelectAPI), SelectAPI.ControlConsole).Replace("Console", " Console");
            }
            else if (PS3API.SetAPI.API == SelectAPI.PS3Manager)
            {
                result = Enum.GetName(typeof(SelectAPI), SelectAPI.PS3Manager).Replace("Manager", " Manager");
            }
            return result;
        }

        /// <summary>This will find the dll ps3tmapi_net.dll for TMAPI.</summary>
        public Assembly PS3TMAPI_NET()
        {
            return PS3API.Common.TmApi.PS3TMAPI_NET();
        }

        /// <summary>Use the extension class with your selected API.</summary>
        public Extension Extension
        {
            get
            {
                return new Extension(PS3API.SetAPI.API);
            }
        }

        /// <summary>Access to all TMAPI functions.</summary>
        public TMAPI TMAPI
        {
            get
            {
                return new TMAPI();
            }
        }

        /// <summary>Access to all CCAPI functions.</summary>
        public CCAPI CCAPI
        {
            get
            {
                return new CCAPI();
            }
        }

        /// <summary>Access to all PS3MAPI functions.</summary>
        public PS3MAPI PS3MAPI
        {
            get
            {
                return new PS3MAPI();
            }
        }

        private static string targetName = string.Empty;

        private static string targetIp = string.Empty;

        private class SetAPI
        {
            public static SelectAPI API;
        }

        private class Common
        {
            public static CCAPI CcApi;

            public static TMAPI TmApi;

            public static PS3MAPI Ps3mApi;
        }

        public enum PowerFlags
        {
            ShutDown,
            QuickReboot,
            SoftReboot,
            HardReboot
        }

        public enum BuzzerMode
        {
            Single,
            Double,
            Triple
        }

        public enum LedColor
        {
            Red,
            Green,
            Yellow
        }

        public enum LedMode
        {
            Off,
            On,
            BlinkSlow,
            BlinkFast
        }


        /// <summary>
        /// This will open the ConsoleList form and await a response from the form.
        /// </summary>
        /// <returns>The connection status from CCAPI</returns>
        public bool ShowConsoles()
        {
            ConsoleList list = new ConsoleList(this);

            while (list.Result is null) ;

            return list.Result.Value;
        }
    }
}
