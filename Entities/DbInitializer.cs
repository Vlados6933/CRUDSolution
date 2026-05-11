using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities
{
    public static class DbInitializer
    {
        public static async Task SeedDataAsync(ApplicationDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await db.Countries.AnyAsync())
            {
                string countriesJson = await File.ReadAllTextAsync(@"D:\prog\ContactManagementPlatformSolution\ContactManagementPlatform\countries.json");
                List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);

                if(countries!= null && countries.Count> 0)
                {
                    await db.Countries.AddRangeAsync(countries);
                    await db.SaveChangesAsync();
                }
            }

            if (!await db.Persons.AnyAsync())
            {
                string personsJson = await File.ReadAllTextAsync(@"D:\prog\ContactManagementPlatformSolution\ContactManagementPlatform\persons.json");
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
