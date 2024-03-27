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
using WpfMvvm2703_1125.mvvm.viewmodel;

namespace WpfMvvm2703_1125.mvvm.view
{
    /// <summary>
    /// Логика взаимодействия для ListDrinks.xaml
    /// </summary>
    public partial class ListDrinks : Page
    {
        public ListDrinks(viewmodel.MainVM mainVM)
        {
            InitializeComponent();
            var vm = DataContext as ListDrinksVM;
            vm?.SetMainVM(mainVM);
            //((ListDrinksVM)DataContext).SetMainVM(mainVM);
            //DataContext = new ListDrinksVM(mainVM);
        }
    }
}
