using AutomatoElectations.Connector;
using AutomatoElectations.Windows.WatcherVisial;
using AutomatoElectations.Windows.WatcherVisual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace AutomatoElectations.Windows.OverallFunctions
{
    

    public partial class AddMemWindow : Window
    {
        WatcherPage f;
        public AddMemWindow(WatcherPage c)
        {
            InitializeComponent();
            cbGen.ItemsSource = Connect.Context.Gender.Select(i => i.GenName).ToList();
            f = c;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbSName.Text) ||
                  string.IsNullOrWhiteSpace(tbFName.Text) ||
                  string.IsNullOrWhiteSpace(tbPName.Text) ||
                  string.IsNullOrWhiteSpace(tbLog.Text) ||
                  string.IsNullOrWhiteSpace(tbPas.Text) ||
                  string.IsNullOrWhiteSpace(tbPas2.Text) ||
                  cbGen == null)
                {
                    MessageBox.Show($"Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                    if (tbPas.Text == tbPas2.Text)
                    {
                        Connect.Context.Member.Add(new Member
                        {
                            FName = tbFName.Text,
                            SName = tbSName.Text,
                            PName = tbPName.Text,
                            PassportNum = Connect.idNewPasport,
                            IdRole = 2,
                            Password = tbPas.Text,
                            Login = tbLog.Text,
                            IsDeleted = false,
                            Gen = Connect.Context.Gender.Where(i => i.GenName == cbGen.SelectedItem.ToString()).Select(i => i.IdGen).FirstOrDefault()
                        });
                    if (Connect.idNewPasport >= 0)
                    {
                        Connect.Context.SaveChanges();
                        MessageBox.Show($"Избиратель {tbSName.Text} {tbFName.Text} {tbPName.Text} успешно добавлен!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                        List<VotersCurrentDisplay> MemberList = new List<VotersCurrentDisplay>();

                        f.FullRefrash();
                        Connect.idNewPasport = 0;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Внесите паспортные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    }
                    else
                    {
                        MessageBox.Show($"Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
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

        private void tbPasNum_Click(object sender, RoutedEventArgs e)
        {
            PasportAdd aW = new PasportAdd();
            aW.ShowDialog();
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

        private void tbLog_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //Можно вводить буквы, цифры, спец.символы
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
        }

        private void tbPas_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //Можно вводить буквы, цифры, спец.символы
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
        }

        private void tbPas2_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //Можно вводить буквы, цифры, спец.символы
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
        }

    }
}
