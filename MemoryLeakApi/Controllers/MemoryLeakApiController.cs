using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MemoryLeakApi.Controllers
{
    public class Product
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public char[] Details { get; set; } = new char[10000];

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class MemoryLeakController : ControllerBase
    {
        private static List<Product> products = new List<Product>();

        private readonly ILogger<MemoryLeakController> _logger;

        public MemoryLeakController(ILogger<MemoryLeakController> logger)
        {
            _logger = logger;
        }

        [HttpGet("leak")]
        public IActionResult LeakMemory()
        {
            for (int i = 0; i < 10000; i++)
            {
                products.Add(new Product(i, "product" + i));
            }

            _logger.LogInformation($"Added 10000 products. Total count: {products.Count}");
            return Ok($"Leaked memory. Total products: {products.Count}");
        }

        [HttpGet("count")]
        public IActionResult GetProductCount()
        {
            return Ok($"Total products: {products.Count}");
        }
    }
}
