using PSP_Komanda32_API.DTOs;
using PSP_Komanda32_API.Models;

namespace PSP_Komanda32_API.Extensions
{
    public static class OrdersExtensions
    {
        public static GetOrdersDTO ToGetOrdersDTO(this Orders order)
            => new GetOrdersDTO
            {
                Id = order.id,
                EmployeeId = order.EmployeeId,
                CustomerId = order.CustomerId,
                Date = order.Date,
                Payment = order.Payment,
                IsPaid = order.IsPaid,
                Comment = order.Comment,
                IsAccepted = order.IsAccepted,
                DeclineReason = order.DeclineReason,
                DeliveryAddressId = order.DeliveryAddressId,
            };
        public static GetOrdersDTO ToGetOrdersDTO(this Orders order, IQueryable<OrderProducts> orderProducts)
        {
            var orderDTO = order.ToGetOrdersDTO();
            orderDTO.ProductServices = orderProducts.Select(op =>
                    new ProductService
                    {
                        id = op.ProductService.id,
                        Name = op.ProductService.Name,
                        Description = op.ProductService.Description,
                        CostInCents = op.CostInCents,
                        BusinessId = op.ProductService.BusinessId
                    })
                .ToList();
            return orderDTO;
        }

        public static GetOrdersDTO ToGetOrdersDTO(this Orders order, List<ProductService> productServices)
        {
            var orderDTO = order.ToGetOrdersDTO();
            orderDTO.ProductServices = productServices;
            return orderDTO;
        }
        public static OrdersSummaryDTO ToOrdersSummaryDTO(this Orders order, IQueryable<OrderProducts> orderProducts)
            => new OrdersSummaryDTO
            {
                id = order.id,
                EmployeeId = order.EmployeeId,
                CustomerId = order.CustomerId,
                Date = order.Date,
                Payment = order.Payment,
                IsPaid = order.IsPaid,
                Comment = order.Comment,
                IsAccepted = order.IsAccepted,
                DeclineReason = order.DeclineReason,
                TotalCostInCents = orderProducts.Sum(op => op.CostInCents),
            };
        public static Orders ToOrders(this CreateOrdersDTO createOrdersDTO)
            => new Orders
            {
                EmployeeId = createOrdersDTO.EmployeeId,
                CustomerId = createOrdersDTO.CustomerId,
                Date = createOrdersDTO.Date,
                Payment = createOrdersDTO.Payment,
                IsPaid = createOrdersDTO.IsPaid,
                Comment = createOrdersDTO.Comment,
                IsAccepted = createOrdersDTO.IsAccepted,
                DeclineReason = createOrdersDTO.DeclineReason,
                DeliveryAddressId = createOrdersDTO.DeliveryAddressId,
            };

        public static OrderProducts ToOrderProducts(this ProductService productService)
            => new OrderProducts
            {
                ProductServiceId = productService.id,
                CostInCents = productService.CostInCents
            };
        public static Orders ToOrders(this CreateOrdersDTO createOrdersDTO, List<ProductService> productServices)
        {
            var order = createOrdersDTO.ToOrders();
            order.OrderProducts = productServices.Select(ps => ps.ToOrderProducts()).ToList();
            return order;
        }
    }
}