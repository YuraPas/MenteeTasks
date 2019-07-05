namespace BusinessLogicLayer.Interfaces
{
    public interface ICustomLogger
    {
        void LogInfo(string infoToLog);
        void LogError(string infoToLog);
        void LogWarning(string infoToLog);
        void LogDebug(string infoToLog);
    }
}
