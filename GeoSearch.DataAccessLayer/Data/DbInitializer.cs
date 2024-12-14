using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Data.Seeds;

namespace GeoSearch.DataAccessLayer.Data;

public class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(UserSeedData.GetSeedData());
            context.SaveChanges();
        }
    }
}