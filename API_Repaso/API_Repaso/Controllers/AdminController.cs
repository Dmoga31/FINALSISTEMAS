using API_Repaso.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;

namespace API_Repaso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly Service.ProductService _ProductService;

        public AdminController(Service.ProductService ProductService)
        {
            _ProductService = ProductService;
        }

        // GET: api/Admin/Products
        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            var products = _ProductService.GetProducts();
            return Ok(products);
        }

        // GET: api/Client/Products/id
        [HttpGet("ProductsById/{id}")]
        public IActionResult GetProductsById(int id)
        {
            var product = _ProductService.GetProductById(id);
            if (product!= null) {
                return Ok(product);
            } else
            {
                return BadRequest("Product with id " + id + " wasn´t found...");
            }
        }

        // GET: api/Client/Products/type
        [HttpGet("ProductsByType/{type}")]
        public IActionResult GetProductsByType(string type)
        {
            var products = _ProductService.GetProductsByType(type);
            if (products.Any())
            {
                return Ok(products);
            }
            else
            {
                // Modifica el mensaje según tus necesidades
                return NotFound($"No products found with type '{type}'.");
            }
        }


        // POST: api/Admin/AddProduct
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            if (_ProductService.AddProduct(product) == false)
            {
                return BadRequest(product.name + " already exists");
            }
            return CreatedAtAction(nameof(GetProducts), new { }, product);

        }

        // PUT: api/Admin/UpdateProduct/1
        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _ProductService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            if(updatedProduct.price <= 0)
            {
                return BadRequest("Price can´t be 0, please try a different amount");
            } else
            {
                _ProductService.UpdateProduct(id, updatedProduct);
                return Ok("Product updated");
            }
        }

        // PUT: api/Admin/AddMoreProduct/1
        [HttpPut("AddMoreProduct/{id}")]
        public IActionResult AddMoreProduct(int id, [FromBody] Product productQuantity)
        {
            var product = _ProductService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            _ProductService.AddMoreProduct(id, productQuantity);
            return Ok("More product added");
        }

        // DELETE: api/Admin/DeleteProduct/1
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _ProductService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product with id " + product.id + " wasn´t found");
            }

            _ProductService.DeleteProduct(id);
            return NoContent();
        }
    }
}