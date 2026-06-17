using Microsoft.AspNetCore.Mvc;
using IoTProject.Infrastructure;
using IoTProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using IoTProject.App.DTOs;
using IoTProject.App.Interfaces;
using IoTProject.Domain.Enums;

using BCrypt;

namespace IoTProject.App.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Register([FromBody] RegisterRequest data)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(data.Password);

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == data.Email);

            if (existingUser != null)
            {
                throw new Exception(
                    "User already exists");
            }

            var user = new User
            {
                Name = data.Name,
                Email = data.Email,
                Password = hashed
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<string> Login([FromBody] LoginRequest data)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == data.Email);

            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(
                    data.Password,
                    user.Password);

            if (!validPassword)
            {
                throw new Exception("Invalid credentials");
            }
            
            return "login success";
        }
    }
}
