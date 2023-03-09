namespace WpfCrud.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DishType DishType { get; set; }
        public double CookingTimeMinutes { get; set; }
        public double WeightGrams { get; set; }
        public string Recipe { get; set; }
        public byte[] Image { get; set; }
        public double CaloricContentPer100Grams { get; set; }
    }
}
