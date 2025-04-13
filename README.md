# Nicopolis Ad Istrum

A modern web application for managing and showcasing the archaeological site of Nicopolis ad Istrum, built with ASP.NET Core 8.0.

## Overview

Nicopolis Ad Istrum is a web platform designed to manage and display information about the ancient Roman city of Nicopolis ad Istrum. The application provides features for managing exhibits, events, collections, and user roles, making it an essential tool for archaeologists, researchers, and visitors interested in this historical site.

## Features

- **User Management**: Role-based authentication system with different access levels (Admin, Associate, Employee, User)
- **Exhibit Management**: Comprehensive system for managing archaeological exhibits
- **Event Management**: Tools for organizing and managing events
- **Collection Management**: System for organizing and displaying collections
- **Modern UI**: Responsive design with a user-friendly interface

## Technical Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: MVC with Razor Views
- **Development Tools**: Visual Studio 2022

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 (recommended)

### Installation

1. Clone the repository
```bash
git clone https://github.com/yourusername/Nicopolis-Ad-Istrum.git
```

2. Navigate to the project directory
```bash
cd Nicopolis-Ad-Istrum
```

3. Update the connection string in `appsettings.json`

4. Run the application
```bash
dotnet run
```

## Project Structure

- **Areas/**: Contains area-specific controllers and views
- **Controllers/**: MVC controllers
- **Data/**: Database context and configurations
- **Models/**: Data models and view models
- **Services/**: Business logic implementation
- **Views/**: Razor views
- **wwwroot/**: Static files (CSS, JavaScript, images)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact

For any queries or suggestions, please open an issue in the GitHub repository. 