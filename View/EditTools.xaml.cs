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
    /// Логика взаимодействия для EditTools.xaml
    /// </summary>
    public partial class EditTools : Page
    {
        //public List<position> positions { get; set; }
        //public position SelectedTitle { get; set; }
        public Tools Edit { get; set; }

        public EditTools()
        {
            InitializeComponent();
            //Title = DB.GetInstance().Getpositions();
            Edit = new Tools();
            DataContext = this;
        }

        public EditTools(Tools edit)
        {
            InitializeComponent();
            Edit = edit;
            DataContext = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Edit.ID == 0)
                DB.GetInstance().AddTool(Edit);
            else
                DB.GetInstance().EditTool(Edit);
            Navigation.GetInstance().CurrentPage = new ListTools();
        }
    }
}
