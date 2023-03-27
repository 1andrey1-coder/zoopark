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
    /// Логика взаимодействия для ListAnimal.xaml
    /// </summary>
    public partial class ListAnimal : Page
    {
        public List<Animal> animal { get; set; }
        public Animal SelectedAnimal { get; set; }
        public ListAnimal()
        {
            InitializeComponent();
            animal = DB.GetInstance().GetDataListAnimal();
            DataContext = this;
        }

        private void EditData(object sender, RoutedEventArgs e)
        {
            if (SelectedAnimal == null)
                return;
            Navigation.GetInstance().CurrentPage = new View.EditAnimal(SelectedAnimal);
        }

        private void RemoveData(object sender, RoutedEventArgs e)
        {
            DB.GetInstance().DeleteAnimal(SelectedAnimal);
            Navigation.GetInstance().CurrentPage = new View.ListAnimal();
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage =
                new View.EditAnimal();

        }
    }
}
