using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Supermarket
{
    public partial class Form1 : Form
    {
        private readonly string apiUrl = "https://localhost:7143/api";
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnConsultarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                // Consultar todos los productos
                string endpoint = "/Client/Products";
                string jsonResponse = await RealizarSolicitudHttp(endpoint);

                // Deserializar la respuesta JSON a una lista de productos
                var productos = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);

                // Mostrar los productos en un ListBox o cualquier otro control
                MessageBox.Show(productos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar productos: {ex.Message}");
            }
        }
    }
}
