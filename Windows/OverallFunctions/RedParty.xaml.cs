using AutomatoElectations.Connector;
using AutomatoElectations.Windows.WatcherVisual;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AutomatoElectations.Windows.OverallFunctions
{
    /// <summary>
    /// Логика взаимодействия для RedParty.xaml
    /// </summary>
    public partial class RedParty : Window
    {
        PartyPage f;
        private string Photo;
        private Image canPhoto = new Image();
        public RedParty(PartyPage c)
        {
            InitializeComponent();
            LoadData();
            f = c;
        }

        public void LoadData()
        {
            var actualPart = Connect.Context.PartysList.Where(i => i.IdPart == Connect.idParty).FirstOrDefault();
            tbPartName.Text = actualPart.NameParty;
        }

        public void EnteryFullData()
        {
            if(Photo != null)
            {
                var NewPart = Connect.Context.PartysList.Where(i => i.IdPart == Connect.idParty).FirstOrDefault();
                NewPart.NameParty = tbPartName.Text;

                NewPart.PhotoPath = File.ReadAllBytes(Photo);
            }
            else
            {
                var NewPart = Connect.Context.PartysList.Where(i => i.IdPart == Connect.idParty).FirstOrDefault();
                NewPart.NameParty = tbPartName.Text;
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbPartName.Text))
                {
                    MessageBox.Show($"Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                EnteryFullData();


                    Connect.Context.SaveChanges();
                    MessageBox.Show($"Партия {tbPartName.Text} успешно обновлена!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                    List<PartysList> partysLists = new List<PartysList>();

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

        private void tbPartName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
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
    }
}
