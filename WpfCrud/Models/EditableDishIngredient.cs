namespace WpfCrud.Models
{
    public class EditableDishIngredient
    {
        public EditableDishIngredient(int dishId, int productId, double requiredWeightGrams)
        {
            DishId = dishId;
            ProductId = productId;
            RequiredWeightGrams = requiredWeightGrams;
        }

        public EditableDishIngredient()
        {
        }

        public int DishId { get; set; }
        public int ProductId { get; set; }
        public double RequiredWeightGrams { get; set; }
    }
}
