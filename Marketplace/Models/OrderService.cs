using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Marketplace.Models;

namespace yyy.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string byerEmail, List<CartItem> cartItems, decimal totalAmount);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetUserOrdersAsync(string byerEmail);
        Task<List<OrderWithStatus>> GetCompletedOrdersAsync(string byerEmail);
        Task<List<OrderWithStatus>> GetCurrentOrdersAsync(string byerEmail);
        Task<OrderWithStatus> GetOrderWithStatusAsync(int orderId);
        Task<string> GetCurrentOrderStatusAsync(int orderId);
        Task<bool> CancelOrderAsync(int orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly AutoSystemForMarketplaceContext _context;
        private readonly IProductService _productService;

        public OrderService(AutoSystemForMarketplaceContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<int> CreateOrderAsync(string byerEmail, List<CartItem> cartItems, decimal totalAmount)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Создаем заказ
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    ByerId = byerEmail
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                // 2. Для каждого товара в корзине создаем НОВЫЙ CartItem (копию) для заказа
                foreach (var cartItem in cartItems)
                {
                    // Создаем новый CartItem для заказа (копируем данные)
                    var newCartItem = new CartItem
                    {
                        ByerId = byerEmail,
                        PresentCardId = cartItem.PresentCardId,
                        Quantity = cartItem.Quantity
                    };

                    await _context.CartItems.AddAsync(newCartItem);
                    await _context.SaveChangesAsync();

                    // Создаем OrderItem, связанный с новым CartItem
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        CartItemId = newCartItem.CartItemId,
                        StatusBoughtOut = "pending"
                    };
                    await _context.OrderItems.AddAsync(orderItem);
                }

                // 3. Добавляем начальный статус "Создан"
                var createdStatus = await _context.DictionaryStatusHistories
                    .FirstOrDefaultAsync(d => d.StatusName == "Создан");

                if (createdStatus != null)
                {
                    var statusHistory = new StatusHistoryOrder
                    {
                        OrderId = order.OrderId,
                        DictionaryStatusHistoryId = createdStatus.DictionaryStatusHistoryId,
                        DataEdit = DateTime.Now
                    };
                    await _context.StatusHistoryOrders.AddAsync(statusHistory);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order.OrderId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Ошибка создания заказа: {ex.Message}");
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CartItem)
                        .ThenInclude(ci => ci.PresentCard)
                            .ThenInclude(pc => pc.ProductPrices)
                .Include(o => o.StatusHistoryOrders)
                    .ThenInclude(sh => sh.DictionaryStatusHistory)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<Order>> GetUserOrdersAsync(string byerEmail)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CartItem)
                        .ThenInclude(ci => ci.PresentCard)
                            .ThenInclude(pc => pc.ProductPrices)
                .Include(o => o.StatusHistoryOrders)
                    .ThenInclude(sh => sh.DictionaryStatusHistory)
                .Where(o => o.ByerId == byerEmail)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<OrderWithStatus>> GetCompletedOrdersAsync(string byerEmail)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CartItem)
                        .ThenInclude(ci => ci.PresentCard)
                            .ThenInclude(pc => pc.ProductPrices)
                .Include(o => o.StatusHistoryOrders)
                    .ThenInclude(sh => sh.DictionaryStatusHistory)
                .Where(o => o.ByerId == byerEmail)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var result = new List<OrderWithStatus>();
            var allProducts = await _productService.GetProductsAsync();

            foreach (var order in orders)
            {
                var lastStatus = order.StatusHistoryOrders
                    .OrderByDescending(sh => sh.DataEdit)
                    .FirstOrDefault();

                // Проверяем, завершен ли заказ
                if (lastStatus != null &&
                    (lastStatus.DictionaryStatusHistory.StatusName == "Завершен" ||
                     lastStatus.DictionaryStatusHistory.StatusName == "Доставлен"))
                {
                    var orderWithStatus = await ConvertToOrderWithStatus(order, allProducts, lastStatus);
                    result.Add(orderWithStatus);
                }
            }

            return result;
        }

        public async Task<List<OrderWithStatus>> GetCurrentOrdersAsync(string byerEmail)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CartItem)
                        .ThenInclude(ci => ci.PresentCard)
                            .ThenInclude(pc => pc.ProductPrices)
                .Include(o => o.StatusHistoryOrders)
                    .ThenInclude(sh => sh.DictionaryStatusHistory)
                .Where(o => o.ByerId == byerEmail)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var result = new List<OrderWithStatus>();
            var allProducts = await _productService.GetProductsAsync();

            foreach (var order in orders)
            {
                var lastStatus = order.StatusHistoryOrders
                    .OrderByDescending(sh => sh.DataEdit)
                    .FirstOrDefault();

                // Проверяем, активен ли заказ (не завершен и не отменен)
                if (lastStatus != null &&
                    lastStatus.DictionaryStatusHistory.StatusName != "Завершен" &&
                    lastStatus.DictionaryStatusHistory.StatusName != "Доставлен" &&
                    lastStatus.DictionaryStatusHistory.StatusName != "Отменен")
                {
                    var orderWithStatus = await ConvertToOrderWithStatus(order, allProducts, lastStatus);
                    result.Add(orderWithStatus);
                }
            }

            return result;
        }

        public async Task<OrderWithStatus> GetOrderWithStatusAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CartItem)
                        .ThenInclude(ci => ci.PresentCard)
                            .ThenInclude(pc => pc.ProductPrices)
                .Include(o => o.StatusHistoryOrders)
                    .ThenInclude(sh => sh.DictionaryStatusHistory)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return null;

            var lastStatus = order.StatusHistoryOrders
                .OrderByDescending(sh => sh.DataEdit)
                .FirstOrDefault();

            var allProducts = await _productService.GetProductsAsync();

            return await ConvertToOrderWithStatus(order, allProducts, lastStatus);
        }

        public async Task<string> GetCurrentOrderStatusAsync(int orderId)
        {
            var lastStatus = await _context.StatusHistoryOrders
                .Include(sh => sh.DictionaryStatusHistory)
                .Where(sh => sh.OrderId == orderId)
                .OrderByDescending(sh => sh.DataEdit)
                .FirstOrDefaultAsync();

            return lastStatus?.DictionaryStatusHistory?.StatusName ?? "Неизвестно";
        }

        public async Task<bool> CancelOrderAsync(int orderId)
        {
            try
            {
                var cancelStatus = await _context.DictionaryStatusHistories
                    .FirstOrDefaultAsync(d => d.StatusName == "Отменен");

                if (cancelStatus == null) return false;

                var statusHistory = new StatusHistoryOrder
                {
                    OrderId = orderId,
                    DictionaryStatusHistoryId = cancelStatus.DictionaryStatusHistoryId,
                    DataEdit = DateTime.Now
                };

                await _context.StatusHistoryOrders.AddAsync(statusHistory);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отмены заказа: {ex.Message}");
                return false;
            }
        }

        private async Task<OrderWithStatus> ConvertToOrderWithStatus(Order order, List<Product> allProducts, StatusHistoryOrder lastStatus)
        {
            var orderWithStatus = new OrderWithStatus
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate ?? DateTime.Now,
                TotalAmount = order.TotalAmount,
                Status = lastStatus?.DictionaryStatusHistory?.StatusName ?? "Неизвестно",
                StatusDate = lastStatus?.DataEdit,
                StatusHistory = order.StatusHistoryOrders.ToList(),
                OrderItems = order.OrderItems.ToList()
            };

            foreach (var orderItem in order.OrderItems)
            {
                if (orderItem.CartItem?.PresentCard != null)
                {
                    var product = allProducts.FirstOrDefault(p => p.Id == orderItem.CartItem.PresentCard.PresentCardId);
                    if (product != null)
                    {
                        orderWithStatus.Products.Add(product);
                        orderWithStatus.Quantities.Add(orderItem.CartItem.Quantity);
                    }
                }
            }

            return orderWithStatus;
        }
    }
}