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
    /// Логика взаимодействия для VoteWindow.xaml
    /// </summary>
    public partial class VoteWindow : Window
    {
        Random rnd = new Random();
        private List<Candidate> CandPhoto = new List<Candidate>();
        private string SerialNum;

        public VoteWindow()
        {
            InitializeComponent();
            SerialNum = RandomString(2) + RandomInt(12); 
            ShowСhoice();   
        }

        //Супер пупер запареный метод генерации серийного номера
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число являющееся латинским символом в юникоде
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                //Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static int RandomInt(int size)
        {
            Random random = new Random();
            int result = 0;
            for (int i = 0; i < size; i++)
            {
                //Генерируем число от 0 до 9, заполняем им разряд.
                result = (int)((result * 10) + (random.NextDouble() * 9));

                //Целое число не может начинаться с 0, если его разрядность больше 1
                if (size > 1 && result == 0)
                    result++;
            }
            return result;
        }

        public void ShowСhoice()
        {
            //хранение записи
            var ActualCandidate = Connect.Context.CandidatsCurrentDisplay.Where(i => i.CandId == Connect.idCand).FirstOrDefault();
            //Первоначальные данные
            tbVoteNum.Text = "Бюллетень №" + SerialNum;
            tbSName.Text = ActualCandidate.SName;
            tbFName.Text = ActualCandidate.FName;
            tbPName.Text = ActualCandidate.PName;
            tbPart.Text = Connect.Context.PartysList.Where(i => i.IdPart == ActualCandidate.Party).Select(i => i.NameParty).FirstOrDefault();
            //Фото кандидата
            CandPhoto = Connect.Context.Candidate.Where(i => i.CandId == Connect.idCand).ToList();
            CandPhohtoVis.ItemsSource = CandPhoto;
        }

        private void Vote_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы уверены что хотите голосовать за {tbSName.Text} {tbFName.Text} {tbPName.Text}?", "Голосование", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Connect.Context.Bulletin.Add(new Bulletin
                {
                    VotetFor = Connect.idCand,
                    GivedDate = DateTime.Now,
                    SerialNum = SerialNum
                });

                //флаг о том что юзер проголосовал
                var member = Connect.Context.Member.Where(i => i.ComID == Connect.idActualUser).FirstOrDefault();
                member.IsVoted = true;

                Connect.Context.SaveChanges();
                this.Close();
            }
           
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
