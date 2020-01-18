using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mitrais_WinForm
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void CheckConnection()
        {
            string ipaddress = txtIP.Text;
            string DB = txtDB.Text;
            string username = txtUN.Text;
            string password = txtPW.Text;

            string a = String.Format("Data Source={0};Initial Catalog={1};User ID={2};password={3};Pooling=false;", ipaddress, DB, username, password);
            string connectionString = a;

            MySqlConnection conn;
            conn = new MySqlConnection(connectionString);
            try
            {
                Mitrais_WinForm.Properties.Settings.Default.IPaddress = txtIP.Text;
                Mitrais_WinForm.Properties.Settings.Default.Database = txtDB.Text;
                Mitrais_WinForm.Properties.Settings.Default.Username = txtUN.Text;
                Mitrais_WinForm.Properties.Settings.Default.Password = txtPW.Text;
                Properties.Settings.Default.Save();
                this.Close();

                MessageBox.Show("Successfully Connected" + Properties.Settings.Default.IPaddress);

                Mitrais_WinForm.frmRegistration frmRegistration = new Mitrais_WinForm.frmRegistration();
                frmRegistration.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed" + ex.Message);
            }
        }
    }
}
