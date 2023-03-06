namespace WpfCrud.Models
{
    public class EditableDish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DishType DishType { get; set; }
        public double CookingTimeMinutes { get; set; }
        public double WeightGrams { get; set; }
        public string Recipe { get; set; }
        public byte[] Image { get; set; }
    }
}
