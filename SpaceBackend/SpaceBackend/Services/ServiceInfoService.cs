using SpaceBackend.Models;
using SpaceBackend.Repositories;

namespace SpaceBackend.Services;

public class ServiceInfoService : IServiceInfoService
{
    private readonly IDatabaseRepository _databaseRepository;
    private static readonly DateTime _startTime = DateTime.UtcNow;

    public ServiceInfoService(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
    }

    public async Task<ServiceInfo> GetServiceInfoAsync()
    {
        var databaseInfo = await _databaseRepository.GetDatabaseInfoAsync();

        return new ServiceInfo
        {
            ServiceName = "SpaceBackend",
            Version = "1.0.0",
            StartTime = _startTime,
            DatabaseInfo = databaseInfo
        };
    }
}
