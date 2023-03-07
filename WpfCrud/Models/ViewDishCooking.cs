using System;

namespace WpfCrud.Models
{
    public class ViewDishCooking
    {
        public ViewDishCooking(int id, int dishId, string dishName, string dishTypeName, int count, DateTime cookedAt)
        {
            Id = id;
            DishId = dishId;
            DishName = dishName;
            DishTypeName = dishTypeName;
            Count = count;
            CookedAt = cookedAt;
        }

        public int Id { get; }
        public int DishId { get; }
        public string DishName { get; }
        public string DishTypeName { get; }
        public int Count { get; }
        public DateTime CookedAt { get; }
    }
}
