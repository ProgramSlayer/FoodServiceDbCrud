namespace WpfCrud.Models
{
    public class EditableDish
    {
        public EditableDish(int id, string name, DishType dishType, double cookingTimeMinutes, double weightGrams, string recipe, byte[] image)
        {
            Id = id;
            Name = name;
            DishType = dishType;
            CookingTimeMinutes = cookingTimeMinutes;
            WeightGrams = weightGrams;
            Recipe = recipe;
            Image = image;
        }
        public EditableDish()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DishType DishType { get; set; }
        public double CookingTimeMinutes { get; set; }
        public double WeightGrams { get; set; }
        public string Recipe { get; set; }
        public byte[] Image { get; set; }
    }
}
