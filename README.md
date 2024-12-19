# GeoSearch API

GeoSearch is a RESTful API built with `.NET 8`. It integrates with the Foursquare Places API to fetch location data and implements basic authentication using API keys.

---

## Features

- Fetch location data from the Foursquare API.
- Save and retrieve search requests and results.
- Support for user-specific favorite locations.
- Basic authentication using API keys provided in request headers.
- Idempotency to prevent duplicate processing of the same requests.
- Real-time updates using SignalR.

---

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL
- Foursquare API Key - https://location.foursquare.com/developer/
- Visual Studio or Rider IDE (optional)


## Installation

1. Clone the repository:
   
   ```bash
   git clone https://github.com/yourusername/GeoSearch.git
   cd GeoSearch
   
2. Update appsettings.Development.json with your environment-specific configuration:
   
   ```bash
   {
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "PostgresConnection": "Host=localhost;Port=5432;Database=GeoSearch;Username=postgres;Password=yourpassword"
    },
    "Foursquare": {
      "BaseUrl": "https://api.foursquare.com/v3/places/search",
      "ApiKey": "your_foursquare_api_key"
    }
   }
   
  3. Build and run the application:
     ```bash
      dotnet build
      dotnet run

## Authentication
The API expects an x-api-key header for authentication. Seeded API keys are available for development purposes:

Username | API Key

- `member0` |	`0888ed55-167f-4ec8-95bd-f74f7f66088c`

- `member1`	| `da490e8e-de97-4f07-9335-83d9ce7f98ff`

- `member2` |	`29955f19-9bec-435a-83db-9bf188e03ada`

## Endpoints
### Locations
- POST `/api/geolocation`

    Fetch locations from Foursquare API and save the request and response in the database.
    
    Request Body:
      
      {
        "latitude": "number",
        "longitude": "number",
        "query": "string",
        "radius": "number"
      }
    
    Headers:
    ```bash
    x-api-key: User's API key.
    
    Idempotency-Key: Unique identifier for the request.
    ```
- GET `/api/geolocation`

    Retrieve all locations stored in the database.

- GET `/api/geolocation/{query}`
  
    Retrieve locations matching a specific category or query.

### Searches
- GET `/api/geolocation/location-searches`
  
    Retrieve all location search requests made by users.

### Favorites

- POST `/api/geolocation/user/{userId}/locations/{locationId}/save-favourite`
  
    Save a location as a favorite for a specific user.

- GET `/api/geolocation/user/{userId}/favourite-locations`
  
    Retrieve all favorite locations for a specific user.

### Idempotency
  To ensure idempotent operations, the API expects an `Idempotency-Key` header for specific endpoints. The value should be a unique combination of `lat:lang:query:radius` for the operation.

### Real-Time Notifications
  GeoSearch uses SignalR to send real-time updates for new search requests and results. Ensure your client subscribes to the `ReceiveSearchRequest` SignalR event. For testing purposes - a simple console application that subscribes to the `ReceiveSearchRequest` SignalR event: https://github.com/anovak57/GeoSearchApiSubscriber

### Technologies Used
- Framework: .NET 8
- Database: PostgreSQL
- ORM: Entity Framework Core
- Real-Time: SignalR
- External API: Foursquare Places API
