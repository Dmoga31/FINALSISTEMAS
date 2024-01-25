using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_Rest.Models;

namespace API_Rest.Services
{
    public class ProductRepository
    {
        private const string CacheKey = "ProductStore";

        public ProductRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var products = new Product[]
                    {
                    };

                    ctx.Cache[CacheKey] = products;
                }
            }
        }

        public Product[] GetAllProducts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Product[])ctx.Cache[CacheKey];
            }

            return new Product[]
                {
            new Product
            {
                Id = 0, Name = "Placeholder", TypeId = 0, Price=0, Quantity=0
            }
               };
        }

        public bool SaveProduct(Product product)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Product[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(product);
                    ctx.Cache[CacheKey] = currentData.ToArray();
                    Console.WriteLine("SUCCESS");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("ERROR");
                    return false;
                }
            }

            return false;
        }
    }
}