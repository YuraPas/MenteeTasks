using NLog;

namespace AirportTask
{
    public class CustomLogger
    {
        private static Logger logger = null;

        public CustomLogger()
        {

        }

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
    }
}
