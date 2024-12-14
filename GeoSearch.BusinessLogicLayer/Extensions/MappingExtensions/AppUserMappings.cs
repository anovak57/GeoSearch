﻿using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;

public static class AppUserMappings
{
    public static AuthResponse ToAuthResponse(this AppUser user)
    {
        return new AuthResponse(user.Username, user.ApiKey);
    }
}