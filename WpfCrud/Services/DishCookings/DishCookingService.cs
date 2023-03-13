using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using WpfCrud.Models;

namespace WpfCrud.Services.DishCookings
{
    /// <summary>
    /// Сервис для работы с приготовлениями блюд.
    /// </summary>
    public class DishCookingService
    {
        /// <summary>
        /// Фабрика по производству контекста.
        /// </summary>
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public DishCookingService()
        {
        }

        /// <summary>
        /// Извлекает все приготовления блюд из БД и возвращает их.
        /// </summary>
        /// <returns>Список приготовлений блюд.</returns>
        public async Task<IEnumerable<ViewDishCooking>> GetViewDishCookingsAsync()
        {
            using (var context = _factory())
            {
                var dbResults = await (
                    from dc in context.DishCookings
                    join d in context.Dishes on dc.DishId equals d.Id
                    join dt in context.DishTypes on d.DishTypeId equals dt.Id
                    select new
                    {
                        DishCookingId = dc.Id,
                        DishId = d.Id,
                        DishName = d.Name,
                        DishTypeName = dt.Name,
                        dc.Count,
                        dc.CookedAt,
                    }).ToListAsync();

                var results = from i in dbResults
                              select new ViewDishCooking(
                                  i.DishCookingId,
                                  i.DishId,
                                  i.DishName,
                                  i.DishTypeName,
                                  i.Count,
                                  i.CookedAt);
                return results;
            }
        }

        /// <summary>
        /// Возвращает все приготовления указанного блюда.
        /// </summary>
        /// <param name="dishName">Наименование блюда.</param>
        /// <returns>Список приготовлений указанного блюда.</returns>
        public async Task<IEnumerable<ViewDishCooking>> GetViewDishCookingsByDishNameAsync(string dishName)
        {
            if (string.IsNullOrWhiteSpace(dishName))
            {
                var results = await GetViewDishCookingsAsync();
                return results;
            }

            using (var context = _factory())
            {
                var dbResults = await (
                    from dc in context.DishCookings
                    join d in context.Dishes on dc.DishId equals d.Id
                    join dt in context.DishTypes on d.DishTypeId equals dt.Id
                    where d.Name.Contains(dishName)
                    select new
                    {
                        DishCookingId = dc.Id,
                        DishId = d.Id,
                        DishName = d.Name,
                        DishTypeName = dt.Name,
                        dc.Count,
                        dc.CookedAt,
                    }).ToListAsync();

                var results = from i in dbResults
                              select new ViewDishCooking(
                                  i.DishCookingId,
                                  i.DishId,
                                  i.DishName,
                                  i.DishTypeName,
                                  i.Count,
                                  i.CookedAt);
                return results;
            }
        }

        /// <summary>
        /// Добавляет приготовление в БД.
        /// </summary>
        /// <param name="dc">Приготовление.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<EditableDishCooking> AddDishCookingAsync(EditableDishCooking dc)
        {
            if (dc is null)
            {
                throw new ArgumentNullException(nameof(dc));
            }

            using (var context = _factory())
            {
                var dbdc = new DishCooking
                {
                    DishId = dc.DishId,
                    CookedAt = dc.CookedAt,
                    Count = dc.Count,
                };
                context.DishCookings.Add(dbdc);
                await context.SaveChangesAsync();
                dc.Id = dbdc.Id;
                return dc;
            }
        }

        /// <summary>
        /// Обновляет указанное приготовление в БД.
        /// </summary>
        /// <param name="dc">Приготовление.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateDishCookingAsync(EditableDishCooking dc)
        {
            if (dc is null)
            {
                throw new ArgumentNullException(nameof(dc));
            }

            using (var context = _factory())
            {
                var dbdc = await context
                    .DishCookings
                    .SingleOrDefaultAsync(c => c.Id == dc.Id)
                    ?? throw new Exception($"Приготовление (Id = {dc.Id}) не найдено в базе данных!");
                dbdc.DishId = dc.DishId;
                dbdc.Count = dc.Count;
                dbdc.CookedAt = dc.CookedAt;
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Удаляет указанное приготовление из БД.
        /// </summary>
        /// <param name="id">Идентификатор приготовления.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteDishCookingAsync(int id)
        {
            using (var context = _factory())
            {
                var dbdc = await context
                    .DishCookings
                    .SingleOrDefaultAsync(c => c.Id == id)
                    ?? throw new Exception($"Приготовление (Id = {id}) не найдено в базе данных!");
                context.DishCookings.Remove(dbdc);
                await context.SaveChangesAsync();
            }
        }
    }
}
