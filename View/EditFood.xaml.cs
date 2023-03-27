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
    /// Логика взаимодействия для EditFood.xaml
    /// </summary>
    public partial class EditFood : Page
    {
        //public List<position> positions { get; set; }
        //public position SelectedTitle { get; set; }
        public Food Edit { get; set; }

        public EditFood()
        {
            InitializeComponent();
            //Title = DB.GetInstance().Getpositions();
            Edit = new Food();
            DataContext = this;
        }

        public EditFood(Food edit)
        {
            InitializeComponent();
            Edit = edit;
            DataContext = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            //if(SelectedTitle == null)
            //{
            //    MessageBox.Show("Выбери должность");
            //    return;
            //}
            //Edit.IDTitle = SelectedTitles.ID;
            if (Edit.ID == 0)
                DB.GetInstance().AddFood(Edit);
            else
                DB.GetInstance().EditFood(Edit);
            Navigation.GetInstance().CurrentPage = new ListFood();
        }
    }
}
