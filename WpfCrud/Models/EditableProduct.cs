namespace WpfCrud.Models
{
    public class EditableProduct
    {
        public EditableProduct(int id, string name, double caloricContentPer100Grams, double weightGrams, decimal pricePerKilogramRoubles)
        {
            Id = id;
            Name = name;
            CaloricContentPer100Grams = caloricContentPer100Grams;
            WeightGrams = weightGrams;
            PricePerKilogramRoubles = pricePerKilogramRoubles;
        }

        public EditableProduct()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double CaloricContentPer100Grams { get; set; }
        public double WeightGrams { get; set; }
        public decimal PricePerKilogramRoubles { get; set; }
    }
}
