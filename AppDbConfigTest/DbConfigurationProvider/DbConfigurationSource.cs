﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDbConfigTest.DbConfigurationProvider
{
    public class DbConfigurationSource: IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        public DbConfigurationSource(Action<DbContextOptionsBuilder> optionsAction)
        {
            _optionsAction = optionsAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(_optionsAction);
        }
    }
}
