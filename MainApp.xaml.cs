using Custom_Class_Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainApp : Window
    {
        public MainApp()
        {
            InitializeComponent();

        }

        public int countBmod = 0;
        string modCode;
        int noMod;
        int selfStuHours;
        int noSemesterWeeks;
        string semesterDay;
        int remainingHours;
        Thread tread1;
        Thread tread2;
        List<int> listNocredit = new List<int>();

        public Module myModules = new Module();
        public Work myWork = new Work();


        static string CS = "data source=SIBUSISO; database=SCHOOL_ST10202790; Integrated Security=SSPI; TrustServerCertificate=True";

        SqlConnection con = new SqlConnection(CS);
        SqlCommand cmd;

        public string userID;


        private void bmod_Click(object sender, RoutedEventArgs e)
        {


            modCode = tbmod.Text;
            try
            {
                myModules[modCode] = new Module(modCode, tbmodname.Text, Convert.ToInt32(tbmodcredits.Text), Convert.ToInt32(tbmodhours.Text));
                listNocredit.Add(Convert.ToInt32(tbmodcredits.Text));
               tread1 = new Thread(insertDataMod);
                tread1.Start();
                error.Text = "Added a Module " + modCode;
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                con.Close();
            }
            btsemester.IsEnabled = true;


            countBmod++;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbdisplay.Items.Add(myModules[tbitem.Text]);
                lbdisplay.Items.Add(myWork[tbitem.Text]);

            }
            catch (Exception ex)
            {
                error.Text = ex.Message + " is incorrect!";
            }

        }

        private void btwork_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myWork[tbworkcode.Text] = new Work(tbworkcode.Text, cworkdate.Text, Convert.ToInt32(tbworkhours.Text));
                tread2 = new Thread(insertDataWork);
                tread2.Start();

            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                con.Close();
            }
            try
            {
                selfStuHours = (myModules[tbworkcode.Text].NoCredits * 10 / noSemesterWeeks) - myModules[tbworkcode.Text].Hours;
            }
            catch
            {
                error.Text = "Calculate the self study hours first";
            }
            remainingHours = selfStuHours - myWork[tbworkcode.Text].WorkHours;
            lbdisplay.Items.Add("Remaining Hours for week " + myWork[tbworkcode.Text].WorkDate + " is " + remainingHours);
            error.Text = "Consider displaying self study hours first";


        }

        private void btdisplayhours_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selfStuHours = (myModules[tbdisplay.Text].NoCredits * 10 / noSemesterWeeks) - myModules[tbdisplay.Text].Hours;
                lbdisplay.Items.Add("Number of self study hours: " + selfStuHours);
            }
            catch (Exception ex)
            {
                error.Text = "Enter number of weeks in the semester first";
            }
        }

        private void btsemester_Click(object sender, RoutedEventArgs e)
        {

            noSemesterWeeks = Convert.ToInt32(tbnoweeks.Text);
            semesterDay = dsemester.Text;
            btwork.IsEnabled = true;
            btdisplayhours.IsEnabled = true;
            btaddcode.IsEnabled = true;

        }

        private void btaddcode_Click(object sender, RoutedEventArgs e)
        {

            if (gr.IsChecked == true)
            {
                var total = from l in listNocredit where l > Convert.ToInt16(tbaddcode.Text) select l;
                foreach (var t in total)
                {
                    lbdisplay.Items.Add(t);
                }

            }
            if (lss.IsChecked == true)
            {

                var total = from l in listNocredit where l < Convert.ToInt16(tbaddcode.Text) select l;
                foreach (var t in total)
                {
                    lbdisplay.Items.Add(t);
                }

            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            lbdisplay.Items.Clear();
        }
        public void insertDataMod()
        {
            this.Dispatcher.Invoke(() =>
            {
                string command = string.Format("INSERT INTO MODULES(MODULE_CODE,USER_ID,MODULE_NAME,NUMBER_CREDITS,MODULE_HOURS) VALUES('{0}','{1}','{2}',{3},{4})", modCode, userID, tbmodname.Text, Convert.ToInt32(tbmodcredits.Text), Convert.ToInt32(tbmodhours.Text)); ;
                cmd = new SqlCommand(command, con);
            });

            con.Open();
            this.Dispatcher.Invoke(() => { cmd.ExecuteNonQuery(); });
            con.Close();
        }
        public void insertDataWork()
        {
            this.Dispatcher.Invoke(() =>
            {
                string command = string.Format("INSERT INTO WORK(WORK_CODE,USER_ID,WORK_DATE,WORK_HOURS) VALUES('{0}','{1}','{2}',{3})", tbworkcode.Text, userID, cworkdate.Text, Convert.ToInt32(tbworkhours.Text));
                cmd = new SqlCommand(command, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            });
        }

        private void workHoursCal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selfStuHours = (myModules[tbworkcode.Text].NoCredits * 10 / noSemesterWeeks) - myModules[tbworkcode.Text].Hours;
            }
            catch
            {
                error.Text = "You have no hours saved";
            }
            remainingHours = selfStuHours - myWork[tbworkcode.Text].WorkHours;
            lbdisplay.Items.Add("Remaining Hours for week " + myWork[tbworkcode.Text].WorkDate + " is " + remainingHours);
            //error.Text = "Consider displaying self study hours first";

        }
    }
}
