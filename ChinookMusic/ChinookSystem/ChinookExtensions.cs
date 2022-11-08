using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.BLL;
#endregion

namespace ChinookSystem
{
    // your class needs to be public so it can be used outisde of this project
    public static class ChinookExtensions
    {
        // method name can be anything, it must match the builder.Services.xxx(options => ..) statement in your Program.cs
        // the first parameter is the class that you are attempting to extend
        // the second parameter is the options value in your call statement
        // it is receiving the connectionString for your application
        public static void ChinookSystemBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            // register the DbContext class with the service collection
            services.AddDbContext<ChinookContext>(options);

            // add any services that you create in the class library using .AddTransient<serviceclassname>(...);

            services.AddTransient<TrackServices>((serviceProvider) =>
            {
                // retrieve the registered DbContext done with AddDbContext
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new TrackServices(context);
            });

            services.AddTransient<PlaylistTrackServices>((serviceProvider) =>
            {
                // retrieve the registered DbContext done with AddDbContext
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new PlaylistTrackServices(context);
            });
        }
    }
}
