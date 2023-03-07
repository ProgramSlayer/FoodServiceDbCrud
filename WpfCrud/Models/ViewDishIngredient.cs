namespace WpfCrud.Models
{
    public class ViewDishIngredient
    {
        public ViewDishIngredient(int dishId, string dishName, string dishTypeName, int productId, string productName, double requiredWeightGrams)
        {
            DishId = dishId;
            DishName = dishName;
            DishTypeName = dishTypeName;
            ProductId = productId;
            ProductName = productName;
            RequiredWeightGrams = requiredWeightGrams;
        }

        public int DishId { get; }
        public string DishName { get; }
        public string DishTypeName { get; }
        public int ProductId { get; }
        public string ProductName { get; }
        public double RequiredWeightGrams { get; }
    }
}
