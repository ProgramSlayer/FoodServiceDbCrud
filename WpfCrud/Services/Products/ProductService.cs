using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using DbProduct = WpfCrud.DbModels.Product;
using Product = WpfCrud.Models.Product;

namespace WpfCrud.Services.Products
{
    public class ProductService
    {
        private readonly Func<FoodServiceDbContext> _factory = () => new FoodServiceDbContext();

        public ProductService()
        {
        }

        public ProductService(Func<FoodServiceDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using (var context = _factory())
            {
                var dbProducts = await context.Products.ToListAsync();
                var products = from dbp in dbProducts
                               select new Product
                               {
                                   Id = dbp.Id,
                                   Name = dbp.Name,
                                   CaloricContentPer100Grams = dbp.CaloricContentPer100Grams,
                                   PricePerKilogramRoubles = dbp.PricePerKilogramRoubles,
                                   WeightGrams = dbp.WeightGrams,
                               };
                return products;
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var context = _factory())
            {
                var dbProd = await context.Products.SingleOrDefaultAsync(p => p.Name == product.Name);
                if (!(dbProd is null))
                {
                    throw new Exception("Продукт с таким наименованием уже есть в базе данных!");
                }

                dbProd = new DbProduct
                {
                    Name = product.Name,
                    CaloricContentPer100Grams = product.CaloricContentPer100Grams,
                    PricePerKilogramRoubles = product.PricePerKilogramRoubles,
                    WeightGrams = product.WeightGrams,
                };

                context.Products.Add(dbProd);
                await context.SaveChangesAsync();
                product.Id = dbProd.Id;
                return product;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var context = _factory())
            {
                var dbProd = await context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);
                if (dbProd is null)
                {
                    throw new Exception($"Продукт (Id = {product.Id}) не найден в базе данных!");
                }

                dbProd.Name = product.Name;
                dbProd.CaloricContentPer100Grams = product.CaloricContentPer100Grams;
                dbProd.PricePerKilogramRoubles = product.PricePerKilogramRoubles;
                dbProd.WeightGrams = dbProd.WeightGrams;

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using (var context = _factory())
            {
                var dbProd = await context.Products.SingleOrDefaultAsync(p => p.Id == id);
                if (dbProd is null)
                {
                    throw new Exception($"Продукт (Id = {id}) не найден в базе данных!");
                }

                context.Products.Remove(dbProd);
                await context.SaveChangesAsync();
            }
        }
    }
}
