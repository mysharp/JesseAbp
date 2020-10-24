using Galaxy.Order.Contracts;
using Galaxy.Order.Contracts.Models.Dtos;
using Galaxy.Order.Entities;
using Galaxy.Product.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Galaxy.Order
{
    public class OrderService : ApplicationService, IOrderService
    {
        private readonly IList<OrderEntity> _orders;

        protected IProductService ProductService { get; set; }

        public OrderService(IProductService productService)
        {
            this.ProductService = productService;

            _orders = new List<OrderEntity>();
            _orders.Add(new OrderEntity {Id = "1", OrderNo = "PO-000001",ProductId = "1"});
            _orders.Add(new OrderEntity {Id = "2", OrderNo = "PO-000002", ProductId = "1" });
            _orders.Add(new OrderEntity {Id = "3", OrderNo = "PO-000003", ProductId = "2" });
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto order)
        {
            var product = await ProductService.GetAsync(order.ProductId);

            var entity = new OrderEntity
            {
                Id = order.Id,
                OrderNo = order.OrderNo,
                ProductId = order.ProductId
            };
            _orders.Add(entity);

            var dto = new OrderDto
            {
                Id = entity.Id,
                OrderNo = entity.OrderNo,
                Product = product
            };

            return dto;
        }

        public async Task<OrderDto> GetAsync(string id)
        {
            var order = _orders.FirstOrDefault(p => p.Id == id);
            if(order == null)
                throw new AbpException("Order is missing.");

            var product = await ProductService.GetAsync(order.ProductId);

            var dto = new OrderDto
            {
                Id = order.Id,
                OrderNo = order.OrderNo,
                Product = product
            };

            return dto;
        }
    }
}