using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.RightsManagement;
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
using RPM_PR_23._106_Oshepkov_PR3_v2.Model;

namespace RPM_PR_23._106_Oshepkov_PR3_v2.Pages
{
    /// <summary>
    /// Логика взаимодействия для client.xaml
    /// </summary>
    public partial class Client : Page
    {
        TimeSpan timeNow = DateTime.Now.TimeOfDay;
        public Client(Users user, string role)
        {
            InitializeComponent();
            GreetingByTime(user);
        }
        private void GreetingByTime(Users user)
        {
            if (user != null) {
                tblHello.Visibility = Visibility.Visible;
                if (timeNow > new TimeSpan(10, 0, 0) && timeNow < new TimeSpan(19, 0, 0))
                {
                    if (timeNow >= new TimeSpan(10, 0, 0) && timeNow <= new TimeSpan(12, 0, 0)) tblHello.Text = "Доброе утро";
                    else if (timeNow >= new TimeSpan(12, 0, 1) && timeNow <= new TimeSpan(17, 0, 0)) tblHello.Text = "Добрый день";
                    else if (timeNow >= new TimeSpan(17, 0, 1) && timeNow <= new TimeSpan(19, 0, 0)) tblHello.Text = "Добрый вечер";
                    tblHello.Text += $" {user.employee_surname} {user.employee_name}";
                }
                else tblHello.Text = "доступ запрещен";
            }
        }
    }
}
