using System;

namespace SpaceStrategy.Models
{
    [Serializable]
    public class ServiceInfo
    {
        public string serviceName;
        public string version;
        public string startTime;
        public DatabaseInfo databaseInfo;
    }

    [Serializable]
    public class DatabaseInfo
    {
        public bool isConnected;
        public string databaseName;
        public string serverVersion;
        public string errorMessage;
    }
}
