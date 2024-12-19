using GeoSearch.BusinessLogicLayer.DTO;

namespace GeoSearch.API.SignalR.ServiceContracts;

public interface ISignalRNotifier
{
    Task NotifySearchRequestAsync(SearchResult searchResult);
}