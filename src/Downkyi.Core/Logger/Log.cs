using NLog;

namespace Downkyi.Core.Log;

public static class Log
{
    static Log()
    {
        var config = new NLog.Config.LoggingConfiguration();

        // Targets where to log to Debugger
        var logConsole = new NLog.Targets.ConsoleTarget("logConsole")
        {
            Layout = "${date:format=HH\\:mm\\:ss} [${level:padding=-5}] ${message}"
        };

        // Targets where to log to Debugger
        var logDebugger = new NLog.Targets.DebuggerTarget("logDebugger")
        {
            Layout = "${date:format=HH\\:mm\\:ss} [${level:padding=-5}] ${message}"
        };

        // Targets where to log to File
        var logFile = new NLog.Targets.FileTarget("logFile")
        {
            FileName = "${baseDir}/Logs/${shortdate}/${level}.log",
            CreateDirs = true,
            KeepFileOpen = true,
            ArchiveAboveSize = 1024 * 1024,
            ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Sequence,
            MaxArchiveDays = 30,
            Layout = "${longdate} [${level:uppercase=false:padding=-5}] ${message} ${onexception:${exception:format=tostring} ${newline} ${stacktrace} ${newline}"
        };

        // Rules for mapping loggers to targets
        config.AddRule(LogLevel.Trace, LogLevel.Fatal, logConsole);
        config.AddRule(LogLevel.Trace, LogLevel.Fatal, logDebugger);
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);

        // Apply config           
        LogManager.Configuration = config;

        LogManager.ThrowExceptions = false;
    }

    public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
}
