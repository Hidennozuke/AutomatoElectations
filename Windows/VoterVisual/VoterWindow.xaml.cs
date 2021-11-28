using AutomatoElectations.Connector;
using AutomatoElectations.Windows.WatcherVisual;
using System.Linq;
using System.Windows;


namespace AutomatoElectations.Windows.VoterVisual
{
    public partial class VoterWindow : Window
    {
        public VoterWindow()
        {
            InitializeComponent();
            SuperList.Navigate(new WatherCandPage());
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
