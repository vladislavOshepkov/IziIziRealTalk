using RPM_PR_23._106_Oshepkov_PR3_v2.Model;
using RPM_PR_23._106_Oshepkov_PR3_v2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RPM_PR_23._106_Oshepkov_PR3_v2.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Autho : Page
    {
        int click;
        int authFails;
        private DispatcherTimer timer;
        private int blockTime;
        public Autho()
        {
            InitializeComponent();
            click = 0;
            authFails = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null, null));
        }

        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;

            string capctchaText = Captcha.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = authLogin.Text.Trim();
            string password = Hash.HashPassword(authPassword.Password.Trim());

            Shoe_factoryEntities db = Helper.GetContext();

            var user = db.Users.Where(x => x.user_login == login && x.user_password == password).FirstOrDefault();
            if (click == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.Roles.role_name);
                    LoadPage(user.Roles.role_name.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha();
                    authPassword.Password = String.Empty;
                    authFails++;
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.Roles.role_name.ToString());
                    LoadPage(user.Roles.role_name.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                    GenerateCapctcha();
                    authFails++;
                }
            }
            if (authFails == 3)
            {
                InterfaceBlock();
            }
        }

        private void LoadPage(string _role, Users user)
        {
            click = 0;
            switch (_role)
            {
                case "Суперадмин":
                    NavigationService.Navigate(new Superadmin(user, _role));
                    break;
                case "Админ":
                    NavigationService.Navigate(new Admin(user, _role));
                    break;
                case "Работник":
                    NavigationService.Navigate(new Employee(user, _role));
                    break;
                case "Клиент":
                    NavigationService.Navigate(new Client(user, _role));
                    break;
            }
        }
        private void InterfaceBlock()
        {
            blockTime = 10;
            btnEnter.IsEnabled = false;
            btnEnterGuests.IsEnabled = false;
            authLogin.IsEnabled = false;
            authPassword.IsEnabled = false;
            tblAuthFailTime.Visibility = Visibility.Visible;
            timer.Start();
            UpdateTimerText(blockTime);
        }
        private void InterfaceUnblock()
        {
            btnEnter.IsEnabled = true;
            btnEnterGuests.IsEnabled = true;
            authLogin.IsEnabled = true;
            authPassword.IsEnabled = true;
            tblAuthFailTime.Visibility = Visibility.Hidden;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (blockTime > 0)
            {
                UpdateTimerText(--blockTime);
            }
            else
            {
                InterfaceUnblock();
                timer.Stop();
                authFails = 0;
            }
        }
        private void UpdateTimerText(int secondsRemaining)
        {
            tblAuthFailTime.Text = $"{secondsRemaining}";
            tblAuthFailTime.Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
