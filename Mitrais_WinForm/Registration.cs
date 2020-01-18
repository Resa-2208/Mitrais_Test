using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mitrais_WinForm
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void FrmRegistration_Load(object sender, EventArgs e)
        {
            getYear();
        }

        private void CbMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            getDate();
        }

        private void CbYear_SelectedValueChanged(object sender, EventArgs e)
        {
            getDate();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            InputsOnOff(this, false);
            ValidateForm();
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Mitrais_WinForm.Login frmLogin = new Mitrais_WinForm.Login();
            frmLogin.Show();
        }

        private void ValidateForm()
        {
            if (txtMobileNum.Text == "" || IsPhoneNumber(txtMobileNum.Text))
            {
                frmError.SetError(txtMobileNum, "Please enter valid Mobile Number");
                InputsOnOff(this, true);
                return;
            }
            else if (txtMobileNum.Text != "" || IsPhoneNumber(txtMobileNum.Text))
            {
                MySqlConnection conn;
                conn = new MySqlConnection(Libs.connectionString);
                try
                {
                    string query = "select MobileNumber from Registration where MobileNumber = '" + txtMobileNum.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        frmError.SetError(txtMobileNum, "Mobile Number already registered");
                        InputsOnOff(this, true);
                        return;
                    }
                    dr.Close();
                    conn.Close();
                }
                catch (Exception ex) { Libs.LogError(ex); }
                finally
                {
                    conn.Close();
                }
            }
            else if (txtEmail.Text == "")
            {
                frmError.SetError(txtEmail, "Please enter valid Email");
                InputsOnOff(this, true);
                return;
            }
            else if (txtEmail.Text != "")
            {
                MySqlConnection conn;
                conn = new MySqlConnection(Libs.connectionString);
                try
                {
                    string query = "select Email from Registration where Email = '" + txtEmail.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        frmError.SetError(txtMobileNum, "Email already registered");
                        InputsOnOff(this, true);
                        return;
                    }
                    dr.Close();
                    conn.Close();
                }
                catch (Exception ex) { Libs.LogError(ex); }
                finally
                {
                    conn.Close();
                }
            }
            else if (txtFirstName.Text == "")
            {
                frmError.SetError(txtFirstName, "Please enter a First Name");
                InputsOnOff(this, true);
                return;
            }
            else if (txtLastName.Text == "")
            {
                frmError.SetError(txtLastName, "Please enter a Last Name");
                InputsOnOff(this, true);
                return;
            }
            else
            {
                Save();
                btnLogin.Visible = true;
            }
        }

        private void Save()
        {
            try
            {
                string cGender;
                if (rbMale.Checked == true) { cGender = "M"; } else if (rbFemale.Checked == true) { cGender = "F"; } else { cGender = "N"; }

                Libs.querystring = "INSERT into registrasi (MobileNumber, FirstName, LastName, Gender, Email, user_add, date_add) " +
                                         "values ('" + txtMobileNum.Text + "', '" + txtFirstName.Text + "', '" + txtLastName.Text + "', " +
                                         "'" + cGender + "', '" + txtEmail.Text + "', '" + Libs.user.ToString() + "', now())";
                Libs.Requery();
            }
            catch (Exception ex)
            {
                Libs.LogError(ex);
            }
        }
        
        #region Functions
        private void getDate()
        {
            if (!string.IsNullOrEmpty(cbMonth.Text) && cbMonth.Text != "Month")
            {
                cbDate.DataSource = null;
                List<int> dt = new List<int>();
                int nMonth = cbMonth.SelectedIndex + 1;

                int nDay = DateTime.DaysInMonth(Convert.ToInt32(cbYear.SelectedValue), nMonth);
                for (int i = 0; i < nDay; i++)
                    dt.Add(i + 1);

                cbDate.DataSource = dt;
                this.Controls.Add(cbDate);
            }
        }

        private void getYear()
        {
            List<int> dt = new List<int>();

            for (int i = 0; i < 30; i++)
                dt.Add(Convert.ToInt32(DateTime.Today.AddYears(i - 29).Year));

            cbYear.DataSource = dt;
            this.Controls.Add(cbYear);
            cbYear.Text = DateTime.Today.Year.ToString();
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }

        void InputsOnOff(Control ctrls, bool act)
        {
            foreach (Control ctrl in ctrls.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Enabled = act;
                }
                else
                {
                    if (ctrl is ComboBox)
                    {
                        ((ComboBox)ctrl).Enabled = act;
                    }
                    else
                    {
                        if (ctrl is CheckBox)
                        {
                            ((CheckBox)ctrl).Enabled = act;
                        }
                        else
                        {
                            if (ctrl is RadioButton)
                            {
                                ((RadioButton)ctrl).Enabled = act;
                            }
                            else
                            {
                                if (ctrl is Button)
                                {
                                    ((Button)ctrl).Enabled = act;
                                }
                                else
                                {
                                    if (ctrl is DateTimePicker)
                                    {
                                        ((DateTimePicker)ctrl).Enabled = act;
                                    }
                                    else
                                    {
                                        if (ctrl is MaskedTextBox)
                                        {
                                            ((MaskedTextBox)ctrl).Enabled = act;
                                        }
                                        else { InputsOnOff(ctrl, act); }
                                    }
                                }
                            }
                        }
                    }

                }

            }

        }

        #endregion

    }
}
