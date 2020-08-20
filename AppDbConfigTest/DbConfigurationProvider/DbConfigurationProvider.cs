using AppDbConfigTest.Data;
using AppDbConfigTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDbConfigTest.DbConfigurationProvider
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        public DbConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            OptionsAction(builder);

            using (var dbContext = new ApplicationDbContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();

                Data = !dbContext.AppVals.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : dbContext.AppVals.ToDictionary(c => c.Title, c => c.Value);
            }
        }

        private static IDictionary<string, string> CreateAndSaveDefaultValues(
            ApplicationDbContext dbContext)
        {
            // Quotes (c)2005 Universal Pictures: Serenity
            // https://www.uphe.com/movies/serenity
            var configValues =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                { "quote1", "I aim to misbehave." },
                { "quote2", "I swallowed a bug." },
                { "quote3", "You can't stop the signal, Mal." }
                };

            dbContext.AppVals.AddRange(configValues
                .Select(kvp => new AppVal
                {
                    Title = kvp.Key,
                    Value = kvp.Value
                })
                .ToArray());

            dbContext.SaveChanges();

            return configValues;
        }
    }
}
