namespace UnityEngine
{
    public static class Debug
    {
        public static void Log(string message) { }
        public static void LogWarning(string message) { }
        public static void LogError(string message) { }
    }

    public static class Application
    {
        public static string persistentDataPath = ".";
    }

    public class MonoBehaviour { }
}

