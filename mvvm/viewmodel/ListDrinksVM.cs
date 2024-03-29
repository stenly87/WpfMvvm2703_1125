using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMvvm2703_1125.mvvm.model;
using WpfMvvm2703_1125.mvvm.view;

namespace WpfMvvm2703_1125.mvvm.viewmodel
{
    public class ListDrinksVM: BaseVM
    {
        private MainVM mainVM;
        private string searchText = "";
        private ObservableCollection<Drink> drinks;
        private Tag selectedTag;

        public VmCommand Create { get; set; }
        public VmCommand Edit { get; set; }
        public VmCommand Delete { get; set; }

        public Tag SelectedTag { 
            get => selectedTag;
            set
            {
                selectedTag = value;
                Signal();
                Search();
            }
        }

        public string SearchText {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }

        public ObservableCollection<Tag> AllTags { get; set; }
        public Drink SelectedDrink { get; set; }
        public ObservableCollection<Drink> Drinks { 
            get => drinks;
            set
            {
                drinks = value;
                Signal();
            }
        }

        public ListDrinksVM()
        {
            AllTags = new ObservableCollection<Tag>( TagsRepository.Instance.GetTags());
            AllTags.Insert(0, new Tag { ID = 0, Title = "Все теги" });
            SelectedTag = AllTags[0];
            string sql = "SELECT d.id, d.Title, d.Capacity, d.Price, d.Description, tt.id AS tagId, tt.Title AS tagTitle FROM CrossDrinkTag cdt, Drink d, TagsTable tt WHERE cdt.idDrink = d.id AND cdt.idTag = tt.id";

            Drinks = new ObservableCollection<Drink>(DrinkRepository.Instance.GetAllDrinks(sql));

            Create = new VmCommand(() =>
            {
                mainVM.CurrentPage = new EditorDrink(mainVM);
            });

            Edit = new VmCommand(() => {
                if (SelectedDrink == null)
                    return;
                mainVM.CurrentPage = new EditorDrink(mainVM, SelectedDrink);
            });

            Delete = new VmCommand(() => {
                if (SelectedDrink == null)
                    return;

                if (MessageBox.Show("Удаление напитка", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DrinkRepository.Instance.Remove(SelectedDrink);
                    Drinks.Remove(SelectedDrink);
                }
            });

        }

        internal void SetMainVM(MainVM mainVM)
        {
           this.mainVM = mainVM;
        }

        private void Search()
        {
            Drinks = new ObservableCollection<Drink>(
                DrinkRepository.Instance.Search(SearchText, SelectedTag));

        }
    }
}
