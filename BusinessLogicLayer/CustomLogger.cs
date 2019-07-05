using BusinessLogicLayer.Interfaces;
using NLog;

namespace BusinessLogicLayer
{
    public class CustomLogger : ICustomLogger
    {
        private static Logger logger = null;

        public static Logger GetLogger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger("Custom Logger");
                }
                return logger;
            }
        }

        public void LogInfo(string infoToLog)
        {
            GetLogger.Info(infoToLog);
        }

        public void LogError(string infoToLog)
        {
            GetLogger.Error(infoToLog);
        }

        public void LogWarning(string infoToLog)
        {
            GetLogger.Warn(infoToLog);
        }

        public void LogDebug(string infoToLog)
        {
            GetLogger.Debug(infoToLog);
        }
    }
}
