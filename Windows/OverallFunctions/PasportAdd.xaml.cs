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
    /// Логика взаимодействия для PasportAdd.xaml
    /// </summary>
    public partial class PasportAdd : Window
    {
        
        public PasportAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              if (string.IsNullOrWhiteSpace(tbSerial.Text) || 
                  string.IsNullOrWhiteSpace(tbNumber.Text) || 
                  string.IsNullOrWhiteSpace(tbCode.Text)||
                  string.IsNullOrWhiteSpace(rtBirthPalce.Text)||
                  string.IsNullOrWhiteSpace(rtGivedPalce.Text))
                {
                    MessageBox.Show($"Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (tbSerial.Text.Length > 4 && tbNumber.Text.Length > 6 && tbCode.Text.Length > 6
                  && rtBirthPalce.Text.Length > 150 && rtGivedPalce.Text.Length > 150)
                {
                    MessageBox.Show("Введенные данные превышают допустимую длину",
                               "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }
                Connect.Context.PasData.Add(new PasData
            {
                Serial = tbSerial.Text,
                Number = tbNumber.Text,
                Code = tbCode.Text,
                GivedDate = dpGiveDate.DisplayDate,
                BirthDate = dpBirthDate.DisplayDate,
                GivedPlace = rtGivedPalce.Text,
                BirthPlace = rtGivedPalce.Text
            });
                    Connect.Context.SaveChanges();
                    MessageBox.Show($"Паспортные данные успешно внесены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Сохранение id нового паспорта
                    var maxId = Connect.Context.PasData.Select(i => i.IdPas);
                    Connect.idNewPasport = maxId.Max();
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
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "№.".IndexOf(e.Text) < 0;
        }

        private void rtBirthPalce_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "№.".IndexOf(e.Text) < 0;
        }

        private void rtBirthPalce_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
