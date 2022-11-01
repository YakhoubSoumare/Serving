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
using System.IO;
using Microsoft.Win32;

namespace Serving
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string[] timeCombo;
        Reservation bokning;
        AsyncCooking recept;
        public MainWindow()
        {
            InitializeComponent();

            timeComboInit();

            bokning = new Reservation();

            FirstList();

            recept = new AsyncCooking();

            //För Async Recept
            receptListBox.ItemsSource = recept.ReceptView();
        }

        private void bokaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((datePicker.SelectedDate == null) || (nameTextBox.Text == null) ||
                (tidComboBox.SelectedItem == null) || (tableComboBox.SelectedItem == null))
                {
                    MessageBox.Show("Fyll alla parametrar!");
                }
                else
                {
                    string personName = nameTextBox.Text;

                    string chosenDate = ((DateTime)datePicker.SelectedDate).ToShortDateString();

                    string chosenTime = tidComboBox.SelectedItem.ToString();

                    Int32.TryParse(tableComboBox.Text, out int tableNumber);

                    foreach (var reservation in bokning.ViewReservations())
                    {
                        if ((reservation.Table == tableNumber) && (reservation.Date == chosenDate) && (reservation.Time == chosenTime))
                        {
                            MessageBox.Show("Bord ej bokningsbar vid given dag och tid!");
                        }
                    }

                    //För att se om det redan finns fem stycken bokade
                    int maxAntalBokningar = bokning.ViewReservations().Where(customer=>customer.Date==chosenDate & customer.Time==chosenTime).Count();

                    if (maxAntalBokningar == 5)
                    {
                        MessageBox.Show("Det är fullbokat vid angiven tid!");
                    }
                    else
                    {
                        bokning.Reserve(new Customer(personName, chosenDate, tableNumber, chosenTime));
                    }
                }

                UpdateListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            datePicker.Text = String.Empty;
            nameTextBox.Clear();
            tidComboBox.Text = String.Empty;
            tableComboBox.Text = String.Empty;
        }

        private void visaBokningarButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateListBox();
        }

        private void avbokaButton_Click(object sender, RoutedEventArgs e)
        {
            Customer temp = null;
            try
            {
                foreach (Customer reservation in bokning.ViewReservations())
                {
                    if ((reservation.Date + " " + reservation.Time + " " + reservation.Name + " " + reservation.Table) == (mainListBox.SelectedItem.ToString()))
                    {
                        temp = reservation;
                    }
                }

                bokning.CancelReservation(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            UpdateListBox();
        }

        private void timeComboInit()
        {
            timeCombo = new string[24];
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    timeCombo[i] = $"0{i}:00";
                }
                else
                {
                    timeCombo[i] = $"{i}:00";
                }

            }
            tidComboBox.ItemsSource = timeCombo;
        }

        private void FirstList()
        {
            List<Customer> theCustomers = new List<Customer> { new Customer("John Doe", "2022-10-29", 4, "22:00"),
                new Customer("Jane Doe", "2022-10-30", 3, "23:00")};

            foreach (Customer customer in theCustomers)
            {
                bokning.Reserve(customer);
            }
            UpdateListBox();
        }

        private void UpdateListBox()
        {

            mainListBox.ItemsSource = bokning.ViewReservations().Select(person => person.Date + " " + person.Time + " " + person.Name + " " + person.Table);
        }

        private void sparaFilButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string listInListBox = string.Empty;
            if (saveFileDialog.ShowDialog() == true)
            {
                foreach(var row in mainListBox.Items)
                {
                    listInListBox += row.ToString()+'\n';
                }
                File.WriteAllText(saveFileDialog.FileName, listInListBox.Trim());
            }               
        }

        private void laddaFilButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string reservationsLista = string.Empty;
            if (openFileDialog.ShowDialog() == true)
            {
                reservationsLista = File.ReadAllText(openFileDialog.FileName);
            }
            string[] newList = reservationsLista.Split('\n');
            mainListBox.ItemsSource = newList;
        }

        private async void cookingButton_Click(object sender, RoutedEventArgs e)
        {
            recept.MatLagning();
        }
    }
}
