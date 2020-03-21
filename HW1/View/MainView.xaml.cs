using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using CSharpHomework.Tools.Navigation;
using CSharpHomework.ViewModel;

namespace CSharpHomework.View
{
    public partial class MainView : /*UserControl,*/ INavigatable
    {
        public MainView()
        {
            InitializeComponent();
            MainViewModel _mainViewModel=new MainViewModel();
            DataContext = _mainViewModel;

            DateViewModel.MVM = _mainViewModel;


        }


        private DataGridColumn currentSortColumn;



        private ListSortDirection currentSortDirection;
        private void dg_Sorting(object sender, DataGridSortingEventArgs e)

        {


           
            e.Handled = true;



            MainViewModel mainViewModel = (MainViewModel)DataContext;


            string sortField = e.Column.SortMemberPath;


            ListSortDirection direction = ListSortDirection.Ascending;


            if (currentSortColumn != null)
                if (currentSortColumn == e.Column)
                    
                    direction = (currentSortDirection != ListSortDirection.Ascending) ?

                ListSortDirection.Ascending : ListSortDirection.Descending;


            bool sortAscending = direction == ListSortDirection.Ascending;

            Task task = new Task(() =>
                mainViewModel.Sort(sortField, sortAscending));

            task.Start();
            
            task.Wait();



            e.Column.SortDirection = direction;

            currentSortColumn = e.Column;

            currentSortDirection = direction;


        }
    }
}