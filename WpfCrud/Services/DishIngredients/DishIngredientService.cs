using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using WpfCrud.Models;
using System.Linq;
using System.Data.Entity;

namespace WpfCrud.Services.DishIngredients
{
    public class DishIngredientService
    {
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public DishIngredientService()
        {
        }

        public DishIngredientService(Func<FoodServiceDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ViewDishIngredient>> GetViewDishIngredientsAsync()
        {
            using (var context = _factory())
            {
                var dbResults = await
                    (from di in context.DishIngredients
                     join d in context.Dishes on di.DishId equals d.Id
                     join dt in context.DishTypes on d.DishTypeId equals dt.Id
                     join p in context.Products on di.ProductId equals p.Id
                     select new
                     {
                         DishId = d.Id,
                         DishName = d.Name,
                         DishTypeName = dt.Name,
                         ProductId = p.Id,
                         ProductName = p.Name,
                         ProductRequiredWeightGrams = di.RequiredWeightGrams,
                     }
                    ).ToListAsync();

                var results = from i in dbResults
                              select new ViewDishIngredient(
                                  i.DishId,
                                  i.DishName,
                                  i.DishTypeName,
                                  i.ProductId,
                                  i.ProductName,
                                  i.ProductRequiredWeightGrams);
                return results;
            }
        }

        public async Task<EditableDishIngredient> AddDishIngredientAsync(EditableDishIngredient dishIngredient)
        {
            if (dishIngredient is null)
            {
                throw new ArgumentNullException(nameof(dishIngredient));
            }

            using (var context = _factory())
            {
                var alreadyInDatabase = await context
                    .DishIngredients
                    .AnyAsync(di => di.DishId == dishIngredient.DishId && di.ProductId == dishIngredient.ProductId);

                if (alreadyInDatabase)
                {
                    throw new Exception($"В таблице ингредиентов уже имеется такая запись!");
                }

                var dbDi = new DishIngredient
                {
                    DishId = dishIngredient.DishId,
                    ProductId = dishIngredient.ProductId,
                    RequiredWeightGrams = dishIngredient.RequiredWeightGrams,
                };

                context.DishIngredients.Add(dbDi);
                await context.SaveChangesAsync();
                return dishIngredient;
            }
        }

        public async Task UpdateDishIngredientAsync(EditableDishIngredient dishIngredient)
        {
            if (dishIngredient is null)
            {
                throw new ArgumentNullException(nameof(dishIngredient));
            }

            using (var context = _factory())
            {
                var dbDi = await context
                    .DishIngredients
                    .SingleOrDefaultAsync(
                        di => di.DishId == dishIngredient.DishId 
                        && di.ProductId == dishIngredient.ProductId) 
                    ?? throw new Exception($"Ингредиент (DishId = {dishIngredient.DishId}, ProductId = {dishIngredient.ProductId}) не найден в базе данных!");
                dbDi.RequiredWeightGrams = dishIngredient.RequiredWeightGrams;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteDishIngredientAsync(int dishId, int productId)
        {
            using (var context = _factory())
            {
                var dbDi = await context
                    .DishIngredients
                    .SingleOrDefaultAsync(
                        di => di.DishId == dishId
                        && di.ProductId == productId)
                    ?? throw new Exception($"Ингредиент (DishId = {dishId}, ProductId = {productId}) не найден в базе данных!");
                context.DishIngredients.Remove(dbDi);
                await context.SaveChangesAsync();
            }
        }
    }
}
