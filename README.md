# Smokin Geckos lab 3  

## Description

A backend application developed for Software Engineering Design and Architecture course that implements a REST API endpoints for a point of sale system based on team's Komandux [document](https://github.com/Simutyte/PSP_Komanda32_API), [data model](https://www.figma.com/file/R4yOcQSJ9v7WMlEwD4cyLn/UML-Diagrams-(Community)?node-id=0%3A1&t=kCVw7liNpcpIf8zo-1) and Komanda32 [API documentation](https://github.com/Simutyte/PSP_Komanda32_API).

## Running the application

1. Download .NET 6 SDK from [here](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Clone this repository
3. Navigate inside the repository folder
4. Run `dotnet run` command
5. Open swagger UI at `https://localhost:7210/swagger/index.html`

### Note:

For Order endpoints field estimatedTime should have format `HH:mm:ss`. More info [here.](https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/6.0/timespan-serialization-format)