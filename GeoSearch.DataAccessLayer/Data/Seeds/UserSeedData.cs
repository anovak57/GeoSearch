using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.DataAccessLayer.Data.Seeds;

public class UserSeedData
{
    public static AppUser[] GetSeedData()
    {
        return
        [
            new AppUser
            {
                UserId = 1,
                Username = "member0",
                ApiKey = "0888ed55-167f-4ec8-95bd-f74f7f66088c"
            },
            new AppUser
            {
                UserId = 2,
                Username = "member1",
                ApiKey = "da490e8e-de97-4f07-9335-83d9ce7f98ff"
            },
            new AppUser
            {
                UserId = 3,
                Username = "member2",
                ApiKey = "29955f19-9bec-435a-83db-9bf188e03ada"
            }
        ];
    }
}