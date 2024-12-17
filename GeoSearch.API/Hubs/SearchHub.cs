using GeoSearch.BusinessLogicLayer.DTO;
using Microsoft.AspNetCore.SignalR;

namespace GeoSearch.API.Hubs;

public class SearchHub : Hub
{
    public async Task BroadcastSearchRequest(LocationSearchRequest searchRequest)
    {
        await Clients.All.SendAsync("ReceiveSearchRequest", searchRequest);
    }
}