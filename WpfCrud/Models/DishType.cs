namespace WpfCrud.Models
{
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
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
