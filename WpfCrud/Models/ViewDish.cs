namespace WpfCrud.Models
{
    public class ViewDish
    {
        public ViewDish(
            int id,
            string name,
            int dishTypeId,
            string dishTypeName,
            double cookingTimeMinutes,
            double weightGrams,
            string recipe,
            byte[] image,
            double? caloricContentPer100Grams,
            double? dishPriceRoubles)
        {
            Id = id;
            Name = name;
            DishTypeId = dishTypeId;
            DishTypeName = dishTypeName;
            CookingTimeMinutes = cookingTimeMinutes;
            WeightGrams = weightGrams;
            Recipe = recipe;
            Image = image;
            CaloricContentPer100Grams = caloricContentPer100Grams;
            DishPriceRoubles = dishPriceRoubles;
        }

        public int Id { get; }
        public string Name { get; }
        public int DishTypeId { get; }
        public string DishTypeName { get; }
        public double CookingTimeMinutes { get; }
        public double WeightGrams { get; }
        public string Recipe { get; }
        public byte[] Image { get; }
        public double? CaloricContentPer100Grams { get; }
        public double? DishPriceRoubles { get; }
    }
}
