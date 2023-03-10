namespace WpfCrud.Models
{
    public class EditableDish
    {
        public EditableDish()
        {
        }

        public EditableDish(int id, string name, int dishTypeId, double cookingTimeMinutes, double weightGrams, string recipe, byte[] image)
        {
            Id = id;
            Name = name;
            DishTypeId = dishTypeId;
            CookingTimeMinutes = cookingTimeMinutes;
            WeightGrams = weightGrams;
            Recipe = recipe;
            Image = image;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DishTypeId { get; set; }
        public double CookingTimeMinutes { get; set; }
        public double WeightGrams { get; set; }
        public string Recipe { get; set; }
        public byte[] Image { get; set; }
    }
}
