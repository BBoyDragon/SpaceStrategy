using SpaceBackend.Models;

namespace SpaceBackend.Repositories;

public interface IDatabaseRepository
{
    Task<DatabaseInfo> GetDatabaseInfoAsync();
}
