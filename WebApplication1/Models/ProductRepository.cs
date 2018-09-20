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
            using(IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery =
                    "SELECT ProductId, ProductName, Price FROM dbo.Product WHERE ProductId < @ID";
                return conn.Query<Product>(sQuery, new { ID = 10 });
            }
        }

        public Product GetById(int id)
        {
            using(IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery =
                    "SELECT ProductId, ProductName, Price FROM dbo.Product WHERE ProductId = @ID";
                return conn.Query<Product>(sQuery, new { ID = id }).FirstOrDefault();
            }
        }

        public IEnumerable<OrderLine> GetOrderLines(int id)
        {
            using(IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery =
                    "SELECT OrderId, LineNumber, ProductId, Qty, LineTotal FROM dbo.OrderLine WHERE ProductId = @ID";
                return conn.Query<OrderLine>(sQuery, new { ID = id });
            }
        }

        public Product CreateProduct(Product prod)
        {
            using(IDbConnection conn = Connection)
            {
                conn.Open();
                string sQuery = @"
                        INSERT INTO [dbo].[Product] 
                            ([ProductName],[Price],[Active],[Stock])
                        VALUES
                            (@ProductName,@Price,1,1);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
                int insertedId = conn.Query<int>(sQuery, prod).FirstOrDefault();
                prod.ProductId = insertedId;
                return prod;
            }
        }

    }
}