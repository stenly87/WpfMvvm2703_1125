using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm2703_1125.mvvm.model
{
    public class DrinkRepository
    {
        private DrinkRepository()
        {
            
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

        internal IEnumerable<Drink> GetAllDrinks(string sql)
        {
            var result = new List<Drink>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            { 
                Drink drink = null;
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("id");
                    drink = result.FirstOrDefault(s => s.ID == id);
                    if (drink == null)
                    {
                        drink = new Drink();
                        result.Add(drink);
                        drink.ID = id;
                        drink.Title = reader.GetString("Title");
                        drink.Capacity = reader.GetInt32("Capacity");
                        drink.Price = reader.GetDouble("Price");
                        drink.Description = reader.GetString("Description");
                        if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                        {
                            using (var mcImg = reader.GetStream("Image")) 
                            {
                                drink.Image = new byte[mcImg.Length];
                                mcImg.Read(drink.Image, 0, drink.Image.Length);
                            }
                        }
                    }
                    Tag tag = new Tag
                    {
                        ID = reader.GetInt32("tagId"),
                        Title = reader.GetString("tagTitle"),
                    };
                    drink.Tags.Add(tag);
                }
            }
            
            return result;
        }

        internal void AddDrink(Drink drink)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = MySqlDB.Instance.GetAutoID("Drink");

            string sql = "INSERT INTO Drink VALUES (0, @title, @capacity, @price, @description, @image)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", drink.Title));
                mc.Parameters.Add(new MySqlParameter("capacity", drink.Capacity));
                mc.Parameters.Add(new MySqlParameter("price", drink.Price));
                mc.Parameters.Add(new MySqlParameter("description", drink.Description));
                mc.Parameters.Add(new MySqlParameter("image", drink.Image));
                if (mc.ExecuteNonQuery() > 0)
                {
                    sql = "";
                    foreach (var tag in drink.Tags)
                        sql += "INSERT INTO CrossDrinkTag VALUES ("+ id +","+tag.ID+");";
                    using (var mcCross = new MySqlCommand(sql, connect))
                        mcCross.ExecuteNonQuery();
                }
            }
        }

        internal void Remove(Drink drink)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM CrossDrinkTag WHERE idDrink = '" + drink.ID + "';"; 
            sql += "DELETE FROM Drink WHERE id = '" + drink.ID + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

        internal IEnumerable<Drink> Search(string searchText, Tag selectedTag)
        {
            string sql = "SELECT d.id, d.Title, d.Capacity, d.Price, d.Description, d.Image, tt.id AS tagId, tt.Title AS tagTitle FROM CrossDrinkTag cdt, Drink d, TagsTable tt WHERE cdt.idDrink = d.id AND cdt.idTag = tt.id";
            sql += " AND (d.Title LIKE '%" + searchText + "%'";
            sql += " OR d.Description LIKE '%" + searchText + "%') order by d.id";

            if (selectedTag.ID != 0)
            { // это не включено в запрос, так как в противном случае потеряются теги
                var result = GetAllDrinks(sql).Where(s=>s.Tags.FirstOrDefault(s=>s.ID == selectedTag.ID) != null);
                return result;
            }
            return GetAllDrinks(sql);
        }

        internal void UpdateDrink(Drink drink)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM CrossDrinkTag WHERE idDrink = '" + drink.ID + "';";
            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();

            sql = "";
            foreach (var tag in drink.Tags)
                sql += "INSERT INTO CrossDrinkTag VALUES (" + drink.ID + "," + tag.ID + ");";
            using (var mcCross = new MySqlCommand(sql, connect))
                mcCross.ExecuteNonQuery();

            sql = "UPDATE Drink SET Title = @title, Capacity = @capacity, Price = @price, Description = @description, Image = @image WHERE Id = " + drink.ID;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("title", drink.Title));
                mc.Parameters.Add(new MySqlParameter("capacity", drink.Capacity));
                mc.Parameters.Add(new MySqlParameter("price", drink.Price));
                mc.Parameters.Add(new MySqlParameter("description", drink.Description));
                mc.Parameters.Add(new MySqlParameter("image", drink.Image));
                mc.ExecuteNonQuery();
            }
        }
    }
}
