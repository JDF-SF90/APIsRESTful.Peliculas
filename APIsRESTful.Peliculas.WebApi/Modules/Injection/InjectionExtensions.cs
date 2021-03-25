using APIsRESTful.Peliculas.Application;
using APIsRESTful.Peliculas.DataAccess;
using APIsRESTful.Peliculas.Domain;
using APIsRESTful.Peliculas.IApplication;
using APIsRESTful.Peliculas.IDataAccess;
using APIsRESTful.Peliculas.IDomain;
using APIsRESTful.Peliculas.IInfrastructure;
using APIsRESTful.Peliculas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIsRESTful.Peliculas.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PeliculasConnection")));
            services.AddScoped<IGenerosApplication, GenerosApplication>();
            services.AddScoped<IGenerosDomain, GenerosDomain>();
            services.AddScoped<IGenerosRepository, GenerosRepository>();

            services.AddScoped<IActoresApplication, ActoresApplication>();
            services.AddScoped<IActoresDomain, ActoresDomain>();
            services.AddScoped<IActoresRepository, ActoresRepository>();

            services.AddScoped<IPeliculasApplication, PeliculasApplication>();
            services.AddScoped<IPeliculasDomain, PeliculasDomain>();
            services.AddScoped<IPeliculasRepository, PeliculasRepository>();

            services.AddTransient<IAlmacenadorArhivos, AlmacenadorArchivosLocal>();
            services.AddHttpContextAccessor();

     


            return services;
        }
    }
}
