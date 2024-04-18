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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        RailwayTicketsEntities tickets = new RailwayTicketsEntities();
        public static CollectionViewSource source;
        public Admin()
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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Tickets deleteTickets = source.View.CurrentItem as Tickets;
            MessageBox.Show("Вы уверены, что хотите удалить этот маршрут?");
            foreach (Tickets t in tickets.Tickets)
            {
                tickets.Tickets.Remove(deleteTickets);
            }
                tickets.SaveChanges();
                source.View.Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            grid1.Visibility = Visibility.Visible;
        }

        private void SaveNewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (grid1.Visibility == Visibility.Visible)
            {
                Tickets newTicket = new Tickets();
                newTicket.TripStart = tripStartTextBox.Text;
                newTicket.TripFinish = tripFinishTextBox.Text;
                newTicket.StartTime = Convert.ToDateTime(startTimeDatePicker.Text);
                newTicket.FinishTime = Convert.ToDateTime(finishTimeDatePicker.Text);
                newTicket.CompartmantTicketsCount = Convert.ToInt32(compartmantTicketsCountTextBox.Text);
                newTicket.CompartmentPrice = Convert.ToDecimal(compartmantTicketsCountTextBox.Text);
                newTicket.ReservedSeatTicketsCount = Convert.ToInt32(reservedSeatTicketsCountTextBox.Text);
                newTicket.ReservedSeatPrice = Convert.ToDecimal(reservedSeatPriceTextBox.Text);
                newTicket.TicketKod = ticketKodTextBox.Text;
                tickets.Tickets.Add(newTicket);
                tickets.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                source.View.Refresh();
                grid1.Visibility = Visibility.Hidden;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ticketsDataGrid.CancelEdit();
            tickets.SaveChanges();
            source.View.Refresh();
        }
    }
}
