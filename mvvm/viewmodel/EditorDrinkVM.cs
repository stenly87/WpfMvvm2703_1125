﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfMvvm2703_1125.mvvm.model;
using WpfMvvm2703_1125.mvvm.view;

namespace WpfMvvm2703_1125.mvvm.viewmodel
{
    public class EditorDrinkVM : BaseVM
    {
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
        public VmCommand PickImage { get; set; }
        public List<Tag> AllTags { get; set; }

        public EditorDrinkVM()
        {
            AllTags = TagsRepository.Instance.GetTags();

            Save = new VmCommand(() => {
                Drink.Tags.Clear();
                foreach (Tag tag in listTags.SelectedItems)
                    Drink.Tags.Add(tag);

                if (Drink.ID == 0) 
                    DrinkRepository.Instance.AddDrink(Drink);
                else
                    DrinkRepository.Instance.UpdateDrink(Drink);

                mainVM.CurrentPage = new ListDrinks(mainVM);
            });

            PickImage = new VmCommand(() => {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Изображения|*.jpg;*.png";
            if (ofd.ShowDialog() == true && ofd.FileName != null)
                {
                    Drink.Image = File.ReadAllBytes(ofd.FileName);
                    Signal(nameof(Drink));
                }
            });
        }

        internal void SetMainVM(MainVM mainVM, 
            ListBox listTags)
        {
            this.mainVM = mainVM;
            this.listTags = listTags;
        }

        internal void SetEditDrink(Drink selectedDrink, System.Windows.Threading.Dispatcher dispatcher)
        {
            Drink = selectedDrink;
            foreach (var tag in Drink.Tags)
            {
                var search = AllTags.FirstOrDefault(s => s.ID == tag.ID);
                if (search != null)
                {
                    search.Selected = true;
                }
            }
        }
    }
}