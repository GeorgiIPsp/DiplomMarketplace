using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Models
{
    public class UserService
    {
        private readonly AutoSystemForMarketplaceContext _context;
        public Byer? currentUser { get; set; } = null;
        public List<Byer> userList { get; set; } = new();

        public UserService(AutoSystemForMarketplaceContext context)
        {
            _context = context;
        }

        public async Task<Byer> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _context.Byers
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    currentUser = new Byer
                    {
                        Email = user.Email,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Phone = user.Phone,
                        IsActive = user.IsActive
                    };
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка входа: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterAsync(string email, string password, string firstName, string lastName, string? phone = null)
        {
            try
            {
                var existingUser = await _context.Byers
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (existingUser != null)
                    return false;

                var newUser = new Byer
                {
                    Email = email,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    PersonalDiscount = 0
                };

                await _context.Byers.AddAsync(newUser);
                await _context.SaveChangesAsync();

                currentUser = new Byer
                {
                    Email = newUser.Email,
                    Password = newUser.Password,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Phone = newUser.Phone
                };

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка регистрации: {ex.Message}");
                return false;
            }
        }

        public async System.Threading.Tasks.Task LoadUserFromEmailAsync(string email)
        {
            try
            {
                var user = await _context.Byers
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    currentUser = new Byer
                    {
                        Email = user.Email,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Phone = user.Phone
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки пользователя: {ex.Message}");
            }
        }

        public void Logout()
        {
            currentUser = null;
        }
    }
}