using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Dapper;

namespace WebApplication1.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<string> data = new List<string>()
        {
            "value1",
            "value2"
        };

        IProductRepository repo { get; }

        public ValuesController(IProductRepository productRepository)
        {
            repo = productRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(repo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "ProductGetById")]
        public ActionResult<Product> GetById(int id)
        {
            return Ok(repo.GetById(id));
        }
        
        // GET api/values/5/orderlines
        [HttpGet("{id}/orderlines")]
        public ActionResult<IEnumerable<OrderLine>> GetOrderLines(int id)
        {
            return Ok(repo.GetOrderLines(id));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Product value)
        {
            Product inserted = repo.CreateProduct(value);

            return Created(Url.RouteUrl("ProductGetById", new { id = inserted.ProductId }), inserted);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            data[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            data.RemoveAt(id);
        }
    }
}
