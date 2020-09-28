using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TennisBookings.Web.Configuration.Custom
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEFConfiguration(this IConfigurationBuilder builder, 
            Action<DbContextOptionsBuilder> optionsAction)
        {
            return builder.Add(new EntityFrameworkConfigurationSource(optionsAction));
        }
    }
}
