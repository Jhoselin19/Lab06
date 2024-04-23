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
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        string connectionString = "Data Source=LAPTOP-19VR9K1O\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=user01;Password=123456";
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterClient(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("InsertClient", conn))
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
                    MessageBox.Show("Cliente registrado correctamente");


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

        private void ViewClients(object sender, RoutedEventArgs e)
        {
            List list = new List();
            list.ShowDialog();
        }
    }
}
