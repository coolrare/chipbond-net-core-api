using System.Collections.Generic;

namespace WebApplication1.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        IEnumerable<OrderLine> GetOrderLines(int id);
        Product CreateProduct(Product product);
    }
}