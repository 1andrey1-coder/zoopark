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
    /// Логика взаимодействия для ListPosition.xaml
    /// </summary>
    public partial class ListPosition : Page
    {
        public List<position> Data { get; set; }
        public position SelectedPost { get; set; }
        public ListPosition()
        {
            InitializeComponent();
            Data = DB.GetInstance().GetDataList();
            DataContext = this;
        }

        private void EditData(object sender, RoutedEventArgs e)
        {
            if (SelectedPost == null)
                return;
            Navigation.GetInstance().CurrentPage = new EditPosition(SelectedPost);
        }

        private void RemoveData(object sender, RoutedEventArgs e)
        {
            DB.GetInstance().DeletePostition(SelectedPost);
            Navigation.GetInstance().CurrentPage = new View.ListPosition();

        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = 
                new View.EditPosition();

        }
    }
}
