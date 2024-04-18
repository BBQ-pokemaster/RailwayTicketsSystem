using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Shapes;

namespace RailwayTicketsSystem
{
    /// <summary>
    /// Логика взаимодействия для Buy.xaml
    /// </summary>
    public partial class Buy : Window
    {
        RailwayTicketsEntities ticket = new RailwayTicketsEntities();
        PurchasedTickets pur = new PurchasedTickets();
        Tickets cur = Application.Current.Resources["CurrentTicket"] as Tickets;
        public Buy()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (Category.SelectedIndex == 0)
            {
                pur.Price = cur.CompartmentPrice * Convert.ToInt32(countTextBox.Text);
                priceTextBox.Text = pur.Price.ToString();
            }
            if (Category.SelectedIndex == 1)
            {
                pur.Price = cur.ReservedSeatPrice * Convert.ToInt32(countTextBox.Text);
                priceTextBox.Text = pur.Price.ToString();
            }
        }

        private void countTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Category.SelectedIndex == 0 && cur.CompartmantTicketsCount < Convert.ToInt32(countTextBox.Text))
            {
                pur.Price = cur.CompartmentPrice * Convert.ToInt32(countTextBox.Text);
                priceTextBox.Text = pur.Price.ToString();
            }
            else
            {
                MessageBox.Show("В наличии меньше билетов, чем вы указали");
            }
            if (Category.SelectedIndex == 1 && cur.ReservedSeatTicketsCount < Convert.ToInt32(countTextBox.Text))
            {
                pur.Price = cur.ReservedSeatPrice * Convert.ToInt32(countTextBox.Text);
                priceTextBox.Text = pur.Price.ToString();
            }
            else
            {
                MessageBox.Show("В наличии меньше билетов, чем вы указали");
            }
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Tickets selectedTicket = App.Current.Resources["CurrentTicket"] as Tickets;
            PurchasedTickets purTicket = new PurchasedTickets();
            if (purTicket.ID != 0)
            {
                int maxIdTovara = (from us in ticket.PurchasedTickets select us.ID).Max();
                purTicket.ID = maxIdTovara + 1;
            }
            else purTicket.ID = 1;
            purTicket.TicketKod = selectedTicket.TicketKod;
            purTicket.Trip = selectedTicket.TripStart + " " + selectedTicket.TripFinish;
            purTicket.StartTime = selectedTicket.StartTime;
            purTicket.FinishTime = selectedTicket.FinishTime;
            if (Category.SelectedIndex == 0 && selectedTicket.CompartmantTicketsCount != 0)
            {
                purTicket.VanType = "Купе";
            }
            else if(selectedTicket.CompartmantTicketsCount == 0)
            {
                MessageBox.Show("Билетов в купе не осталось");
            }
            if (Category.SelectedIndex == 1 && selectedTicket.ReservedSeatTicketsCount != 0)
            {
                purTicket.VanType = "Плацкарт";
            }
            else if (selectedTicket.ReservedSeatTicketsCount == 0)
            {
                MessageBox.Show("Билетов в плацкарт не осталось");
            }
            purTicket.Name = nameTextBox.Text;
            purTicket.Pasport = pasportTextBox.Text;
            purTicket.Price = Convert.ToDecimal(priceTextBox.Text);
            ticket.PurchasedTickets.Add(purTicket);
            if (Category.SelectedIndex == 0)
            {
                selectedTicket.CompartmantTicketsCount -= Convert.ToInt32(countTextBox.Text);
                AvailableTickets.source.View.Refresh();
                ticket.SaveChanges();
            }
            if (Category.SelectedIndex == 1)
            {
                selectedTicket.ReservedSeatTicketsCount -= Convert.ToInt32(countTextBox.Text);
                AvailableTickets.source.View.Refresh();
                ticket.SaveChanges();
            }
            
            ticket.SaveChanges();
            this.Close();
        }
    }
}
