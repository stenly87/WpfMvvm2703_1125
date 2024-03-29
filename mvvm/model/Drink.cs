using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm2703_1125.mvvm.model
{
    public class Drink
    {//Наименование, теги, объем, цена, состав
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty; 
        public List<Tag> Tags { get; set; } = new();
    }
}
