namespace WpfCrud.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CaloricContentPer100Grams { get; set; }
        public double WeightGrams { get; set; }
        public decimal PricePerKilogramRoubles { get; set; }
    }
}
