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
                UserId = new Guid("1fe4e147-de43-44f5-85ad-d1d24a0cfbb2"),
                Username = "member0",
                ApiKey = "0888ed55-167f-4ec8-95bd-f74f7f66088c"
            },
            new AppUser
            {
                UserId = new Guid("a9f585ae-33ae-4e31-82b1-5cf8719ab2f4"),
                Username = "member1",
                ApiKey = "da490e8e-de97-4f07-9335-83d9ce7f98ff"
            },
            new AppUser
            {
                UserId = new Guid("b4ceed95-f1c2-4ae2-a8f3-d7942c9364fd"),
                Username = "member2",
                ApiKey = "29955f19-9bec-435a-83db-9bf188e03ada"
            }
        ];
    }
}