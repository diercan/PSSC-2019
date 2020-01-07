using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMadeMarketAPI.Models;
using MongoDB.Driver;

namespace HomeMadeMarketAPI.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(IHomeMadeMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>(settings.ProductsCollectionName);
        }

        public List<Product> Get() =>
            _product.Find(product => true).ToList();

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await Task.Run(() => _product.Find(product => true).ToList());
        }
        public Product Get(string id) =>
            _product.Find<Product>(product => product.Id == id).FirstOrDefault();

        public List<Product> GetBySeller(string sellerId) =>
            _product.Find(product => product.SellerId == sellerId).ToList();

        public List<Product> GetByCategory(string category) =>
           _product.Find(product => product.Category == category).ToList();


        public Product Create(Product product)
        {
            _product.InsertOne(product);
            return product;
        }

        public void Update(string id, Product productIn) =>
            _product.ReplaceOne(product => product.Id == id, productIn);

        public void Remove(Product productIn) =>
            _product.DeleteOne(product => product.Id == productIn.Id);

        public void Remove(string id) =>
            _product.DeleteOne(product => product.Id == id);
    }
}
