﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using WpfCrud.Models;

namespace WpfCrud.Services.DishCookings
{
    public class DishCookingService
    {
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public DishCookingService()
        {
        }

        public DishCookingService(Func<FoodServiceDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

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