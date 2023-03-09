using System;
using ViewDish = WpfCrud.Models.ViewDish;
using DbDish = WpfCrud.DbModels.Dish;
using WpfCrud.DbModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WpfCrud.Models;

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

        public async Task<IEnumerable<ViewDish>> GetDishInfosAsync()
        {
            using (var context = _factory())
            {
                var dbViewDishes = await context.ViewDishes.ToListAsync();
                var viewDishes = from i in dbViewDishes
                                 select new ViewDish(
                                     i.DishId,
                                     i.DishName,
                                     i.DishTypeId,
                                     i.DishTypeName,
                                     i.DishCookingTimeMinutes,
                                     i.DishWeightGrams,
                                     i.DishRecipe,
                                     i.DishImage,
                                     i.DishCaloricContentPer100Grams,
                                     i.DishPriceRoubles);
                return viewDishes;
            }
        }

        public async Task<IEnumerable<DishWithIngredients>> GetDishesWithIngredientsAsync()
        {
            using (var context = _factory())
            {
                var dbDishesWithIngredients = await context
                    .Dishes
                    .Include(d => d.DishType)
                    .Include(d => d.DishIngredients.Select(di => di.Product))
                    .ToListAsync();
                List<DishWithIngredients> results = ToDishesWithIngredients(dbDishesWithIngredients);
                return results;
            }
        }

        public async Task<IEnumerable<DishWithIngredients>> GetDishesWithIngredientsByDishNameAsync(string dishName)
        {
            if (string.IsNullOrWhiteSpace(dishName))
            {
                var result = await GetDishesWithIngredientsAsync();
                return result;
            }

            using (var context = _factory())
            {
                var dbDishesWithIngredients = await context
                    .Dishes
                    .Where(d => d.Name.Contains(dishName))
                    .Include(d => d.DishType)
                    .Include(d => d.DishIngredients.Select(di => di.Product))
                    .ToListAsync();
                List<DishWithIngredients> results = ToDishesWithIngredients(dbDishesWithIngredients);
                return results;
            }
        }

        private static List<DishWithIngredients> ToDishesWithIngredients(List<DbDish> dbDishesWithIngredients)
        {
            var results = new List<DishWithIngredients>();
            foreach (var dbDish in dbDishesWithIngredients)
            {
                var ingredients = from di in dbDish.DishIngredients
                                  select new ViewDishIngredient(
                                      di.DishId,
                                      dbDish.Name,
                                      dbDish.DishType.Name,
                                      di.Product.Id,
                                      di.Product.Name,
                                      di.RequiredWeightGrams);
                var dishWithIngredients = new DishWithIngredients(
                    dbDish.Id,
                    dbDish.Name,
                    new Models.DishType(dbDish.DishTypeId, dbDish.DishType.Name),
                    dbDish.CookingTimeMinutes,
                    dbDish.WeightGrams,
                    dbDish.Recipe,
                    dbDish.Image,
                    ingredients);
                results.Add(dishWithIngredients);
            }

            return results;
        }

        public async Task<EditableDish> AddDishAsync(EditableDish dish)
        {
            if (dish is null)
            {
                throw new ArgumentNullException(nameof(dish));
            }

            using (var context = _factory())
            {
                var alreadyInBase = await context.Dishes.AnyAsync(d => d.Id == dish.Id || d.Name == dish.Name);
                if (alreadyInBase)
                {
                    throw new Exception($"Блюдо (Name = {dish.Name}) уже есть в базе данных!");
                }
                var dbDish = new DbDish
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
                return dish;
            }
        }

        public async Task UpdateDishAsync(EditableDish dish)
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
                var hasDishIngredients = await context.Entry(dbDish).Collection(d => d.DishIngredients).Query().AnyAsync();

                if (hasDishIngredients)
                {
                    throw new Exception($"Нельзя удалить блюдо (Id = {id}) из-за наличия связанных записей в таблице ингредиентов!");
                }
                
                var hasDishCookings = await context.Entry(dbDish).Collection(d => d.DishCookings).Query().AnyAsync();

                if (hasDishCookings)
                {
                    throw new Exception($"Нельзя удалить блюдо (Id = {id}) из-за наличия записей в таблице приготовлений!");
                }
                
                context.Dishes.Remove(dbDish);
                await context.SaveChangesAsync();
            }
        }
    }
}
