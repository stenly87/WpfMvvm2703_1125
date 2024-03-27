﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private string searchText;
        private ObservableCollection<Drink> drinks;

        public VmCommand Create { get; set; }
        public VmCommand Edit { get; set; }
        public VmCommand Delete { get; set; }

        public string SearchText {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }

        public List<string> AllTags { get; set; }
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
            AllTags = TagsRepository.Instance.GetTags();

            Drinks = new ObservableCollection<Drink>(DrinkRepository.Instance.GetAllDrinks());

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
                DrinkRepository.Instance.Search(SearchText));

        }
    }
}