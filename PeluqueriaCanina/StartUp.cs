using DataAccess.DAO;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

namespace PeluqueriaCanina
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-ES");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("es-ES") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("es-ES") };
            });

            services.AddSingleton<System.Globalization.CultureInfo>(new System.Globalization.CultureInfo("es-ES"));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
             {
                 c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                 {
                     Name = "Authorization",
                     Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = Microsoft.OpenApi.Models.ParameterLocation.Header

                 });

                 c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference= new OpenApiReference
                             {
                                Type= ReferenceType.SecurityScheme,
                                Id = "Bearer"
                             }
                         },
                        new string[]{ }
                     }
                 });
             });

            services.AddCors(options =>
             {
                    options.AddDefaultPolicy(b =>
                    {
                        b.WithOrigins("https://www.apirequest.io").AllowAnyMethod().AllowAnyHeader();
                    });
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "1"));
                options.AddPolicy("Employee", policy => policy.RequireClaim("Role", "2"));
                options.AddPolicy("Client", policy => policy.RequireClaim("Role", "3"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EstaEsLaClaveSecreta"))

                };
            });

            services.AddScoped<IEmployeeDAO, EmployeeDAO>();
            services.AddScoped<IUserDAO, UserDAO>();
            services.AddScoped<IClientDAO, ClientDAO>();
            services.AddScoped<ITurnDAO, TurnDAO>();
            //services.AddScoped<IDAO, DAO>();


        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
