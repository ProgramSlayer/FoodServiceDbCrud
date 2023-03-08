using System;

namespace WpfCrud.Models
{
    public class EditableDishCooking
    {
        public EditableDishCooking()
        {
        }

        public EditableDishCooking(int id, int dishId, int count, DateTime cookedAt)
        {
            Id = id;
            DishId = dishId;
            Count = count;
            CookedAt = cookedAt;
        }

        public int Id { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
        public DateTime CookedAt { get; set; } = DateTime.Now;
    }
}
