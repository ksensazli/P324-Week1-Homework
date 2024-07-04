namespace w1_homework.Models;

// Define the Product model
public class Product
{
    // Unique identifier for the product
    public int Id { get; set; }
    
    // Name of the product
    public string Name { get; set; }
    
    // Price of the product
    public decimal Price { get; set; }
    
    // Description of the product
    public string Description { get; set; }
}