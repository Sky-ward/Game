using UnityEngine;

public enum LogLevel
{
    Info = 0,
    Warn = 1
}

public static class Log
{
    public static LogLevel Level = Debug.isDebugBuild ? LogLevel.Info : LogLevel.Warn;

    public static void SetLevel(LogLevel level) => Level = level;

    public static void Info(string message)
    {
        if (Level <= LogLevel.Info)
            Debug.Log(message);
    }

    public static void Warn(string message)
    {
        if (Level <= LogLevel.Warn)
            Debug.LogWarning(message);
    }
}
