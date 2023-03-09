using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfCrud.DbModels;
using WpfCrud.Models;

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

        public async Task<IEnumerable<ViewProduct>> GetViewProductsAsync()
        {
            using (var context = _factory())
            {
                var dbProducts = await context.Products.ToListAsync();
                var products = from dbp in dbProducts
                               select new ViewProduct(
                                   dbp.Id,
                                   dbp.Name,
                                   dbp.CaloricContentPer100Grams,
                                   dbp.WeightGrams,
                                   dbp.PricePerKilogramRoubles);
                return products;
            }
        }

        public async Task<IEnumerable<ViewProduct>> GetViewProductsByNameAsync(string prodName)
        {
            if (string.IsNullOrWhiteSpace(prodName))
            {
                var allProducts = await GetViewProductsAsync();
                return allProducts;
            }

            using (var context = _factory())
            {
                var dbProducts = await context.Products.Where(p => p.Name.Contains(prodName)).ToListAsync();
                var products = from dbp in dbProducts
                               select new ViewProduct(
                                   dbp.Id,
                                   dbp.Name,
                                   dbp.CaloricContentPer100Grams,
                                   dbp.WeightGrams,
                                   dbp.PricePerKilogramRoubles);
                return products;
            }
        }

        public async Task<EditableProduct> AddProductAsync(EditableProduct product)
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

                dbProd = new Product
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

        public async Task UpdateProductAsync(EditableProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using (var context = _factory())
            {
                var dbProd = await context.Products.SingleOrDefaultAsync(p => p.Id == product.Id)
                    ?? throw new Exception($"Продукт (Id = {product.Id}) не найден в базе данных!");
                dbProd.Name = product.Name;
                dbProd.CaloricContentPer100Grams = product.CaloricContentPer100Grams;
                dbProd.PricePerKilogramRoubles = product.PricePerKilogramRoubles;
                dbProd.WeightGrams = product.WeightGrams;

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using (var context = _factory())
            {
                var dbProd = await context.Products.SingleOrDefaultAsync(p => p.Id == id)
                    ?? throw new Exception($"Продукт (Id = {id}) не найден в базе данных!");
                context.Products.Remove(dbProd);
                await context.SaveChangesAsync();
            }
        }
    }
}
