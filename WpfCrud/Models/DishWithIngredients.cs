using System.Collections.Generic;
using System.Linq;

namespace WpfCrud.Models
{
    /// <summary>
    /// Блюдо с его ингредиентами.
    /// </summary>
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

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Тип.
        /// </summary>
        public DishType DishType { get; }

        /// <summary>
        /// Время готовки (минут).
        /// </summary>
        public double CookingTimeMinutes { get; }

        /// <summary>
        /// Масса готового блюда (г).
        /// </summary>
        public double WeightGrams { get; }

        /// <summary>
        /// Рецепт.
        /// </summary>
        public string Recipe { get; }

        /// <summary>
        /// Изображение.
        /// </summary>
        public byte[] Image { get; }

        /// <summary>
        /// Ингредиенты.
        /// </summary>
        public IEnumerable<ViewDishIngredient> Ingredients { get; }
    }
}
