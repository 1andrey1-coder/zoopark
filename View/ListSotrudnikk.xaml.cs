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
using System.Windows.Navigation;
using System.Windows.Shapes;
using zoopark.DTO;

namespace zoopark.View
{
    /// <summary>
    /// Логика взаимодействия для sotrudnikk.xaml
    /// </summary>
    public partial class ListSotrudnikk : Page
    {
        public List<Sotrudnikk> sotrudnikk { get; set; }
        public Sotrudnikk SelectedSotrudnikk { get; set; }
        public ListSotrudnikk()
        {
            InitializeComponent();
            sotrudnikk = DB.GetInstance().GetDataListSotrudnik();
            DataContext = this;
        }

        private void EditData(object sender, RoutedEventArgs e)
        {

            if (SelectedSotrudnikk == null)
                return;
            Navigation.GetInstance().CurrentPage = new View.EditSotrudnikk(SelectedSotrudnikk);
        }

        private void RemoveData(object sender, RoutedEventArgs e)
        {
            DB.GetInstance().DeleteSotrudnikk(SelectedSotrudnikk);
            Navigation.GetInstance().CurrentPage = new View.ListSotrudnikk();
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage =
                new View.EditSotrudnikk();

        }
    }
}
