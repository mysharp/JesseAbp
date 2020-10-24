using System.Threading.Tasks;
using Galaxy.Order.Contracts.Models.Dtos;
using Volo.Abp.Application.Services;

namespace Galaxy.Order.Contracts
{
    public interface IOrderService: IApplicationService
    {
        Task<OrderDto> CreateAsync(OrderCreateDto order);

        Task<OrderDto> GetAsync(string id);
    }
}