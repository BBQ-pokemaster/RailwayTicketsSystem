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
    /// Логика взаимодействия для AvailableTickets.xaml
    /// </summary>
    public partial class AvailableTickets : Page
    {
        RailwayTicketsEntities tickets = new RailwayTicketsEntities();
        public static CollectionViewSource source;
        public AvailableTickets()
        {
            InitializeComponent();
            source = (CollectionViewSource)FindResource("ticketsViewSource");
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tickets.Tickets.Load();
            source.Source = tickets.Tickets.Local;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Resources["CurrentTicket"] = source.View.CurrentItem as Tickets;
            Buy buy = new Buy();
            buy.Show();
        }
        private void PurchasedTicketsView_Click(object sender, RoutedEventArgs e)
        {
            PurchasedTicketsPage page = new PurchasedTicketsPage();
            this.NavigationService.Navigate(page);
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text != "")
            {
                var tick = tickets.Tickets.Where(x => x.TripStart == Search.Text || x.TripFinish == Search.Text);
                ticketsDataGrid.ItemsSource = tick.ToList();
                source.View.Refresh();
            }
            else
            {
                ticketsDataGrid.ItemsSource = tickets.Tickets.ToList();
                source.View.Refresh();
            }
        }
    }
}
