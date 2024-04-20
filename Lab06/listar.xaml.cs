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
    /// Lógica de interacción para listar.xaml
    /// </summary>
    public partial class listar : Window
    {
        string connectionString = "Data Source=LAB1504-22\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=user01;Password=123456";

        public listar()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("GetClients", connection);
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
        // Evento para notificar cuando se selecciona un cliente
        public event EventHandler<ClienteSeleccionadoEventArgs> ClienteSeleccionado;

        // Método para activar el evento ClienteSeleccionado
        protected virtual void OnClienteSeleccionado(ClienteSeleccionadoEventArgs e)
        {
            ClienteSeleccionado?.Invoke(this, e);
        }

        // Método que se ejecuta cuando cambia la selección en ListarClientes
        private void ListarClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListarClientes.SelectedItem != null)
            {
                // Obtiene el cliente seleccionado
                Cliente clienteSeleccionado = (Cliente)ListarClientes.SelectedItem;
                // Activa el evento ClienteSeleccionado y pasa el cliente seleccionado
                OnClienteSeleccionado(new ClienteSeleccionadoEventArgs(clienteSeleccionado));
            }
        }
    }

    public class ClienteSeleccionadoEventArgs : EventArgs
    {
        public Cliente ClienteSeleccionado { get; }

        public ClienteSeleccionadoEventArgs(Cliente clienteSeleccionado)
        {
            ClienteSeleccionado = clienteSeleccionado;
        }
    }
}