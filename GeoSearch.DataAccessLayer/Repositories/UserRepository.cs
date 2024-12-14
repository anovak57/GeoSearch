using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Entities;
using GeoSearch.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace GeoSearch.DataAccessLayer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<AppUser?> GetUserByApiKey(string apiKey)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.ApiKey == apiKey);
    }
}