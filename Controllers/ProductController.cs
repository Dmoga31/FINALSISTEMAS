using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using API_Rest.Models;
using MySql.Data.MySqlClient;
using API_Rest.Services;
using System.Net.Http;

namespace API_Rest.Controllers
{
    public class ProductController : ApiController
    {

        private ProductRepository productRepository;

        /*private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        // Método para abrir la conexión a la base de datos
        private MySqlConnection GetSqlConnection()
        {
            return new MySqlConnection(connectionString);
        }*/

        // Repositorio de producto en caché
        public ProductController()
        {
            this.productRepository = new ProductRepository();
        }

        // GET usando la caché
        [HttpGet]
        [Route("api/product")]
        public Product[] GetAll()
        {
            return productRepository.GetAllProducts();
        }

        [HttpPost]
        [Route("api/product")]
        public HttpResponseMessage Post(Product product)
        {
            this.productRepository.SaveProduct(product);

            var response = Request.CreateResponse(System.Net.HttpStatusCode.Created, product);

            return response;
        }

        /*
        // GET api/products
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                // Realiza una consulta SELECT para obtener todos los productos
                string query = "SELECT p.Id, p.Name, p.Price, p.Quantity, t.TypeId " +
                               "FROM products p " +
                               "JOIN Type t ON p.TypeId = t.TypeId";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            TypeId = Convert.ToInt32(reader["TypeId"]),
                            Price = Convert.ToDouble(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };

                        products.Add(product);
                    }
                }
            }

            return products;
        }

        // GET api/products/5
        [HttpGet]
        [Route("api/product")]
        public Product GetById(int id)
        {
            using (MySqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                // Realiza una consulta SELECT para obtener el producto por su Id
                string query = "SELECT p.Id, p.Name, p.Price, p.Quantity, t.TypeId " +
                               "FROM products p " +
                               "JOIN Type t ON p.TypeId = t.TypeId " +
                               "WHERE p.Id = @Id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            TypeId = Convert.ToInt32(reader["TypeId"]),
                            Price = Convert.ToDouble(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };

                        return product;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // POST api/products
        [HttpPost]
        [Route("api/products")]
        public bool InsertProduct([FromBody] Product product)
        {
            using (MySqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                // Realiza la consulta INSERT para agregar un nuevo producto
                string insertQuery = "INSERT INTO products (Name, TypeId, Price, Quantity) VALUES (@Name, @TypeId, @Price, @Quantity)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);

                // Parámetros
                insertCmd.Parameters.AddWithValue("@Name", product.Name);
                insertCmd.Parameters.AddWithValue("@TypeId", product.TypeId);
                insertCmd.Parameters.AddWithValue("@Price", product.Price);
                insertCmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                int rowsAffected = insertCmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        // PUT api/products
        [HttpPut]
        [Route("api/product")]
        public IHttpActionResult PutProduct([FromBody] Product updatedProduct)
        {
            try
            {
                using (MySqlConnection connection = GetSqlConnection())
                {
                    connection.Open();

                    // Verifica si el producto con el nombre proporcionado existe
                    if (ProductExists(updatedProduct.Name, connection))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("UPDATE products SET TypeId = @TypeId, Price = @Price, Quantity = @Quantity WHERE Name = @Name", connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", updatedProduct.Name);
                            cmd.Parameters.AddWithValue("@TypeId", updatedProduct.TypeId);
                            cmd.Parameters.AddWithValue("@Price", updatedProduct.Price);
                            cmd.Parameters.AddWithValue("@Quantity", updatedProduct.Quantity);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                return Ok(); // 200 OK
                            }
                            else
                            {
                                return InternalServerError(new Exception("No se pudo actualizar el producto.")); // 500 Internal Server Error
                            }
                        }
                    }
                    else
                    {
                        // El producto con el nombre proporcionado no existe
                        return NotFound(); // 404 Not Found
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return InternalServerError(ex);
            }
        }

        // Método para verificar si un producto con el nombre proporcionado existe
        private bool ProductExists(string productName, MySqlConnection connection)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM products WHERE Name = @Name", connection))
            {
                cmd.Parameters.AddWithValue("@Name", productName);

                int productCount = Convert.ToInt32(cmd.ExecuteScalar());

                return productCount > 0;
            }
        }


        // DELETE api/products/5
        [HttpDelete]
        [Route("api/product/{id}")]
        public bool DeleteProduct(int id)
        {
            using (MySqlConnection connection = GetSqlConnection())
            {
                connection.Open();

                // Realiza una consulta DELETE para eliminar un producto por su Id
                string query = "DELETE FROM products WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }*/
    }
}
