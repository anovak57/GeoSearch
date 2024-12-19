using GeoSearch.API.SignalR.Hubs;
using GeoSearch.API.SignalR.ServiceContracts;
using GeoSearch.BusinessLogicLayer.DTO;
using Microsoft.AspNetCore.SignalR;

namespace GeoSearch.API.SignalR.Services;

public class SignalRNotifier : ISignalRNotifier
{
    private readonly IHubContext<SearchHub> _hubContext;

    public SignalRNotifier(IHubContext<SearchHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifySearchRequestAsync(SearchResult searchResult)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveSearchRequest", searchResult);
    }
}