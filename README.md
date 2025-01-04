# Application Readme

## Overview
This application retrieves top stories from an external API using caching for improved performance.

### Key Features
- Fetches and caches story data.
- Sorts stories by score and returns top results.
- Logs errors and important operations.

## Technologies Used
- ASP.NET Core
- C#
- Memory Caching
- AutoMapper
- Dependency Injection
- Async/Await

## Project Structure
```
Controllers/
  StoryController.cs
Services/
  StoryService.cs
Models/
  Story.cs
  StoryDto.cs
Interfaces/
  IHackerNewsApiClient.cs
Clients/
  HackerNewsApiClient.cs
Mappings/
  AutoMapperProfile.cs
Program.cs
```

## Configuration
- **AppSettings.json** for API base URL and cache durations.
- Dependency injection setup in `Program.cs`.

## Running the Application
1. Clone the repository.
2. Navigate to the project directory.
3. Run `dotnet restore`.
4. Build the project with `dotnet build`.
5. Start the application using `dotnet run`.

## API Endpoints
- **Get Top Stories**: `/api/Stories?count=10` (GET)

## Future Enhancements
- Add unit tests.
- Improve error handling.
- Introduce distributed caching.

