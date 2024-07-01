using Microsoft.AspNetCore.Mvc;
using w1_homework.Models;

namespace w1_homework.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product1", Price = 10.0M, Description = "Description1" },
        new Product { Id = 2, Name = "Product2", Price = 20.0M, Description = "Description2" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts([FromQuery] string name, [FromQuery] string sort)
    {
        var result = products.AsEnumerable();

        if (!string.IsNullOrEmpty(name))
        {
            result = result.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(sort))
        {
            result = sort switch
            {
                "name" => result.OrderBy(p => p.Name),
                "price" => result.OrderBy(p => p.Price),
                _ => result
            };
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        product.Id = products.Max(p => p.Id) + 1;
        products.Add(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null || product.Id != id)
        {
            return BadRequest();
        }

        var existingProduct = products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        products.Remove(product);

        return NoContent();
    }
}