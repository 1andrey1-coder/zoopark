using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zoopark.DTO;
using zoopark.View;

namespace zoopark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ПОООООИСК

        public string SearchText { get; set; }

        private List<Sotrudnikk> sotrudnikks;

        public List<Sotrudnikk> Sotrudnikk
        {
            get => sotrudnikks;
            set
            {
                sotrudnikks = value;
                Signal();
            }
        }
        void Signal([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<int> ViewRowsVariants { get; set; }
        public int ViewRowsCount
        {
            get => paginator.CountRows;
            set
            {
                paginator.CountRows = value;
                buttonToStart(this, null);
            }
        }


        Paginator<Sotrudnikk> paginator;
        public MainWindow()
        {

            DataContext = Navigation.GetInstance();
            InitializeComponent();
            DataContext = this;
            int rowsOnPage = 2;
            paginator = new Paginator<Sotrudnikk>(
                "SELECT * FROM tbl_Sotrudnikk",
                rowsOnPage,
                s => new Sotrudnikk
                {
                    ID = s.GetInt32("id"),
                    Name = s.GetString("Name"),
                    Otchestvo = s.GetString("Otchestvo")
                },
                "sotrudnikk"
                );

            ViewRowsVariants = new List<int>() { 5, 10, 15 };

            buttonToStart(this, null);

            
        }

        private void clickOpenPosition(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListPosition();

        }

        private void clickOpenSotrudnikk(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListSotrudnikk();
        }

        private void clickOpenTool(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListTools();
        }

        private void clickOpenFood(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListFood();
        }

        private void clickOpenFamily(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListFamily();
        }


        private void clickOpenAnimal(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListAnimal();
        }





        private void buttonToStart(object sender, RoutedEventArgs e)
        {
            paginator.PageIndex = 0;
            Sotrudnikk = paginator.GetPageValues();
        }

        private void buttonBack(object sender, RoutedEventArgs e)
        {
            paginator.PageIndex--;
            Sotrudnikk = paginator.GetPageValues();
        }

        private void buttonForward(object sender, RoutedEventArgs e)
        {
            paginator.PageIndex++;
            Sotrudnikk = paginator.GetPageValues();
        }

        private void buttonToEnd(object sender, RoutedEventArgs e)
        {
            paginator.PageIndex = int.MaxValue;
            Sotrudnikk = paginator.GetPageValues();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            paginator.Query = $"SELECT * FROM tbl_Sotrudnikk WHERE Name LIKE '%{SearchText}%' or Otchestvo LIKE '%{SearchText}%' ";
            buttonToStart(this, new RoutedEventArgs());
        }



      

        






    }
        
}
