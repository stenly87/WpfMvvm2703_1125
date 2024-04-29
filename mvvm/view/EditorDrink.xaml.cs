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
using WpfMvvm2703_1125.mvvm.model;
using WpfMvvm2703_1125.mvvm.viewmodel;

namespace WpfMvvm2703_1125.mvvm.view
{
    /// <summary>
    /// Логика взаимодействия для EditorDrink.xaml
    /// </summary>
    public partial class EditorDrink : Page
    {
        public EditorDrink(viewmodel.MainVM mainVM)
        {
            InitializeComponent();
            var vm = ((EditorDrinkVM)DataContext);
            vm.SetMainVM(mainVM, listTags);
        }

        public EditorDrink(MainVM mainVM, Drink selectedDrink) : this(mainVM)
        {
            ((EditorDrinkVM)DataContext).SetEditDrink(selectedDrink, Dispatcher);
        }
    }
}
