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
    /// Логика взаимодействия для ListFamily.xaml
    /// </summary>
 
    
        public partial class ListFamily : Page
        {
            public List<Family> Family { get; set; }
            public Family SelectedFamily { get; set; }
            public ListFamily()
            {
                InitializeComponent();
                Family = DB.GetInstance().GetDataListFamily();
                DataContext = this;
            }

            private void EditData(object sender, RoutedEventArgs e)
            {
                if (SelectedFamily == null)
                    return;
                Navigation.GetInstance().CurrentPage = new View.EditFamily(SelectedFamily);
            }

            private void RemoveData(object sender, RoutedEventArgs e)
            {

                DB.GetInstance().DeleteFamily(SelectedFamily);
                            Navigation.GetInstance().CurrentPage = new View.ListFamily();

            }

            private void AddData(object sender, RoutedEventArgs e)
            {
                Navigation.GetInstance().CurrentPage =
                    new View.EditFamily();

            }
       
    }
    

}
