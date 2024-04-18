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
    public partial class RegistrationPage : Page
    {
        RailwayTicketsEntities usersRegist = new RailwayTicketsEntities();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            bool existLogin = false;
            var users = usersRegist.RailwayUsers;
            if(LoginTextBox.Text != "Admin" && PasswordTextBox.Password == ChekPasswordTextBox.Password)
            {
                foreach (RailwayUsers u in users)
                {
                    if (LoginTextBox.Text == u.Login.ToString())
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                        LoginTextBox.Focus();
                        existLogin = true;
                    }
                }
                if(existLogin == false)
                {
                    RailwayUsers newUser = new RailwayUsers();
                    newUser.Login = LoginTextBox.Text.Trim();
                    newUser.Password = PasswordTextBox.Password;
                    usersRegist.RailwayUsers.Add(newUser);
                    Succsess();
                }  
            }
        }

        private void Succsess()
        {
            MessageBox.Show("Успешная регистрация");
            usersRegist.SaveChanges();
            LoginPage page = new LoginPage();
            NavigationService.Navigate(page);
        }
    }
}
