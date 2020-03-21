using CSharpHomework.ViewModel;

using System.Windows.Controls;
using  CSharpHomework.Tools.Navigation;


namespace CSharpHomework.View
{
    /// <summary>
    /// Логика взаимодействия для Data.xaml
    /// </summary>
    public partial class Date : UserControl, INavigatable
    {
        internal Date()
        {
            InitializeComponent();
            DateViewModel _dateViewModel=new DateViewModel();
            DataContext = _dateViewModel;
            MainViewModel.DVM = _dateViewModel;

        }

    }
}
