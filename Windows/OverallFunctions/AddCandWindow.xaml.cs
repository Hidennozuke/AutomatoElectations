using AutomatoElectations.Connector;
using AutomatoElectations.Windows.WatcherVisual;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace AutomatoElectations.Windows.OverallFunctions
{
    public partial class AddCandWindow : Window
    {
        WatherCandPage f;
        private string Photo;
        private Image canPhoto = new Image();
        public AddCandWindow(WatherCandPage c)
        {
            InitializeComponent();
            cbGen.ItemsSource = Connect.Context.Gender.Select(i => i.GenName).ToList();
            cbPart.ItemsSource = Connect.Context.PartysList.Select(i => i.NameParty).ToList();
            f = c;
        }

        public void EnterFullData()
        {
            if (Photo != null)
            {
                Connect.Context.Candidate.Add(new Candidate
                {
                    FName = tbFName.Text,
                    SName = tbSName.Text,
                    PName = tbPName.Text,
                    PassportNum = Connect.idNewPasport,
                    IsDeleted = false,
                    Gender = Connect.Context.Gender.Where(i => i.GenName == cbGen.SelectedItem.ToString()).Select(i => i.IdGen).FirstOrDefault(),
                    Party = Connect.Context.PartysList.Where(i => i.NameParty == cbPart.SelectedItem.ToString()).Select(i => i.IdPart).FirstOrDefault(),
                    PhotoPath = File.ReadAllBytes(Photo)
                }); ;
            }
            else
            {
                Connect.Context.Candidate.Add(new Candidate
                {
                    FName = tbFName.Text,
                    SName = tbSName.Text,
                    PName = tbPName.Text,
                    PassportNum = Connect.idNewPasport,
                    IsDeleted = false,
                    Gender = Connect.Context.Gender.Where(i => i.GenName == cbGen.SelectedItem.ToString()).Select(i => i.IdGen).FirstOrDefault(),
                    Party = Connect.Context.PartysList.Where(i => i.NameParty == cbPart.SelectedItem.ToString()).Select(i => i.IdPart).FirstOrDefault(),

                }); ;
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
                  cbGen == null || cbPart == null)
                {
                    MessageBox.Show($"Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                EnterFullData();

                Connect.Context.SaveChanges();
                MessageBox.Show($"Кандидат {tbSName.Text} {tbFName.Text} {tbPName.Text} успешно добавлен!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                List<VotersCurrentDisplay> CandsList = new List<VotersCurrentDisplay>();

                f.FullRefrash();
                Connect.idNewPasport = 0;
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
        private void tbPasNum_Click(object sender, RoutedEventArgs e)
        {
            PasportAdd aW = new PasportAdd();
            aW.Show();
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
