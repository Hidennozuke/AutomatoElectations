using AutomatoElectations.Connector;
using AutomatoElectations.Windows.WatcherVisual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace AutomatoElectations.Windows.OverallFunctions
{
    public partial class RedMemWindow : Window
    {
        WatcherPage f;
        public RedMemWindow(WatcherPage c)
        {
            InitializeComponent();
            LoadData();
            f = c;   
        }

        public void LoadData()
        {
            cbGen.ItemsSource = Connect.Context.Gender.Select(i => i.GenName).ToList();
            //хранение записи
            var ActualMember = Connect.Context.Member.Where(i => i.ComID == Connect.idMember).FirstOrDefault();
            //Первоначальные данные
            cbGen.SelectedItem = Connect.Context.Gender.Where(i => i.IdGen == ActualMember.Gen).Select(i => i.GenName).FirstOrDefault();
            tbSName.Text = ActualMember.SName;
            tbFName.Text = ActualMember.FName;
            tbPName.Text = ActualMember.PName;
            tbLog.Text = ActualMember.Login;
            tbPas.Text = ActualMember.Password;
            tbPas2.Text = ActualMember.Password;
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
                         var ActualMember = Connect.Context.Member.Where(i => i.ComID == Connect.idMember).FirstOrDefault();
                         ActualMember.SName = tbSName.Text;
                         ActualMember.FName = tbFName.Text;
                         ActualMember.PName = tbPName.Text;
                         ActualMember.Password = tbPas.Text;
                         ActualMember.Login = tbLog.Text;
                         ActualMember.PassportNum = Connect.idNewPasport;
                         ActualMember.Gen = Connect.Context.Gender.Where(i => i.GenName == cbGen.SelectedItem.ToString()).Select(i => i.IdGen).FirstOrDefault();


                            Connect.Context.SaveChanges();
                        MessageBox.Show($"Избиратель {tbSName.Text} {tbFName.Text} {tbPName.Text} успешно обновлен!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                        List<VotersCurrentDisplay> MemberList = new List<VotersCurrentDisplay>();

                        f.FullRefrash();

                        this.Close();

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
