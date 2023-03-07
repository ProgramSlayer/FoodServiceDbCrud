using System;

namespace WpfCrud.Models
{
    public class EditableDishCooking
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
        public DateTime CookedAt { get; set; } = DateTime.Now;
    }
}
