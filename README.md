# Lottery API

This project is an ASP.NET Core Web API for purchasing lottery tickets. The API interacts with a mock third-party API to resell tickets, ensures duplicate requests are not processed multiple times, and maintains a local record of purchases using an EF Core in-memory database.

## Prerequisites

- .NET SDK 6.0 or later
- A development environment (e.g., Visual Studio, Visual Studio Code, or JetBrains Rider)

## Getting Started

### 1. Clone the Repository
```sh
git clone <repository-url>
cd <repository-folder>
```

### 2. Build the Project
```sh
dotnet build
```

### 3. Run the API
```sh
dotnet run
```

By default, the API will run on `http://localhost:5000` and `https://localhost:5001`.

### 4. Access Swagger UI

Once the API is running, open your browser and navigate to:

```
https://localhost:5001/swagger
```

or

```
http://localhost:5000/swagger
```

Swagger UI provides an interactive interface to test API endpoints.

## API Endpoints

### Purchase Lottery Ticket
**Endpoint:** `POST /api/lottery/purchase`

**Request Body:**
```json
{
    "customerId": "e7a06d19-28e4-4d91-9c9d-046fdc7e21f0",
    "drawId": 1,
    "numberOfTickets": 2,
    "timestamp": "2024-04-02T12:00:00Z"
}
```

**Response:**
```json
{
    "purchaseId": "b2a1d8e3-5247-4a84-b6f8-74a5f712e3d1",
    "totalCost": 20.0
}
```

## Running Tests

To run unit tests, use the following command:
```sh
dotnet test
```

This will execute all unit tests and ensure the application behaves as expected.

## Notes

- The API includes duplicate request detection.
- A draw has a finite number of 500 tickets available.
- The third-party API is mocked within the application for testing purposes.

## License

This project is open-source and available for use under the GNU GENERAL PUBLIC LICENSE.

