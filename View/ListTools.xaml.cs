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
    /// Логика взаимодействия для ListTools.xaml
    /// </summary>
    public partial class ListTools : Page
    {
        public List<Tools> tool { get; set; }
        public Tools SelectedTool { get; set; }
        public ListTools()
        {
            InitializeComponent();
            tool = DB.GetInstance().GetDataListTool();
            DataContext = this;
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            Navigation.GetInstance().CurrentPage =
               new View.EditTools();
        }

        private void RemoveData(object sender, RoutedEventArgs e)
        {
            DB.GetInstance().DeleteTool(SelectedTool);
            Navigation.GetInstance().CurrentPage = new View.ListTools();
        }

        private void EditData(object sender, RoutedEventArgs e)
        {
            if (SelectedTool == null)
                return;
            Navigation.GetInstance().CurrentPage = new View.EditTools(SelectedTool);
        }
    }
}
