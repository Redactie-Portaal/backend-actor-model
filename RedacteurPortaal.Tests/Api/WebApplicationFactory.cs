using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RedacteurPortaal.Data.Context;

namespace RedacteurPortaal.Tests.Api;

class RedacteurPortaalApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();
        Environment.SetEnvironmentVariable("InTest", "IsTrue");
        builder.ConfigureServices(services => {
            services.RemoveAll(typeof(DbContextOptions<DataContext>));
            services.RemoveAll(typeof(DataContext));
            services.RemoveAll<DbContextOptions<DataContext>>();
            services.AddDbContext<DataContext>(options => {
                options.UseInMemoryDatabase("Testing", root);
                options.ConfigureWarnings(x => {
                    x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning);
                });
            });
        });

        return base.CreateHost(builder);
    }
}