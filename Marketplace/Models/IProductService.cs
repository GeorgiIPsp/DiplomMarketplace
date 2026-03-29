using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        System.Threading.Tasks.Task AddToCartAsync(string byerEmail, int presentCardId, int quantity);
        Task<List<CartItem>> GetUserCartItemsAsync(string byerEmail);
        System.Threading.Tasks.Task RemoveFromCartAsync(int cartItemId);
        System.Threading.Tasks.Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
        System.Threading.Tasks.Task ClearUserCartAsync(string byerEmail);
        Task<int> GetCartItemsCountAsync(string byerEmail);
    }

    public class ProductService : IProductService
    {
        private readonly AutoSystemForMarketplaceContext _context;

        public ProductService(AutoSystemForMarketplaceContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var presentCards = await _context.PresentCards
                    .Include(p => p.ProductPrices)
                    .Include(p => p.Seller)
                    .Where(p => p.IsAvailable == true)
                    .ToListAsync();

                var products = presentCards.Select(pc => new Product
                {
                    Id = pc.PresentCardId,
                    ImageUrl = pc.Images ?? "/images/default.jpg",
                    Name = pc.Name,
                    Price = pc.ProductPrices?.FirstOrDefault()?.Price.ToString() ?? "0",
                    Description = pc.Description ?? ""
                }).ToList();

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки товаров: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var presentCard = await _context.PresentCards
                    .Include(p => p.ProductPrices)
                    .Include(p => p.Seller)
                    .FirstOrDefaultAsync(p => p.PresentCardId == id);

                if (presentCard == null)
                    return null;

                return new Product
                {
                    Id = presentCard.PresentCardId,
                    ImageUrl = presentCard.Images ?? "/images/default.jpg",
                    Name = presentCard.Name,
                    Price = presentCard.ProductPrices?.FirstOrDefault()?.Price.ToString() ?? "0",
                    Description = presentCard.Description ?? ""
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки товара: {ex.Message}");
                return null;
            }
        }

        public async System.Threading.Tasks.Task AddToCartAsync(string byerEmail, int presentCardId, int quantity)
        {
            try
            {
                // Проверяем, существует ли пользователь
                var byer = await _context.Byers.FirstOrDefaultAsync(b => b.Email == byerEmail);
                if (byer == null)
                {
                    throw new Exception("Пользователь не найден");
                }

                // Проверяем, существует ли товар
                var presentCard = await _context.PresentCards.FindAsync(presentCardId);
                if (presentCard == null)
                {
                    throw new Exception("Товар не найден");
                }

                // Проверяем, есть ли уже такой товар в корзине
                var existingCartItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.ByerId == byerEmail && ci.PresentCardId == presentCardId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                    _context.CartItems.Update(existingCartItem);
                }
                else
                {
                    var newCartItem = new CartItem
                    {
                        ByerId = byerEmail,
                        PresentCardId = presentCardId,
                        Quantity = quantity
                    };
                    await _context.CartItems.AddAsync(newCartItem);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка добавления в корзину: {ex.Message}");
                throw;
            }
        }

        public async Task<List<CartItem>> GetUserCartItemsAsync(string byerEmail)
        {
            try
            {
                var cartItems = await _context.CartItems
                    .Include(ci => ci.PresentCard)
                        .ThenInclude(pc => pc.ProductPrices)
                    .Where(ci => ci.ByerId == byerEmail)
                    .ToListAsync();

                return cartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки корзины: {ex.Message}");
                return new List<CartItem>();
            }
        }

        public async System.Threading.Tasks.Task RemoveFromCartAsync(int cartItemId)
        {
            try
            {
                // Сначала проверяем, есть ли связанные OrderItem
                var orderItems = await _context.OrderItems
                    .Where(oi => oi.CartItemId == cartItemId)
                    .ToListAsync();

                // Если есть связанные заказы, удаляем их
                if (orderItems.Any())
                {
                    _context.OrderItems.RemoveRange(orderItems);
                    await _context.SaveChangesAsync(); // Добавьте await
                }

                // Затем удаляем сам CartItem
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка удаления из корзины: {ex.Message}");
                throw;
            }
        }

        public async System.Threading.Tasks.Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem != null)
                {
                    if (quantity <= 0)
                    {
                        // Используем RemoveFromCartAsync для правильного удаления
                        await RemoveFromCartAsync(cartItemId);
                    }
                    else
                    {
                        cartItem.Quantity = quantity;
                        _context.CartItems.Update(cartItem);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обновления количества: {ex.Message}");
                throw;
            }
        }

        public async System.Threading.Tasks.Task ClearUserCartAsync(string byerEmail)
        {
            try
            {
                // Получаем все CartItem пользователя
                var cartItems = await _context.CartItems
                    .Where(ci => ci.ByerId == byerEmail)
                    .ToListAsync();

                if (cartItems.Any())
                {
                    // Удаляем ВСЕ CartItem пользователя
                    _context.CartItems.RemoveRange(cartItems);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Удалено {cartItems.Count} товаров из корзины");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка очистки корзины: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetCartItemsCountAsync(string byerEmail)
        {
            try
            {
                var count = await _context.CartItems
                    .Where(ci => ci.ByerId == byerEmail)
                    .SumAsync(ci => ci.Quantity);

                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подсчета товаров: {ex.Message}");
                return 0;
            }
        }
    }
}