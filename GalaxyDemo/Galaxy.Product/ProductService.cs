using Galaxy.Product.Contracts;
using Galaxy.Product.Contracts.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Galaxy.Product.Entities;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Galaxy.Product
{
    public class ProductService : ApplicationService, IProductService
    {
        private readonly IList<ProductEntity> _products;

        public ProductService()
        {
            _products = new List<ProductEntity>();

            _products.Add(new ProductEntity() {Id = "1", Name = "Apple"});
            _products.Add(new ProductEntity() {Id = "2", Name = "Orange"});
        }

        public Task<ProductDto> CreateAsync(ProductDto product)
        {
            var entity = new ProductEntity
            {
                Id = product.Id,
                Name = product.Name
            };
            _products.Add(entity);

            return Task.FromResult(product);
        }

        public async Task<ProductDto> GetAsync(string id)
        {
            var task = await Task.Run(() =>
            {
                Thread.Sleep(100);
                var entity = _products.FirstOrDefault(p => p.Id == id);
                if (entity == null)
                    throw new AbpException("Product is missing.");

                return new ProductDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            });

            return task;
        }

        public async Task<IEnumerable<ProductDto>> GetListAsync(ProductQueryDto query)
        {
            var task = await Task.Run(() =>
            {
                Thread.Sleep(100);
                return _products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).AsEnumerable();
            });

            return task;
        }
    }
}