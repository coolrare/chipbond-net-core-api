using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Models
{
    public class ProductRepository : IProductRepository
    {
        IConfiguration _config { get; }

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery =
                    "SELECT ProductId, ProductName, Price FROM dbo.Product WHERE ProductId < @ID";
                return conn.Query<Product>(sQuery, new { ID = 10 });
            }
        }
    }
}
