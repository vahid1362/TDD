using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Application.Contracts;
using TDD.Application;
using TDD.Domain.Infrastructure;
using TDD.Infrastructure.Repositories;
using TDD.Infrastructure;

namespace TDD.API.Integeration.Test
{
    public  class TestServer:WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("https_port", "443");
            builder.ConfigureAppConfiguration((_, config) =>
            {
                var root = Directory.GetCurrentDirectory();
                var fileProvider = new PhysicalFileProvider(root);
                config.AddJsonFile(fileProvider, "testsettings.json", false, false);
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddDbContext<ApplicationContext>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
                services.AddScoped<ICategoryService, CategoryService>();

            });
        }
    }
}
