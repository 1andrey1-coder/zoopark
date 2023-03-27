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
    /// Логика взаимодействия для EditAnimal.xaml
    /// </summary>
    public partial class EditAnimal : Page
    {
        public Animal Edit { get; set; }

        public Food SelectedFood { get; set; }
        public List<Food> Food { get; set; }

        public Family SelectedFamily { get; set; }
        public List<Family> Family { get; set; }

        public Sotrudnikk SelectedSotrudnikk { get; set; }
        public List<Sotrudnikk> Sotrudnikk { get; set; }

        public EditAnimal()
        {
            InitializeComponent();
            Food = DB.GetInstance().GetDataListFood();
            Family = DB.GetInstance().GetDataListFamily();
            Sotrudnikk = DB.GetInstance().GetDataListSotrudnik();

            Edit = new Animal();
            DataContext = this;
        }

        public EditAnimal(Animal edit)
        {
            InitializeComponent();
            Food = DB.GetInstance().GetDataListFood();
            Family = DB.GetInstance().GetDataListFamily();
            Sotrudnikk = DB.GetInstance().GetDataListSotrudnik();
            Edit = edit;
            DataContext = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (SelectedFood == null)
            {
                MessageBox.Show("Что-то с едой хреново");
                return;
            }
            if (SelectedFamily == null)
            {
                MessageBox.Show("Что-то с семейством хреново");
                return;
            }
            if (SelectedSotrudnikk == null)
            {
                MessageBox.Show("Что-то с сотрудником хреново");
                return;
            }
            Edit.IdFeed = SelectedFood.ID;
            Edit.IdFamily = SelectedFamily.ID; 
            Edit.IdSotrudnik = SelectedSotrudnikk.ID;

            if (Edit.ID == 0)
                DB.GetInstance().AddAnimal(Edit);


            Navigation.GetInstance().CurrentPage = new ListAnimal();
        }
    }

}


