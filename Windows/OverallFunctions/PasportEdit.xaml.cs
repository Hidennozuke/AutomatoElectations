using AutomatoElectations.Connector;
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
using System.Windows.Shapes;

namespace AutomatoElectations.Windows.OverallFunctions
{
    /// <summary>
    /// Логика взаимодействия для PasportEdit.xaml
    /// </summary>
    public partial class PasportEdit : Window
    {
        public PasportEdit()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            //хранение записи
            var ActualPasport = Connect.Context.PasData.Where(i => i.IdPas == Connect.idNewPasport).FirstOrDefault();
            //Первоначальные данные
            tbSerial.Text = ActualPasport.Serial;
            tbNumber.Text = ActualPasport.Number;
            tbCode.Text = ActualPasport.Code;
            rtBirthPalce.Text = ActualPasport.BirthPlace;
            rtGivedPalce.Text = ActualPasport.GivedPlace;
            dpBirthDate.SelectedDate = ActualPasport.BirthDate;
            dpGiveDate.SelectedDate = ActualPasport.GivedDate;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbSerial.Text) ||
                  string.IsNullOrWhiteSpace(tbNumber.Text) ||
                  string.IsNullOrWhiteSpace(tbCode.Text) ||
                  string.IsNullOrWhiteSpace(rtBirthPalce.Text) ||
                  string.IsNullOrWhiteSpace(rtGivedPalce.Text))
                {
                    MessageBox.Show($"Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                

                var ActualMember = Connect.Context.PasData.Where(i => i.IdPas == Connect.idNewPasport).FirstOrDefault();
                ActualMember.Serial = tbSerial.Text;
                ActualMember.Number = tbNumber.Text;
                ActualMember.Code = tbCode.Text;
                ActualMember.GivedDate = dpGiveDate.DisplayDate;
                ActualMember.BirthDate = dpBirthDate.DisplayDate;
                ActualMember.GivedPlace = rtGivedPalce.Text;
                ActualMember.BirthPlace = rtGivedPalce.Text;

                Connect.Context.SaveChanges();
                MessageBox.Show($"Избиратель  успешно обновлен!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show($"Проверьте правильность ввода данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbSerial_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));

        }

        private void tbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));

        }

        private void tbCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));

        }

        private void rtGivedPalce_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
          
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
        }

        private void rtBirthPalce_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
        }
    }
}
