using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStartStopService.ConfigLoaders;
using GameStartStopService.BasicConfig;
using System.IO;
using GameStartStopService;

namespace GameStartStopService.UtilitiesFolder
{
    [Flags]
    public enum LoggerLogType
    {
        WindowsLog = 1,
        LocalLog = 0x2,
        ServerLog = 0x4,
        All = WindowsLog | LocalLog | ServerLog
    }

    public enum LoggerWarringLevel
    {
        RegularMessage,
        Warring,
        Danger,
        CriticalError
    }

    class Logger
    {
        const string ArcadeWindowsEventLogSource = "ArcadeServiceLog";
        const string ArcadeWindowsEventLog = "ArcadeServiceLog";

        const string LocalLogDefualtLoc = "\\LocalArcadeGameStartAndStopServiceLog.txt";

        static EventLog ArcadeServiceEventEventLog;

        readonly string LocalLog;
        readonly string ServerLog;
        readonly string MachineName;
        readonly string MasterServerURL;

        public Logger()
        {
            ArcadeServiceEventEventLog = new EventLog();

            //if (!EventLog.SourceExists(ArcadeWindowsEventLogSource))
            //{
            //    EventLog.CreateEventSource(ArcadeWindowsEventLogSource, ArcadeWindowsEventLog);
            //}

            //ArcadeServiceEventEventLog.Source = ArcadeWindowsEventLogSource;
            //ArcadeServiceEventEventLog.Log = ArcadeWindowsEventLog;

            LocalLog = ArcadeGameStartAndStopService.MainConfig.LocalLogOutput;
            if(LocalLog == null)
                LocalLog = Environment.CurrentDirectory + LocalLogDefualtLoc;
            ServerLog = ArcadeGameStartAndStopService.MainConfig.ServerLogOutput;
            MachineName = ArcadeGameStartAndStopService.MainConfig.MachineName;
            MasterServerURL = ArcadeGameStartAndStopService.MainConfig.MasterServerURL;
        }

        public void ClearTextLog()
        {
            File.Create(LocalLog).Close();
        }

        public void OpenTextLog()
        {
            Process.Start(LocalLog);
        }

        internal void WriteLog(string Message, LoggerWarringLevel Level = LoggerWarringLevel.RegularMessage, LoggerLogType Type = LoggerLogType.All)
        {
            switch(Type)
            {
                case LoggerLogType.All:
                    WriteLocalLogEntry(Message, Level);
                    WriteServerLogEntry(Message, Level);
                    WriteWindowsLogEntry(Message, Level);
                    break;
                case LoggerLogType.LocalLog:
                    WriteLocalLogEntry(Message, Level);
                    break;
                case LoggerLogType.ServerLog:
                    WriteServerLogEntry(Message, Level);
                    break;
                case LoggerLogType.WindowsLog:
                    WriteWindowsLogEntry(Message, Level);
                    break;
                case LoggerLogType.LocalLog | LoggerLogType.ServerLog:
                    WriteLocalLogEntry(Message);
                    WriteServerLogEntry(Message, Level);
                    break;
                case LoggerLogType.LocalLog | LoggerLogType.WindowsLog:
                    WriteLocalLogEntry(Message, Level);
                    WriteWindowsLogEntry(Message, Level);
                    break;
                case LoggerLogType.ServerLog | LoggerLogType.WindowsLog:
                    WriteServerLogEntry(Message, Level);
                    WriteWindowsLogEntry(Message, Level);
                    break;
            }
            WriteConsole(Message, Level);
        }

        internal void WriteWindowsLogEntry(string Message, LoggerWarringLevel Level = LoggerWarringLevel.RegularMessage)
        {
            //lock(ArcadeServiceEventEventLog)
            //    ArcadeServiceEventEventLog.WriteEntry(BuildLogString(Message, Level));
        }

#warning GameStartStopService.UtilitiesFolder.Logger WriteWindowsLogEntry not Implemented yet
        internal void WriteLocalLogEntry(string Message, LoggerWarringLevel Level = LoggerWarringLevel.RegularMessage)
        {
            lock (ArcadeServiceEventEventLog)
                using (StreamWriter SW = new StreamWriter(LocalLog, true))
                    SW.WriteLine(BuildLogString(Message, Level));
        }

#warning GameStartStopService.UtilitiesFolder.Logger WriteServerLogEntry not Implemented yet
        internal void WriteServerLogEntry(string Message, LoggerWarringLevel Level = LoggerWarringLevel.RegularMessage)
        {
            //throw new NotImplementedException();
        }

        internal void WriteConsole(string Message, LoggerWarringLevel Level = LoggerWarringLevel.RegularMessage)
        {
#if DEBUG
            lock (ArcadeServiceEventEventLog)
                try
                {
                    ConsoleColor ReturnColor = Console.ForegroundColor;
                    switch (Level)
                    {
                        case LoggerWarringLevel.RegularMessage:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(BuildLogString(Message, Level));
                            Console.ForegroundColor = ReturnColor;
                            break;
                        case LoggerWarringLevel.Warring:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(BuildLogString(Message, Level));
                            Console.ForegroundColor = ReturnColor;
                            break;
                        case LoggerWarringLevel.Danger:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(BuildLogString(Message, Level));
                            Console.ForegroundColor = ReturnColor;
                            break;
                        case LoggerWarringLevel.CriticalError:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine(BuildLogString(Message, Level));
                            Console.ForegroundColor = ReturnColor;
                            break;
                    }
                } catch{}
#endif
        }

        string BuildLogString(string Message, LoggerWarringLevel Level)
        {
            return "<" + Level + " Time=\"" + DateTime.UtcNow + "\">" + Message + "</" + Level + ">";
        }
    }
}
