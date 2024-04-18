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

namespace RailwayTicketsSystem
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        RailwayTicketsEntities users = new RailwayTicketsEntities();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            if (LoginTextBox.Text != "Admin")
            {
                foreach (var u in users.RailwayUsers)
                {
                    string login = LoginTextBox.Text.Trim();
                    if ( login == u.Login && PasswordTextBox.Password == u.Password)
                    {
                        valid = true;
                    }
                }
                    if(valid)
                    {
                        AvailableTickets page = new AvailableTickets();
                        this.NavigationService.Navigate(page);
                }
                    else
                    {
                        MessageBox.Show("Неверные логин или пароль");
                        LoginTextBox.Focus();
                    }
            }
            else if (LoginTextBox.Text == "Admin" && PasswordTextBox.Password == "123")
            {
                Admin page = new Admin();
                this.NavigationService.Navigate(page);
            }
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage page = new RegistrationPage();
            this.NavigationService.Navigate(page);
        }
    }
}
