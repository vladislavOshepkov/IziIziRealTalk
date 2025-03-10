using RPM_PR_23._106_Oshepkov_PR3_v2.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPM_PR_23._106_Oshepkov_PR3_v2.Model;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Data;

namespace RPM_PR_23._106_Oshepkov_PR3_v2.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesEdit.xaml
    /// </summary>
    public partial class EmployeesEdit : Page
    {
        private Employees _employees;
        public EmployeesEdit(Employees employees)
        {
            InitializeComponent();
            _employees = employees;

            Shoe_factoryEntities db = Helper.GetContext();
            var postNameList = db.Posts.Select(p => p.post_name).ToList();
            cbPosts.ItemsSource = postNameList;
            var genderList = db.Gender.Select(p => p.gender_name).ToList();
            cbGender.ItemsSource = genderList;

            tbSurname.Text = employees.employee_surname.ToString();
            tbName.Text = employees.employee_name.ToString();
            tbPatronymic.Text = employees.employee_patronym.ToString();
            cbGender.SelectedItem = employees.Gender.gender_name.ToString();
            tbPhoneNumber.Text = employees.employee_phone_number.ToString();
            cbPosts.SelectedItem = employees.Posts.post_name.ToString();

        }

        private void SaveEmployeeChanges(object sender, RoutedEventArgs e)
        {
            lmao(_employees);
        }
        private void lmao(Employees employees)
        {
            Shoe_factoryEntities db = Helper.GetContext();
            if (employees != null) 
            { 
                employees.employee_surname = tbSurname.Text;
                employees.employee_name = tbName.Text;
                employees.employee_patronym = tbPatronymic.Text;
                int? gender_nameToId = db.Gender.FirstOrDefault(g => g.gender_name == cbGender.SelectedItem.ToString())?.gender_id;
                employees.employee_phone_number = tbPhoneNumber.Text;
                int? post_nameToId = db.Posts.FirstOrDefault(g => g.post_name == cbPosts.SelectedItem.ToString())?.post_id;

                employees.gender_id = gender_nameToId.Value;
                employees.post_id = post_nameToId.Value;
                db.SaveChanges();
                try
                {
                    if (gender_nameToId != null && post_nameToId != null)
                    {
                        employees.gender_id = gender_nameToId.Value;
                        employees.post_id = post_nameToId.Value;
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show("ERROR");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Admin());
        }
    }
}
