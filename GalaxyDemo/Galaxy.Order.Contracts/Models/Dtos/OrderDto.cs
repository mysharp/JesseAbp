using Galaxy.Product.Contracts.Models.Dtos;

namespace Galaxy.Order.Contracts.Models.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }

        public string OrderNo { get; set; }

        public ProductDto Product { get; set; }
    }
}