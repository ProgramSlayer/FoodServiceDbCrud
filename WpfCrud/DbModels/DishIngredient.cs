//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfCrud.DbModels
{

    /// <summary>
    /// Ингредиент блюда.
    /// </summary>
    public partial class DishIngredient
    {
        /// <summary>
        /// Идентификатор блюда.
        /// </summary>
        public int DishId { get; set; }

        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Требуемая масса продукта (г).
        /// </summary>
        public double RequiredWeightGrams { get; set; }
    
        /// <summary>
        /// Блюдо.
        /// </summary>
        public virtual Dish Dish { get; set; }
        
        /// <summary>
        /// Продукт.
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
