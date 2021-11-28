using System;
using System.Collections.Generic;
using System.Linq;
using AutomatoElectations.Windows.WatcherVisual;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AutomatoElectations.Connector;
using System.IO;
using Microsoft.Win32;

namespace AutomatoElectations.Windows.OverallFunctions
{
    /// <summary>
    /// Логика взаимодействия для RedCandsWindow.xaml
    /// </summary>
    public partial class RedCandsWindow : Window
    {
        WatherCandPage f;
        private string Photo;
        private Image canPhoto = new Image();
        public RedCandsWindow(WatherCandPage c)
        {
            InitializeComponent();
            f = c;
            cbGen.ItemsSource = Connect.Context.Gender.Select(i => i.GenName).ToList();
            tbPart.ItemsSource = Connect.Context.PartysList.Select(i => i.NameParty).ToList();

            //хранение записи
            var ActualCandidate = Connect.Context.Candidate.Where(i => i.CandId == Connect.idCand).FirstOrDefault();
            cbGen.SelectedItem = Connect.Context.Gender.Where(i => i.IdGen == ActualCandidate.Gender).Select(i => i.GenName).FirstOrDefault();
            tbPart.SelectedItem = Connect.Context.PartysList.Where(i => i.IdPart == ActualCandidate.Party).Select(i => i.NameParty).FirstOrDefault();
            //Первоначальные данные
            tbSName.Text = ActualCandidate.SName;
            tbFName.Text = ActualCandidate.FName;
            tbPName.Text = ActualCandidate.PName;
        }
        public void EnterFullData()
        {
            if (Photo != null)
            {
                var NewCand = Connect.Context.Candidate.Where(i => i.CandId == Connect.idCand).FirstOrDefault();
                NewCand.SName = tbSName.Text;
                NewCand.FName = tbFName.Text;
                NewCand.PName = tbPName.Text;
                NewCand.PassportNum = Connect.idNewPasport;
                NewCand.PhotoPath = File.ReadAllBytes(Photo);
                NewCand.Gender = Connect.Context.Gender.Where(i => i.GenName == cbGen.SelectedItem.ToString()).Select(i => i.IdGen).FirstOrDefault();
                NewCand.Party = Connect.Context.PartysList.Where(i => i.NameParty == tbPart.SelectedItem.ToString()).Select(i => i.IdPart).FirstOrDefault();
            }
            else
            {
                var NewCand = Connect.Context.Candidate.Where(i => i.CandId == Connect.idCand).FirstOrDefault();
                NewCand.SName = tbSName.Text;
                NewCand.FName = tbFName.Text;
                NewCand.PName = tbPName.Text;
                NewCand.PassportNum = Connect.idNewPasport;
                NewCand.Gender = Connect.Context.Gender.Where(i => i.GenName == cbGen.SelectedItem.ToString()).Select(i => i.IdGen).FirstOrDefault();
                NewCand.Party = Connect.Context.PartysList.Where(i => i.NameParty == tbPart.SelectedItem.ToString()).Select(i => i.IdPart).FirstOrDefault();
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnterFullData();
                if (string.IsNullOrWhiteSpace(tbSName.Text) ||
                  string.IsNullOrWhiteSpace(tbFName.Text) ||
                  string.IsNullOrWhiteSpace(tbPName.Text) ||
                  cbGen == null || tbPart == null)
                {
                    MessageBox.Show($"Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }  
                    Connect.Context.SaveChanges();
                    MessageBox.Show($"Кандидат {tbSName.Text} {tbFName.Text} {tbPName.Text} успешно обновлен!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                    List<CandidatsCurrentDisplay> Cands = new List<CandidatsCurrentDisplay>();

                    f.FullRefrash();

                    this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show($"Проверьте правильность ввода данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                canPhoto.Source = new BitmapImage(new Uri(fileDialog.FileName));

                Photo = fileDialog.FileName;
            }

        }

        private void cbGen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbPasNum_Click(object sender, RoutedEventArgs e)
        {
            PasportEdit pE = new PasportEdit();
            pE.ShowDialog();
        }

        private void tbSName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void tbFName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }
        private void tbPName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }
    }
}
