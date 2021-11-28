using AutomatoElectations.Connector;
using AutomatoElectations.Windows.OverallFunctions;
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

namespace AutomatoElectations.Windows.WatcherVisual
{
    /// <summary>
    /// Логика взаимодействия для PartyPage.xaml
    /// </summary>
    public partial class PartyPage : Page
    {
        public PartyPage()
        {
            InitializeComponent();
            InitWindow();
            FullRefrash();
        }

        private void InitWindow()
        {
            if (Connect.idActualRole != 1)
            {
                AddParts.Visibility = Visibility.Collapsed;
                RedParts.Visibility = Visibility.Collapsed;
                DelParts.Visibility = Visibility.Collapsed;
            }
        }

        public void Filtr()
        {
            //поиск
            var Flist = Connect.Context.PartysList.Where(i =>
            i.IsDeleted == false).ToList().Where(i =>
            i.NameParty.ToLower().Contains(Search.Text)).ToList();
            
        }
        public void FullRefrash()
        {
            Search.Text = null;
            SuperView.ItemsSource = Connect.Context.PartysList.Where(i => i.IsDeleted != true).ToList();
        }
        private void AddParts_Click(object sender, RoutedEventArgs e)
        {
            AddPartyWindow aD = new AddPartyWindow(this);
            aD.ShowDialog();
        }


        private void DelParts_Click(object sender, RoutedEventArgs e)
        {
            if (SuperView.SelectedItem is PartysList partys)
            {
                Connect.idParty = partys.IdPart;
                var partysdel = Connect.Context.PartysList.Where(i => i.IdPart == Connect.idParty).FirstOrDefault();
                partysdel.IsDeleted = true;
                Connect.Context.SaveChanges();
                FullRefrash();
            }
            else
            {
                MessageBox.Show("Выберите запись", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            FullRefrash();
        }

        private void RedParts_Click(object sender, RoutedEventArgs e)
        {
            if (SuperView.SelectedItem is PartysList partys)
            {
                Connect.idParty = partys.IdPart;
                RedParty rP = new RedParty(this);
                rP.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите запись", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SuperView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
