using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data;
using System.IO;

namespace Mitrais_WinForm
{
    class Libs
    {
        public static string connectionString = "", querystring = "", Code = "", Name = "",
                                CodeID, user, user_id, date1, date2, ParameterFunc, FormName, Messages,
                                ParameterBranch, ParameterLevel, ParameterButton, angka,
                                pCommand, pWhere, pCode, pName, TableName,
                                pFields, pField, pOrder, pUserActive;
        public static MySqlConnection conn;
        public static MySqlCommand cmd;
        public static MySqlDataAdapter da;
        public static DataTable dt, dt2;
        public static DataSet ds;
        public static int getScalar = 0;
        public static string Path;


        #region CONNECTION
        public static void CallSettingsConn()
        {
            string ip = "";
            string db = "";
            string user = "";
            string pass = "";
            ip = Mitrais_WinForm.Properties.Settings.Default.IPaddress;
            db = Mitrais_WinForm.Properties.Settings.Default.Database;
            user = Mitrais_WinForm.Properties.Settings.Default.Username;
            pass = Mitrais_WinForm.Properties.Settings.Default.Password;
            string conn = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Allow User Variables=True;", ip, db, user, pass);
            connectionString = conn;
        }
        #endregion
        
        #region LOGERROR
        public static void LogError(Exception ex)
        {
            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                Path = Application.StartupPath + "\\logs\\Errors.txt";
                using (StreamWriter writer = new StreamWriter(Path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch(Exception Ex) { }
        }
        #endregion

        #region REQUERY
        public static void Requery()
        {
            CallSettingsConn();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(querystring, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                LogError(ex);
            }
        }

        public static void Scalarquery()
        {
            CallSettingsConn();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(querystring, conn);
                conn.Open();
                getScalar = (Int32)cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                LogError(ex);
            }
        }
        #endregion


    }
}
