using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.DataAccessLayer.RepositoryContracts;

public interface IUserRepository
{
    Task<AppUser?> GetUserByApiKey(string apiKey);
}