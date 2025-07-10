using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Custom_Class_Library;

namespace LoginForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string CS = "data source=SIBUSISO; database=SCHOOL_ST10202790; Integrated Security=SSPI; TrustServerCertificate=True";

        SqlConnection con = new SqlConnection(CS);
        SqlCommand cmd;
        MainApp app = new MainApp();
        Boolean drStatus;
        String dataName;
        String dataSurname;
        String dataPassword;


       public string n;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Register register = new Register();
           
            
            register.Show();
            this.Close();
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string id = ID.Text;
             n = name.Text;
            string s = surname.Text;
            string p = password.Password;
           
            string command = string.Format("SELECT NAME, SURNAME, PASSWORD FROM USERS WHERE USER_ID={0}", id);
            string SqlQuery = "SELECT * FROM MODULES WHERE USER_ID=" + id;
            SqlCommand scom = new SqlCommand(SqlQuery, con);
            cmd = new SqlCommand(command, con);
             con.Open();
           SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                drStatus = dr.Read();
                dataName = dr["NAME"].ToString();
                dataSurname = dr["SURNAME"].ToString();
                dataPassword = DecodeFrom64(dr["PASSWORD"].ToString());
            }catch (Exception ex)
            {
                MessageBox.Show("Register an account first");
            }
            con.Close();
            con.Open();
            SqlDataReader reader = scom.ExecuteReader();
         
            if (drStatus) {
                if (dataName == n) {
                    if (dataSurname == s)
                    {
                        if (dataPassword == p)
                        {
                            display.Text = "Successful Login";
                            app.userID = id;
                            app.tbSessionName.Text ="User " +n;
                            app.Show();
                            this.Close();

                            
                           
                            while (reader.Read())
                            {
                                string display = "Course Information =" + reader.GetValue(0) + " Course  Name " + reader.GetValue(2);
                                app.myModules[(string)reader.GetValue(0)] = new Module((string)reader.GetValue(0), (string)reader.GetValue(2), (int)reader.GetValue(3), (int)reader.GetValue(4));
                               
                                app.btsemester.IsEnabled = true;
                                app.btwork.IsEnabled = true;
                                app.btdisplayhours.IsEnabled = true;
                            }
                           
                            
                          
                        }
                        else
                        {
                            display.Text = "Incorrect User Password";
                        }
                    }
                    else { display.Text = "Incorrect User Surname"; }
                }
                else { display.Text = "Incorrect User Name"; }
            }

            con.Close();
            con.Open();
            string command1 = "SELECT * FROM WORK WHERE USER_ID ="+id;
           SqlCommand sqlCommand=new SqlCommand(command1, con);
            SqlDataReader reader1 = sqlCommand.ExecuteReader();
            if (drStatus)
            {
                if (dataName == n)
                {
                    if (dataSurname == s)
                    {
                        if (dataPassword == p)
                        {
                           



                            while (reader1.Read())
                            {
                                app.myWork[(string)reader1.GetValue(0)] = new Work((string)reader1.GetValue(0), (string)reader1.GetValue(2), (int)reader1.GetValue(3));
                               
                                app.btsemester.IsEnabled = true;
                                app.btwork.IsEnabled = true;
                                app.btdisplayhours.IsEnabled = true;
                            }



                        }
                    }
                }
            }
            con.Close() ;
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            app.Show();
            this.Close();
        }
    }
}
