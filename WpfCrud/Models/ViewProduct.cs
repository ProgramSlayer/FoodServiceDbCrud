namespace WpfCrud.Models
{
    public class ViewProduct
    {
        public ViewProduct(int id, string name, double caloricContentPer100Grams, double weightGrams, decimal pricePerKilogramRoubles)
        {
            Id = id;
            Name = name;
            CaloricContentPer100Grams = caloricContentPer100Grams;
            WeightGrams = weightGrams;
            PricePerKilogramRoubles = pricePerKilogramRoubles;
        }

        public int Id { get; }
        public string Name { get; }
        public double CaloricContentPer100Grams { get; }
        public double WeightGrams { get; }
        public decimal PricePerKilogramRoubles { get; }
    }
}
