# .NET 8 Web API - Product Management

## Overview

This is a simple .NET 8 Web API project for managing products. The API allows for CRUD (Create, Read, Update, Delete) operations on a list of products. Products are stored in-memory for demonstration purposes.

## Getting Started

To get started with this project, you need to have [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

### Clone the Repository

```bash
git clone [<repository-url>](https://github.com/ksensazli/P324-Week1-Homework)
cd P324-Week1-Homework
```

### Build and Run the Project

```bash
dotnet build
dotnet run
```
## API Endpoints

### Product Model
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
```

### Endpoints
Get All Products
- URL: /api/products
- Method: GET
- Query Parameters:
  - name (optional) - Filter products by name.
  - sort (optional) - Sort by name or price.
- Response:
  - 200 OK - Returns a list of products.
 
Get Product by ID
- URL: /api/products/{id}
- Method: GET
- URL Parameters:
  - id - ID of the product.
- Response:
  - 200 OK - Returns the product with the specified ID.
  - 404 Not Found - If the product with the specified ID does not exist.
 
Create Product
- URL: /api/products
- Method: POST
- Body: Product object.
- Response:
  - 201 Created - Returns the created product.
  - 400 Bad Request - If the provided product is null.


Update Product
- URL: /api/products/{id}
- Method: PUT
- URL Parameters:
  - id - ID of the product to be updated.
- Body: Product object with updated details.
- Response:
  - 204 No Content - If the update is successful.
  - 400 Bad Request - If the product is null or the ID does not match.
  - 404 Not Found - If the product with the specified ID does not exist.
 

Delete Product
- URL: /api/products/{id}
- Method: DELETE
- URL Parameters:
  - id - ID of the product to be deleted.
- Response:
  - 204 No Content - If the delete is successful.
  - 404 Not Found - If the product with the specified ID does not exist.
