using GeoSearch.BusinessLogicLayer.DTO;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface IUserService
{
    Task<AuthResponse?> GetUserByApiKey(string apiKey);
}