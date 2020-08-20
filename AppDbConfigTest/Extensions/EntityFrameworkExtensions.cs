using AppDbConfigTest.DbConfigurationProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDbConfigTest.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddDbConfiguration(
            this IConfigurationBuilder builder,
            Action<DbContextOptionsBuilder> optionsAction)
        {
            return builder.Add(new DbConfigurationSource(optionsAction));
        }
    }
}
