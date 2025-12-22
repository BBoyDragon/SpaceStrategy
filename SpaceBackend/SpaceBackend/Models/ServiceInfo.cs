namespace SpaceBackend.Models;

public class ServiceInfo
{
    public string ServiceName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DatabaseInfo DatabaseInfo { get; set; } = new();
}

public class DatabaseInfo
{
    public bool IsConnected { get; set; }
    public string? DatabaseName { get; set; }
    public string? ServerVersion { get; set; }
    public string? ErrorMessage { get; set; }
}
