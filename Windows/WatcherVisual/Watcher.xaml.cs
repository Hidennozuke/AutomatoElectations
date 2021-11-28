using AutomatoElectations.Connector;
using AutomatoElectations.Windows.WatcherVisual;
using System.Windows;

namespace AutomatoElectations.Windows.WatcherVisial
{
    /// <summary>
    /// Логика взаимодействия для Watcher.xaml
    /// </summary>
    public partial class Watcher : Window
    {
        public Watcher()
        {
            InitializeComponent();
            InitWatcPage();
        }

        public void InitWatcPage()
        {
            SuperList.Navigate(new WatcherPage());
        }

        private void ShowVoters_Click(object sender, RoutedEventArgs e)
        {
            SuperList.Navigate(new WatcherPage());
        }

        private void ShowCands_Click(object sender, RoutedEventArgs e)
        {
            SuperList.Navigate(new WatherCandPage());
        }

        private void ShowParts_Click(object sender, RoutedEventArgs e)
        {
            SuperList.Navigate(new PartyPage());
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Connect.idActualRole = 0;
            SingIn Log = new SingIn();
            Log.Show();
            this.Close();
        }

        private void SuperList_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        
    }
}
