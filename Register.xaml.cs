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

namespace LoginForm
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        static string CS = "data source=SIBUSISO; database=SCHOOL_ST10202790; Integrated Security=SSPI; TrustServerCertificate=True";

        SqlConnection con = new SqlConnection(CS);
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlCommand cmd2;
       MainWindow main =new MainWindow();
       
        public Register()
        {
            InitializeComponent();
            Reset();
        }
        public void Reset()
        {
            string preID;
            object result;
            string result2;
            string command1 = "SELECT USER_ID FROM USERS";
            string command2 = "SELECT max(USER_ID)+1 FROM USERS";
            cmd1 = new SqlCommand(command1, con);
            cmd = new SqlCommand(command2, con);
            try
            {
                con.Open();
                result = cmd1.ExecuteScalar();
                result2 = cmd.ExecuteScalar().ToString();

                con.Close();
                if (result == null)
                {
                    preID = "101";
                    ID.Text = preID;
                }
                else
                {
                    ID.Text = result2;
                }
            }
            catch (Exception ex) { }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string id = ID.Text;
            string n = name.Text;
            string s = surname.Text;
            string p = EncodePasswordToBase64(password.Password).ToString();
            try
            {

                string command = string.Format("INSERT INTO USERS(USER_ID,NAME,SURNAME,PASSWORD) VALUES({0},'{1}','{2}','{3}')", id, n, s, p);
                cmd2 = new SqlCommand(command, con);
                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                error.Text = "Successful Register " + n;
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                con.Close();
            }
            Reset();
        }
        //this function Convert to Encord your Password
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
           
            main.Show();
            this.Close();
        }
    }
}

