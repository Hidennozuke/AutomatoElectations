using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutomatoElectations.Connector;
using AutomatoElectations.Windows.OverallFunctions;

namespace AutomatoElectations.Windows.WatcherVisual
{
    public partial class WatherCandPage : Page
    {
        public WatherCandPage()
        {
            InitializeComponent();
            SortsInit();
            FullRefrash();
            InitWindow();
        }
        private void InitWindow()
        {
            if (Connect.idActualRole != 1)
            {
                AddCands.Visibility = Visibility.Collapsed;
                RedCands.Visibility = Visibility.Collapsed;
                DelCands.Visibility = Visibility.Collapsed;
            }

            else if (Connect.idActualRole != 2)
            {
                Vote.Visibility = Visibility.Collapsed;
                //TODO не показывать изберателю паспортные данные (снести стобец?)
                //SuperView.lvPasNum.Visibility = Visibility.Collapsed;
            }
        }

        public void FullRefrash()
        {
            Search.Text = null;
            SortsInit();
            SuperView.ItemsSource = Connect.Context.CandidatsCurrentDisplay.Where(i => i.IsDeleted != true).ToList();
        }

        public void SortsInit()
        {
            List<Gender> GenSort = Connect.Context.Gender.ToList();
            GenSort.Insert(0, new Gender() { GenName = "Выберите пол" });
            SortGender.ItemsSource = GenSort;
            SortGender.DisplayMemberPath = "GenName";
            SortGender.SelectedIndex = 0;
        }

        public void Filtr()
        {

            //поиск
            var Flist = Connect.Context.CandidatsCurrentDisplay.Where(i =>
            i.IsDeleted == false).ToList().Where(i => 
            i.SName.ToLower().Contains(Search.Text) || 
            i.FName.ToLower().Contains(Search.Text) || 
            i.PName.ToLower().Contains(Search.Text) ||
            i.PassportNum.ToString().Contains(Search.Text)).ToList();
            //сортировка пол

            if (SortGender.SelectedIndex == 0 )
            {
                SuperView.ItemsSource = Flist.Where(i => i.IsDeleted == false).ToList();
            }
            else
            {
                var genSort = SortGender.SelectedItem as Gender;
                if (genSort != null)
                { 
                    SuperView.ItemsSource = Flist.Where(i => i.IdGen == genSort.IdGen && i.IsDeleted == false).ToList();
                }
            }
        }

        private void AddCands_Click(object sender, RoutedEventArgs e)
        {
            AddCandWindow Acd = new AddCandWindow(this);
            Acd.Show();
        }

        private void RedCands_Click(object sender, RoutedEventArgs e)
        {
            if (SuperView.SelectedItem is CandidatsCurrentDisplay candidatsCurrentDisplay)
            {
                Connect.idCand = candidatsCurrentDisplay.CandId;
                Connect.idNewPasport = candidatsCurrentDisplay.PassportNum;
                RedCandsWindow Rcw = new RedCandsWindow(this);
                Rcw.Show();
            }
            else
            {
                MessageBox.Show("Выберите запись", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SuperView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DelCands_Click(object sender, RoutedEventArgs e)
        {
            if (SuperView.SelectedItem is CandidatsCurrentDisplay candidatsCurrentDisplay)
            {
                Connect.idCand = candidatsCurrentDisplay.CandId;
                var canddel = Connect.Context.Candidate.Where(i => i.CandId == Connect.idCand).FirstOrDefault();
                canddel.IsDeleted = true;
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
        private void SortGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            FullRefrash();
        }

        private void Vote_Click(object sender, RoutedEventArgs e)
        {
            var member = Connect.Context.Member.Where(i => i.ComID == Connect.idActualUser).FirstOrDefault();
            if (member.IsVoted == true)
            {
                MessageBox.Show("Вы уже голосовали, вам доступен только просмотр списка кандидатов", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (SuperView.SelectedItem is CandidatsCurrentDisplay candidatsCurrentDisplay)
            {
                Connect.idCand = candidatsCurrentDisplay.CandId;
                VoteWindow VFC = new VoteWindow();
                VFC.ShowDialog(); 
            }
            else
            {
                MessageBox.Show("Выберите запись", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
