using Microsoft.AspNetCore.Mvc;
using w1_homework.Models;

namespace w1_homework.Controllers;

// Define the route for the controller
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    // In-memory list to store products
    private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product1", Price = 10.0M, Description = "Description1" },
        new Product { Id = 2, Name = "Product2", Price = 20.0M, Description = "Description2" }
    };

    // GET: api/products
    // Retrieves the list of products, optionally filtered by name and sorted by name or price
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts([FromQuery] string name, [FromQuery] string sort)
    {
        // Start with all products
        var result = products.AsEnumerable();

        // Filter by name if provided
        if (!string.IsNullOrEmpty(name))
        {
            result = result.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        // Sort by specified field if provided
        if (!string.IsNullOrEmpty(sort))
        {
            result = sort switch
            {
                "name" => result.OrderBy(p => p.Name),
                "price" => result.OrderBy(p => p.Price),
                _ => result
            };
        }

        // Return the filtered and/or sorted list of products
        return Ok(result);
    }

    // GET: api/products/{id}
    // Retrieves a product by its ID
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        // Find the product with the specified ID
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            // Return 404 if not found
            return NotFound();
        }

        // Return the found product
        return Ok(product);
    }

    // POST: api/products
    // Creates a new product
    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] Product product)
    {
        if (product == null)
        {
            // Return 400 if the product is null
            return BadRequest();
        }

        // Set the new product's ID to be one greater than the current maximum ID
        product.Id = products.Max(p => p.Id) + 1;
        products.Add(product);

        // Return 201 Created with the location of the new product
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    // Updates an existing product
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null || product.Id != id)
        {
            // Return 400 if the product is null or the ID doesn't match
            return BadRequest();
        }

        // Find the existing product
        var existingProduct = products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            // Return 404 if not found
            return NotFound();
        }

        // Update the existing product's properties
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;

        // Return 204 No Content
        return NoContent();
    }

    // DELETE: api/products/{id}
    // Deletes a product by its ID
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        // Find the product with the specified ID
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            // Return 404 if not found
            return NotFound();
        }

        // Remove the product from the list
        products.Remove(product);

        // Return 204 No Content
        return NoContent();
    }
}
