using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutomatoElectations.Connector;
using AutomatoElectations.Windows.VoterVisual;
using AutomatoElectations.Windows.WatcherVisial;

namespace AutomatoElectations
{
    /// <summary>
    /// Логика взаимодействия для SingIn.xaml
    /// </summary>
    public partial class SingIn : Window
    {
        public SingIn()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var logging = Connect.Context.Member.Where(i => i.Login == Log1.Text && i.Password == Pas1.Password && i.IsDeleted == false).FirstOrDefault();

            if (Log1.ToString() != "" && Pas1.ToString() != "" && logging != null)
            {
                switch (logging.IdRole)
                {
                    case 1:
                        Watcher wW = new Watcher();
                        wW.Show();
                        Connect.idActualRole = 1;
                        Connect.idActualUser = logging.ComID;
                        this.Close();
                        break;

                    case 2:
                        VoterWindow vW = new VoterWindow();
                        vW.Show();
                        Connect.idActualRole = 2;
                        Connect.idActualUser = logging.ComID;
                        this.Close();
                        break;

                    default:

                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверные данные для входа");
            }
        }

        
        
    }
}
