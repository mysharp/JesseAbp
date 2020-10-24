using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Product.Contracts.Models.Dtos;
using Volo.Abp.Application.Services;

namespace Galaxy.Product.Contracts
{
    public interface IProductService :IApplicationService
    {
        Task<ProductDto> CreateAsync(ProductDto product);

        Task<ProductDto> GetAsync(string id);

        Task<IEnumerable<ProductDto>> GetListAsync(ProductQueryDto query);
    }
}
