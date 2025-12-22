using SpaceBackend.Models;

namespace SpaceBackend.Services;

public interface IServiceInfoService
{
    Task<ServiceInfo> GetServiceInfoAsync();
}
