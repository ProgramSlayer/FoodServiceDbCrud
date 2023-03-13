namespace WpfCrud.Models
{
    /// <summary>
    /// Информация о блюде (без ингредиентов).
    /// </summary>
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

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Идентификатор типа блюда.
        /// </summary>
        public int DishTypeId { get; }

        /// <summary>
        /// Название типа блюда.
        /// </summary>
        public string DishTypeName { get; }

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
        /// Калорийность (ккал / 100 г) (на основе калорийности ингредиентов).
        /// </summary>
        public double? CaloricContentPer100Grams { get; }

        /// <summary>
        /// Стоимость 1 ед. блюда (руб.).
        /// </summary>
        public double? DishPriceRoubles { get; }
    }
}
