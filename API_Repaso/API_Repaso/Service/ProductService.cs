using API_Repaso.Models;
using Microsoft.OpenApi.Models;
using System.Xml.Linq;

namespace API_Repaso.Service
{
    public class ProductService
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { id = 1, type = "Bakery", name = "Bread", price = 2.5, quantity = 10 },
            new Product { id = 2, type = "Dairy", name = "Milk", price = 1.8, quantity = 20 },
            new Product { id = 3, type = "Meat", name = "Eggs", price = 3.0, quantity = 30 }
        };




        public List<Product> GetProducts()
        {
            return _products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.id == id);
        }

        public List<Product> GetProductsByType(string type)
        {
            return _products.Where(p => p.type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool CheckIfProductExist(Product product)
        {
            return _products.Any(p => p.name.ToLower() == product.name.ToLower());
        }

        public bool AddProduct(Product product)
        {
            if (CheckIfProductExist(product))
            {
                return false;
            }
            product.id = GenerateProductid();
            _products.Add(product);
            return true;
        }

        public void UpdateProduct(int id, Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.id == id);
            if (product != null && product.quantity > 0)
            {
                product.name = updatedProduct.name;
                product.type = updatedProduct.type;
                product.price = updatedProduct.price;
                product.quantity = updatedProduct.quantity;
            }
        }

        public void AddMoreProduct(int id, Product productQuantity)
        {
            var product = _products.FirstOrDefault(p => p.id == id);
            if (product != null && product.quantity > 0)
            {
                product.quantity = product.quantity + productQuantity.quantity;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public void BuyProduct(int id, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.id == id);
            if (product != null && product.quantity > 0)
            {
                if(quantity > product.quantity)
                {
                    quantity = 0;
                }

                product.quantity = product.quantity - quantity;
            }
        }

        private int GenerateProductid()
        {
            return _products.Count + 1;
        }

    }
}
