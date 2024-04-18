using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для PurchasedTicketsPage.xaml
    /// </summary>
    public partial class PurchasedTicketsPage : Page
    {
        RailwayTicketsEntities tickets = new RailwayTicketsEntities();
        public static CollectionViewSource source;
        public PurchasedTicketsPage()
        {
            InitializeComponent();
            source = (CollectionViewSource)FindResource("purchasedTicketsViewSource");
            DataContext = this;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tickets.PurchasedTickets.Load();
            source.Source = tickets.PurchasedTickets.Local;
        }

        private void TicketsView_Click(object sender, RoutedEventArgs e)
        {
            AvailableTickets page = new AvailableTickets();
            this.NavigationService.Navigate(page);
        }
    }
}
