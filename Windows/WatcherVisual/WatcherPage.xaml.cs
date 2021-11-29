using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutomatoElectations.Connector;
using AutomatoElectations.Windows.OverallFunctions;

namespace AutomatoElectations.Windows.WatcherVisual
{
    public partial class WatcherPage : Page
    {
        public WatcherPage()
        {
            InitializeComponent();
            SortsInit();
            FullRefrash();
        }

        public void FullRefrash()
        {
            SortsInit();
            Filtr();
            Search.Text = null;
            SuperView.ItemsSource = Connect.Context.VotersCurrentDisplay.Where(i => i.IdRole == 2 && i.IsDeleted != true).ToList();
        }
        public void SortsInit()
        {
            List<Gender> Gens = Connect.Context.Gender.ToList();
            Gens.Insert(0, new Gender() { GenName = "Выберите пол" });
            SortGender.ItemsSource = Gens;
            SortGender.DisplayMemberPath = "GenName";
            SortGender.SelectedIndex = 0;
        }
        public void Filtr()
        {
            //Поиск   
            var list = Connect.Context.VotersCurrentDisplay.Where(i=>i.IsDeleted != true && i.IdRole == 2 ).ToList().Where(i => 
            i.SName.ToLower().Contains(Search.Text.ToLower()) || 
            i.FName.ToLower().Contains(Search.Text.ToLower()) || 
            i.PName.ToLower().Contains(Search.Text.ToLower()) || 
            i.PassportNum.ToString().Contains(Search.Text)).ToList();

            //Сотиравка (пол)
            if (SortGender.SelectedIndex == 0)
            {
                SuperView.ItemsSource = list;
            }
            else
            {
                var gens = SortGender.SelectedItem as Gender;

                if (gens != null)
                {
                    list = list.Where(i => i.IdGen == gens.IdGen).ToList();
                    SuperView.ItemsSource = list;
                }
            }
        } 
        
        private void SuperView_SelectionChanged(object sender, SelectionChangedEventArgs e){}
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
        //добавление
        private void ShowVoters_Click(object sender, RoutedEventArgs e)
        {
            AddMemWindow aW = new AddMemWindow(this);
            aW.Show();
        }
        //редактирование
        private void RedVoters_Click(object sender, RoutedEventArgs e)
        {
            if (SuperView.SelectedItem is VotersCurrentDisplay votersCurrentDisplay)
            {
                Connect.idMember = votersCurrentDisplay.ComID;
                Connect.idNewPasport = votersCurrentDisplay.PassportNum;
                RedMemWindow rW = new RedMemWindow(this);
                rW.Show();
            }
            else
            {
                MessageBox.Show("Выберите запись", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
             
        }
        //жесткое удаление
        private void DelVoters_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы уверены что хотите удалить запись?", "Голосование", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (SuperView.SelectedItem is VotersCurrentDisplay votersCurrentDisplay)
                {
                    Connect.idMember = votersCurrentDisplay.ComID;
                    var membel = Connect.Context.Member.Where(i => i.ComID == Connect.idMember).FirstOrDefault();
                    membel.IsDeleted = true;
                    Connect.Context.SaveChanges();
                    FullRefrash();
                }
                else
                {
                    MessageBox.Show("Выберите запись", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Vote_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
