using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WpfCrud.Models
{
    public class DishWithIngredients
    {
        public DishWithIngredients(int id, string name, DishType dishType, double cookingTimeMinutes, double weightGrams, string recipe, byte[] image, IEnumerable<ViewDishIngredient> ingredients)
        {
            Id = id;
            Name = name;
            DishType = dishType;
            CookingTimeMinutes = cookingTimeMinutes;
            WeightGrams = weightGrams;
            Recipe = recipe;
            Image = image;
            Ingredients = ingredients ?? Enumerable.Empty<ViewDishIngredient>();
        }

        public int Id { get; }
        public string Name { get; }
        public DishType DishType { get; }
        public double CookingTimeMinutes { get; }
        public double WeightGrams { get; }
        public string Recipe { get; }
        public byte[] Image { get; }
        public IEnumerable<ViewDishIngredient> Ingredients { get; }
    }
}
