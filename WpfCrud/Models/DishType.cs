namespace WpfCrud.Models
{
    /// <summary>
    /// Тип блюда.
    /// </summary>
    public class DishType
    {
        public DishType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public DishType()
        {
        }
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }
}
