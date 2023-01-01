using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class ProductDTO 
{
    public Guid ProductId { get; set; }
    public string Name { get; set;}
    public decimal Price { get; set; }

    public string barcdoe { get; set; }

}