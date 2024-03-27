using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfMvvm2703_1125.mvvm.model;
using WpfMvvm2703_1125.mvvm.view;

namespace WpfMvvm2703_1125.mvvm.viewmodel
{
    public class EditorDrinkVM : BaseVM
    {
        bool isEdit = false;
        MainVM mainVM;
        ListBox listTags;
        private Drink drink = new();

        public Drink Drink { 
            get => drink;
            set
            {
                drink = value;
                Signal();
            }
        }
        public VmCommand Save { get; set; }
        public List<string> AllTags { get; set; }

        public EditorDrinkVM()
        {
            AllTags = TagsRepository.Instance.GetTags();

            Save = new VmCommand(() => {
                if (!isEdit)
                    DrinkRepository.Instance.AddDrink(Drink);
                Drink.Tags.Clear();
                foreach (string tag in listTags.SelectedItems)
                    Drink.Tags.Add(tag);
                mainVM.CurrentPage = new ListDrinks(mainVM);
            });
        }

        internal void SetMainVM(MainVM mainVM, 
            ListBox listTags)
        {
            this.mainVM = mainVM;
            this.listTags = listTags;
        }

        internal void SetEditDrink(Drink selectedDrink)
        {
            isEdit = true;
            Drink = selectedDrink;
            foreach (var tag in Drink.Tags)
                listTags.SelectedItems.Add(tag);
        }
    }
}