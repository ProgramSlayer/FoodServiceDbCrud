using System;
using Dish = WpfCrud.Models.Dish;
using DbDish = WpfCrud.DbModels.Dish;
using WpfCrud.DbModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace WpfCrud.Services.Dishes
{
    public class DishService
    {
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public DishService()
        {
        }

        public DishService(Func<FoodServiceDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IEnumerable<Dish>> GetDishesAsync()
        {
            using (var context = _factory())
            {
                var dbDishes = await context
                    .Dishes
                    .Include(d => d.DishType)
                    .Include(d => d.DishIngredients.Select(di => di.Product))
                    .ToListAsync();

                var dishes = new List<Dish>();
                foreach (var dbDish in dbDishes)
                {
                    var dishCaloricContentPer100Grams =
                        dbDish.DishIngredients
                        .Sum(
                            di => di.Product.CaloricContentPer100Grams
                            * di.RequiredWeightGrams / 100)
                        / dbDish.WeightGrams * 100;
                    var dish = new Dish
                    {
                        Id = dbDish.Id,
                        Name = dbDish.Name,
                        DishType = new Models.DishType
                        {
                            Id = dbDish.DishTypeId,
                            Name = dbDish.DishType.Name,
                        },
                        CookingTimeMinutes = dbDish.CookingTimeMinutes,
                        WeightGrams = dbDish.WeightGrams,
                        Recipe = dbDish.Recipe,
                        Image = dbDish.Image,
                        CaloricContentPer100Grams = dishCaloricContentPer100Grams,
                    };
                    dishes.Add(dish);
                }

                return dishes;
            }
        }

        public async Task<Dish> AddDishAsync(Dish dish)
        {
            if (dish == null)
            {
                throw new ArgumentNullException(nameof(dish));
            }

            using (var context = _factory())
            {
                var dbDish = await context.Dishes.SingleOrDefaultAsync(d => d.Name == dish.Name);
                if (!(dbDish is null))
                {
                    throw new Exception($"Блюдо (Name = {dish.Name}) уже есть в базе данных!");
                }
                dbDish = new DbDish
                {
                    Name = dish.Name,
                    CookingTimeMinutes = dish.CookingTimeMinutes,
                    DishTypeId = dish.DishType.Id,
                    WeightGrams = dish.WeightGrams,
                    Image = dish.Image,
                    Recipe = dish.Recipe,
                };
                context.Dishes.Add(dbDish);
                await context.SaveChangesAsync();
                dish.Id = dbDish.Id;
                dish.DishType.Name = dbDish.DishType.Name;
                return dish;
            }
        }

        public async Task UpdateDishAsync(Dish dish)
        {
            if (dish == null)
            {
                throw new ArgumentNullException(nameof(dish));
            }

            using (var context = _factory())
            {
                var dbDish = await context
                    .Dishes
                    .SingleOrDefaultAsync(d => d.Id == dish.Id) 
                    ?? throw new Exception($"Блюдо (Id = {dish.Id}) не найдено в базе данных!");
                dbDish.Name = dish.Name;
                dbDish.DishTypeId = dish.DishType.Id;
                dbDish.CookingTimeMinutes = dish.CookingTimeMinutes;
                dbDish.WeightGrams = dish.WeightGrams;
                dbDish.Recipe = dish.Recipe;
                dbDish.Image = dish.Image;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteDishAsync(int id)
        {
            using (var context = _factory())
            {
                var dbDish = await context
                    .Dishes
                    .SingleOrDefaultAsync(d => d.Id == id)
                    ?? throw new Exception($"Блюдо (Id = {id}) не найдено в базе данных!");
                var dishIngredientCount = await context
                    .Entry(dbDish)
                    .Collection(d => d.DishIngredients)
                    .Query()
                    .CountAsync();
                var dishCookingCount = await context
                    .Entry(dbDish)
                    .Collection(d => d.DishCookings)
                    .Query()
                    .CountAsync();
                if (dishIngredientCount != 0)
                {
                    throw new Exception($"Нельзя удалить блюдо (Id = {id}) из-за наличия связанных записей в таблице ингредиентов!");
                }
                if (dishCookingCount != 0)
                {
                    throw new Exception($"Нельзя удалить блюдо (Id = {id}) из-за наличия записей в таблице приготовлений!");
                }
                context.Dishes.Remove(dbDish);
                await context.SaveChangesAsync();
            }
        }
    }
}
