using Azure.Core;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Persistence.Seed
{
    internal class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.AppUsers.AnyAsync())
                return;

            //var filePath = Path.Combine("Seed", "UserSeedData.json");
            //var usersData = await File.ReadAllTextAsync(filePath);

            var resourceName = "Persistence.Seed.UserSeedData.json";

            using var stream = typeof(Seed).Assembly.GetManifestResourceStream(resourceName);
            if(stream is null)
                throw new FileNotFoundException($"Embeded Resource {resourceName} not found.");

            using var reader = new StreamReader(stream);
            var usersData = await reader.ReadToEndAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            options.Converters.Add(new JsonStringEnumConverter());  // by default System.Text.Json does not support deserializing enums from strings

            var users = JsonSerializer.Deserialize<List<AppUser>>(usersData, options);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
                user.PasswordSalt = hmac.Key;
            }

            // An exeption will be thrown if the entities are added to the DbSet before the SaveChangesAsync is called in case
            // of overriding the savechanges to populate the ids, it won't work here because the savechanges or interceptor logic is not being applied before the entities are added to the DbSet
            context.AddRange(users);

            await context.SaveChangesAsync();
        }
    }
}
