using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm2703_1125.mvvm.model
{
    public class DrinkRepository
    {
        List<Drink> drinks = new();
        private DrinkRepository()
        {
            drinks.Add(new Drink
            {
                Title = "Кофе",
                Capacity = 100,
                Description = "Кофе с молоком",
                Price = 10000,
                Tags = new List<string>(new string[] {
                  "Арабика",
                  "Молоко",
                  "Сахар"
                 })
            });
            drinks.Add(new Drink
            {
                Title = "Чай",
                Capacity = 100,
                Description = "Чай с молоком",
                Price = 10000,
                Tags = new List<string>(new string[] {
                  "Принцесса дури",
                  "Молоко",
                  "Сахар"
                 })
            });
        }

        static DrinkRepository instance;
        public static DrinkRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new DrinkRepository();
                return instance;
            }
        }

        internal IEnumerable<Drink> GetAllDrinks()
        {
           return drinks;
        }

        internal void AddDrink(Drink drink)
        {
            drinks.Add(drink);
        }

        internal void Remove(Drink drink)
        {
            drinks.Remove(drink);
        }

        internal IEnumerable<Drink> Search(string searchText, string selectedTag)
        {
            if (selectedTag == "Все теги")
                return drinks.Where(s => 
                    s.Title.Contains(searchText) ||
                    s.Description.Contains(searchText));
                else
                    return drinks.Where(s =>
                    (s.Title.Contains(searchText) ||
                    s.Description.Contains(searchText)) &&
                    s.Tags.Contains(selectedTag));
        }
    }
}
