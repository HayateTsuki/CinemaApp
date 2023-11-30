using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Cinema.Core.Extensions;
using Cinema.Core.Filters;
using Cinema.Domain;
using Cinema.Domain.Data.Context;
using Cinema.Domain.Data.Entities;
using Cinema.Domain.Data.Seed;
using Cinema.Domain.Models;
using Cinema.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Cinema.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            configuration.AddConfiguredLogger();
            services.AddMediatR(typeof(DomainModule));
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema API", Version = "v1" });
                c.OperationFilter<SwaggerAddAuthHeaderOperationFilter>();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "`Token only!!!` - without `Bearer_` prefix",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            var sqlConnectionString = configuration.GetSection("DbConnectionString").Value;
            services.AddDbContext<CinemaContext>(
                optionsBuilder =>
                {
                    optionsBuilder.UseNpgsql(sqlConnectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", "cinema").CommandTimeout(600));
                });
            services.AddIdentity<CinemaUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddRoleStore<RoleStore<IdentityRole<int>, CinemaContext, int>>()
                .AddUserStore<UserStore<CinemaUser, IdentityRole<int>, CinemaContext, int>>()
                .AddEntityFrameworkStores<CinemaContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddScoped<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUserContext, UserContext>(uc =>
            {
                var claims = uc.GetService<IActionContextAccessor>()?.ActionContext?.HttpContext.User.Claims.ToList();
                if (claims is not null && claims.Any())
                {
                    var id = claims.Single(x => x.Type == "id").Value;
                    var name = claims.Single(x => x.Type == ClaimTypes.GivenName).Value;
                    var email = claims.Single(x => x.Type == ClaimTypes.Email).Value;
                    return new UserContext(int.Parse(id), name, email);
                }

                return new UserContext(0, string.Empty, string.Empty);
            });

            var jwtSettings = services.RegisterSettings<JwtSettings>(configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.SaveToken = true;
                });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new DomainModule());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema.Api v1"));
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            loggerFactory.AddSerilog();
            ConfigureAsync(app).Wait();
        }

        public async Task ConfigureAsync(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<CinemaContext>();
            await context.Database.MigrateAsync().ConfigureAwait(false);
            var seeder = serviceScope.ServiceProvider.GetService<CinemaContextDataSeeder>();
            await seeder.SeedAsync().ConfigureAwait(false);
        }
    }
}