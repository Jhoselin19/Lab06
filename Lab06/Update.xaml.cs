using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab06
{
    /// <summary>
    /// Lógica de interacción para Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        string connectionString = "Data Source=LAB1504-22\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=user01;Password=123456";
        public Update()
        {
            InitializeComponent();
            // Crear una instancia de la ventana listar
            listar ventanaListar = new listar();

            // Suscribir al evento ClienteSeleccionado de la ventana listar
            ventanaListar.ClienteSeleccionado += Listar_ClienteSeleccionado;
        }
        private void Listar_ClienteSeleccionado(object sender, ClienteSeleccionadoEventArgs e)
        {
            // Actualizar los campos con los datos del cliente seleccionado
            Cliente clienteSeleccionado = e.ClienteSeleccionado;
            txtIdClient.Text = clienteSeleccionado.idCliente;
            txtCompany.Text = clienteSeleccionado.NombreCompañia;
            txtContact.Text = clienteSeleccionado.NombreContacto;
            txtPosition.Text = clienteSeleccionado.CargoContacto;
            txtAddress.Text = clienteSeleccionado.Direccion;
            txtCity.Text = clienteSeleccionado.Ciudad;
            txtRegion.Text = clienteSeleccionado.Region;
            txtPostalCode.Text = clienteSeleccionado.CodPostal;
            txtPhone.Text = clienteSeleccionado.Telefono;
            txtFax.Text = clienteSeleccionado.Fax;
            txtCountry.Text = clienteSeleccionado.Pais;
        }
        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("USP_UpdateClientes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Valores
                    cmd.Parameters.AddWithValue("@IdCliente", txtIdClient.Text);
                    cmd.Parameters.AddWithValue("@Compañia", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@Contacto", txtContact.Text);
                    cmd.Parameters.AddWithValue("@CargoContacto", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Ciudad", txtCity.Text);
                    cmd.Parameters.AddWithValue("@Region", txtRegion.Text);
                    cmd.Parameters.AddWithValue("@CodPostal", txtPostalCode.Text);
                    cmd.Parameters.AddWithValue("@Pais", txtCountry.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Fax", txtFax.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente actualizado");


                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listar list = new listar();
            list.ShowDialog();
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteClientById", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", txtIdClient.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente Eliminado");
                }
            }
        }

    }
}
