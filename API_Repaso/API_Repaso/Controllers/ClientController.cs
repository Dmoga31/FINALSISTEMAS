using API_Repaso.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Repaso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly Service.ProductService _ProductService;

        public ClientController(Service.ProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            var products = _ProductService.GetProducts();
            return Ok(products);
        }

        [HttpGet("ProductsById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var products = _ProductService.GetProductById(id);
            return Ok(products);
        }

        [HttpGet("ProductsByType/{type}")]
        public IActionResult GetProductsByType(string type)
        {
            var products = _ProductService.GetProductsByType(type);
            return Ok(products);
        }

        [HttpPost("BuyProduct")]
        public IActionResult BuyProduct([FromBody] Product request)
        {
            var productId = request.id;
            var productName = request.name;
            var productQuantity = request.quantity;

            var product = _ProductService.GetProductById(productId);

            if (product == null)
            {
                return NotFound($"Product with name '{productName}' not found.");
            }

            if (product.quantity == 0)
            {
                return BadRequest($"Product with name '{productName}' out of stock.");
            }
            else if (product.quantity > 0 && productQuantity > product.quantity)
            {
                return BadRequest($"There is not enough '{productName}' ({product.quantity}) for you to buy... try a smaller amount.");
            }

            _ProductService.BuyProduct(productId, productQuantity);
            return Ok($"Product '{productName}' purchased successfully.");
        }
    }
}
