using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using zoopark.DTO;

namespace zoopark.View
{
    /// <summary>
    /// Логика взаимодействия для Position.xaml
    /// </summary>
    public partial class EditSotrudnikk : Page
    {
        public List<Tools> Tools { get; set; }
        public ObservableCollection<Tools> SelectedTools { get; set; }

        public List<position> positions { get; set; }
        public position SelectedTitle { get; set; }
        public Sotrudnikk Edit { get; set; }

        public Tools SelectedFreeItem { get; set; }
        public Tools SelectedItem { get; set; }

        public EditSotrudnikk()
        {
            InitializeComponent();
            Edit = new Sotrudnikk();
            Load(Edit);
            DataContext = this;
        }

        private void Load(Sotrudnikk edit)
        {
            positions = DB.GetInstance().GetDataList();
            Tools = DB.GetInstance().GetDataListTool();
            SelectedTools = edit.ID > 0 ? new ObservableCollection<Tools>(DB.GetInstance().LoadToolsBySotrudnik(edit.ID)) : new ();
        }

        public EditSotrudnikk(Sotrudnikk edit)
        {
            InitializeComponent();
            Edit = edit;
            Load(Edit);
            DataContext = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(SelectedTitle == null)
            {
                MessageBox.Show("Выбери должность");
                return;
            }
            Edit.Idposition = SelectedTitle.ID;

            if (Edit.ID == 0)
            {
                int newId = DB.GetInstance().GetNextID("tbl_Sotrudnikk");
                DB.GetInstance().AddSotrudnikk(Edit);
                Edit.ID = newId;
            }
            else
                DB.GetInstance().EditSotrudnikk(Edit);

            DB.GetInstance().UpdateToolsForSotrudnik(Edit.ID, SelectedTools.Select(s => s.ID));

            Navigation.GetInstance().CurrentPage = new EditSotrudnikk();
        }

        private void SelectTool(object sender, RoutedEventArgs e)
        {
            if (SelectedFreeItem != null && !SelectedTools.Contains(SelectedFreeItem))
                SelectedTools.Add(SelectedFreeItem);
        }

        private void RemoveTool(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
                SelectedTools.Remove(SelectedItem);
        }
    }
}
