namespace RestaurantsInNY.Models
{
    public class Restaurant
    {
        public int Restaurant_ID { get; set; }
        public string Restaurant_Name { get; set; }
        public string Restaurant_Location { get; set; }
        List<Menu> Menu { get; set; }
    }

    public class Menu
    {
        public int Menu_Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Item_Category { get; set; }
        public Restaurant Restaurant { get; set; }
        public Nutrition Nutrition { get; set; }

    }

    public class Nutrition
    {
        public int Nutrition_ID { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
        public int Sugar { get; set; }
        public int Total_Fat { get; set; }
        public Menu Menu { get; set; }

    }

}
