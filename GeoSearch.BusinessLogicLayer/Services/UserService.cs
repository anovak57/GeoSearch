using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.DataAccessLayer.Entities;
using GeoSearch.DataAccessLayer.RepositoryContracts;

namespace GeoSearch.BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<AuthResponse?> GetUserByApiKey(string apiKey)
    {
        AppUser? user = await _userRepository.GetUserByApiKey(apiKey);

        return user?.MapToAuthResponse();
    }
}