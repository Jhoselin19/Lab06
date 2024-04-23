using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Lógica de interacción para List.xaml
    /// </summary>
    /// 

    public partial class List : Window
    {
        string connectionString = "Data Source=LAPTOP-19VR9K1O\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=user01;Password=123456";
        public List()
        {
            InitializeComponent();
            LoadData();
            ListarClientes.SelectionChanged += ListarClientes_SelectionChanged;
        }

        private void LoadData()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();

                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("ListClients", connection);
                command.CommandType = CommandType.StoredProcedure;

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string idCliente = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? string.Empty : reader.GetString("idCliente");
                    string NombreCompañia = reader.IsDBNull(reader.GetOrdinal("NombreCompañia")) ? string.Empty : reader.GetString("NombreCompañia");
                    string NombreContacto = reader.IsDBNull(reader.GetOrdinal("NombreContacto")) ? string.Empty : reader.GetString("NombreContacto");
                    string CargoContacto = reader.IsDBNull(reader.GetOrdinal("CargoContacto")) ? string.Empty : reader.GetString("CargoContacto");
                    string Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? string.Empty : reader.GetString("Direccion");
                    string Ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? string.Empty : reader.GetString("Ciudad");
                    string Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? string.Empty : reader.GetString("Region");
                    string CodPostal = reader.IsDBNull(reader.GetOrdinal("CodPostal")) ? string.Empty : reader.GetString("CodPostal");
                    string Pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? string.Empty : reader.GetString("Pais");
                    string Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? string.Empty : reader.GetString("Telefono");
                    string Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? string.Empty : reader.GetString("Fax");


                    clientes.Add(new Cliente
                    {
                        idCliente = idCliente,
                        NombreCompañia = NombreCompañia,
                        NombreContacto = NombreContacto,
                        CargoContacto = CargoContacto,
                        Direccion = Direccion,
                        Ciudad = Ciudad,
                        Region = Region,
                        CodPostal = CodPostal,
                        Pais = Pais,
                        Telefono = Telefono,
                        Fax = Fax
                    });


                }
                connection.Close();

                ListarClientes.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }

        }

        private void ListarClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si hay alguna fila seleccionada
            if (ListarClientes.SelectedItem != null)
            {
                // Obtiene el cliente seleccionado del DataGrid
                Cliente clienteSeleccionado = (Cliente)ListarClientes.SelectedItem;

                // Asigna los valores de los campos del cliente seleccionado a los campos del formulario de edición
                txtIdClient.Text = clienteSeleccionado.idCliente;
                txtCompany.Text = clienteSeleccionado.NombreCompañia;
                txtContact.Text = clienteSeleccionado.NombreContacto;
                txtPosition.Text = clienteSeleccionado.CargoContacto;
                txtAddress.Text = clienteSeleccionado.Direccion;
                txtCity.Text = clienteSeleccionado.Ciudad;
                txtRegion.Text = clienteSeleccionado.Region;
                txtPostalCode.Text = clienteSeleccionado.CodPostal;
                txtCountry.Text = clienteSeleccionado.Pais;
                txtPhone.Text = clienteSeleccionado.Telefono;
                txtFax.Text = clienteSeleccionado.Fax;
            }
        }


        private void UpdateClient(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateClients", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Valores
                    cmd.Parameters.AddWithValue("@ID", txtIdClient.Text);
                    cmd.Parameters.AddWithValue("@NombreCompañia", txtCompany.Text);
                    cmd.Parameters.AddWithValue("@NombreContacto", txtContact.Text);
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
                    LoadData();
                }
            }

        }

        private void DropClient(object sender, RoutedEventArgs e)
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
                    LoadData();
                }
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            txtIdClient.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtRegion.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
    }
}
