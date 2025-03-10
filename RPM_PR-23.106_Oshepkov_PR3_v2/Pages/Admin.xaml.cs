using RPM_PR_23._106_Oshepkov_PR3_v2;
using RPM_PR_23._106_Oshepkov_PR3_v2.Model;
using RPM_PR_23._106_Oshepkov_PR3_v2.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPM_PR_23._106_Oshepkov_PR3_v2.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        Shoe_factoryEntities db = Helper.GetContext();
        private string connectionString = "data source=DESKTOP-GH4HECC\\SQLEXPRESS01;initial catalog=Polyclinic;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        public Admin(Users user, string role)
        {
            InitializeComponent();

            var postNameList = db.Posts.Select(p => p.post_name).ToList();
            postNameList.Insert(0, "Все");
            postNameList.Sort();
            cmbSorting.ItemsSource = postNameList;

            cmbFilter.Items.Add("По возрастанию");
            cmbFilter.Items.Add("По убыванию");

            var employeeList = db.Employees.ToList();
            EmployeeList.ItemsSource = employeeList;
        }

        private void EmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void EmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeList.SelectedItem is Employees selectedEmployee)
            {
                NavigationService.Navigate(new EmployeesEdit(selectedEmployee));
            }
        }

        private void txtSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var searchText = txtSearch.Text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var filteredEmployees = db.Employees.ToList().Where(s => searchText.All(w => s.employee_surname.ToLower().Contains(w) || s.employee_name.ToLower().Contains(w) || s.employee_patronym.ToLower().Contains(w)));

            EmployeeList.ItemsSource = filteredEmployees;
        }

        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var searchText = txtSearch.Text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (cmbSorting.SelectedItem)
            {
                case "Все":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(s => searchText.All(w => s.employee_surname.ToLower().Contains(w) || s.employee_name.ToLower().Contains(w) || s.employee_patronym.ToLower().Contains(w)));
                    break;
                case "Затяжчик обуви":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(emp => emp.Posts.post_name == "Затяжчик обуви" && searchText.All(w => emp.employee_surname.ToLower().Contains(w) || emp.employee_name.ToLower().Contains(w) || emp.employee_patronym.ToLower().Contains(w)));
                    break;
                case "Модельер":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(emp => emp.Posts.post_name == "Модельер" && searchText.All(w => emp.employee_surname.ToLower().Contains(w) || emp.employee_name.ToLower().Contains(w) || emp.employee_patronym.ToLower().Contains(w)));
                    break;
                case "Обувщик":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(emp => emp.Posts.post_name == "Обувщик" && searchText.All(w => emp.employee_surname.ToLower().Contains(w) || emp.employee_name.ToLower().Contains(w) || emp.employee_patronym.ToLower().Contains(w)));
                    break;
                case "Раскройщик материалов":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(emp => emp.Posts.post_name == "Раскройщик материалов" && searchText.All(w => emp.employee_surname.ToLower().Contains(w) || emp.employee_name.ToLower().Contains(w) || emp.employee_patronym.ToLower().Contains(w)));
                    break;
                case "Сборщик обуви":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(emp => emp.Posts.post_name == "Сборщик обуви" && searchText.All(w => emp.employee_surname.ToLower().Contains(w) || emp.employee_name.ToLower().Contains(w) || emp.employee_patronym.ToLower().Contains(w)));
                    break;
                case "Технолог":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(emp => emp.Posts.post_name == "Технолог" && searchText.All(w => emp.employee_surname.ToLower().Contains(w) || emp.employee_name.ToLower().Contains(w) || emp.employee_patronym.ToLower().Contains(w)));
                    break;
            }
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var searchText = txtSearch.Text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (cmbFilter.SelectedItem)
            {
                case "По возрастанию":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(s => searchText.All(w => s.employee_surname.ToLower().Contains(w) || s.employee_name.ToLower().Contains(w) || s.employee_patronym.ToLower().Contains(w))).ToList().OrderBy(emp => emp.employee_surname);
                    break;
                case "По убыванию":
                    EmployeeList.ItemsSource = db.Employees.ToList().Where(s => searchText.All(w => s.employee_surname.ToLower().Contains(w) || s.employee_name.ToLower().Contains(w) || s.employee_patronym.ToLower().Contains(w))).ToList().OrderByDescending(emp => emp.employee_surname);
                    break;
            }
        }
    }
}
