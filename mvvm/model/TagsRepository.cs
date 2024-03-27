using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm2703_1125.mvvm.model
{
    internal class TagsRepository
    {
        List<string> tags;
        private TagsRepository()
        {
            tags = new List<string>(new string[]{
                "Кофе",
                "Чай",
                "Молоко",
                "Сахар",
                "Сироп",
                "Вода"
            });
        }
        static TagsRepository instance;
        public static TagsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new TagsRepository();
                return instance;
            }
        }

        internal List<string>? GetTags()
        {
            return tags;
        }
    }
}
