using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using DbDishType = WpfCrud.DbModels.DishType;
using DishType = WpfCrud.Models.DishType;

namespace WpfCrud.Services.DishTypes
{
    public class DishTypeService
    {
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public DishTypeService()
        {
        }

        public DishTypeService(Func<FoodServiceDbContext> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IEnumerable<DishType>> GetDishTypesAsync()
        {
            using (var context = _factory())
            {
                var dbDishTypes = await context.DishTypes.ToListAsync();
                var dishTypes = 
                    from dbDt in dbDishTypes
                    select new DishType
                    {
                        Id = dbDt.Id,
                        Name = dbDt.Name
                    };
                return dishTypes;
            }
        }

        public async Task<DishType> AddDishTypeAsync(DishType dishType)
        {
            if (dishType == null)
            {
                throw new ArgumentNullException(nameof(dishType));
            }

            using (var context = _factory())
            {
                var dbDishType = await context.DishTypes.SingleOrDefaultAsync(dt => dt.Name == dishType.Name);
                if (!(dbDishType is null))
                {
                    throw new Exception("Указанный тип блюда уже существует в базе данных!");
                }
                dbDishType = new DbDishType { Name = dishType.Name };
                context.DishTypes.Add(dbDishType);
                await context.SaveChangesAsync();
                dishType.Id = dbDishType.Id;
                return dishType;
            }
        }

        public async Task<int> GetDishCountByDishType(int dishTypeId)
        {
            using (var context = _factory())
            {
                var count = await context.Dishes.CountAsync(d => d.DishTypeId == dishTypeId);
                return count;
            }
        }

        public async Task DeleteDishTypeAsync(int id)
        {
            using (var context = _factory())
            {
                var dbDishType = await context.DishTypes.SingleOrDefaultAsync(dt => dt.Id == id);
                if (dbDishType is null)
                {
                    throw new Exception($"Тип блюда (Id = {id}) не найден в базе данных!");
                }
                context.DishTypes.Remove(dbDishType);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateDishTypeAsync(DishType dishType)
        {
            if (dishType == null)
            {
                throw new ArgumentNullException(nameof(dishType));
            }

            using (var context = _factory())
            {
                var dbDishType = await context.DishTypes.SingleOrDefaultAsync(dt => dt.Id == dishType.Id);
                if (dbDishType is null)
                {
                    throw new Exception($"Тип блюда (Id = {dishType.Id}) не найден в базе данных!");
                }
                dbDishType.Name = dishType.Name;
                await context.SaveChangesAsync();
            }
        }
    }
}
