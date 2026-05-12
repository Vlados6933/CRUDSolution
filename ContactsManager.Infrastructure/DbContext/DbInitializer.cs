using ContactsManager.Core.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ContactsManager.Infrastructure.DbContext
{
    public static class DbInitializer
    {
        public static async Task SeedDataAsync(ApplicationDbContext db, IWebHostEnvironment env)
        {
            await db.Database.MigrateAsync();

            if (!await db.Countries.AnyAsync())
            {
                string countriesJson = await File.ReadAllTextAsync(Path.Combine(env.ContentRootPath, "countries.json"));
                List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);

                if(countries!= null && countries.Count> 0)
                {
                    await db.Countries.AddRangeAsync(countries);
                    await db.SaveChangesAsync();
                }
            }

            if (!await db.Persons.AnyAsync())
            {
                string personsJson = await File.ReadAllTextAsync(Path.Combine(env.ContentRootPath, "persons.json"));
                List<Person>? persons = JsonSerializer.Deserialize<List<Person>>(personsJson);

                if (persons != null && persons.Count > 0)
                {
                    await db.Persons.AddRangeAsync(persons);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
