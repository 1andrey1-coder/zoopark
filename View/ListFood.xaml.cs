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
    /// Логика взаимодействия для ListFood.xaml
    /// </summary>
    public partial class ListFood : Page
    {
        public List<Food> Food { get; set; }
        public Food SelectedFood { get; set; }
        public ListFood()
        {
            InitializeComponent();
            Food = DB.GetInstance().GetDataListFood();
            DataContext = this;
        }

        private void EditData(object sender, RoutedEventArgs e)
        {
            if (SelectedFood == null)
                return;
            Navigation.GetInstance().CurrentPage = new View.EditFood(SelectedFood);
        }

        private void RemoveData(object sender, RoutedEventArgs e)
        {
            DB.GetInstance().DeleteFood(SelectedFood);
            Navigation.GetInstance().CurrentPage = new View.ListFood();
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage =
                 new View.EditFood();

        }
    }
}
