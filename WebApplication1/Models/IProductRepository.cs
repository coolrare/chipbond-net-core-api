using System.Collections.Generic;

namespace WebApplication1.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
    }
}