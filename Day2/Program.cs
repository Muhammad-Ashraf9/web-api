
using System.Text;
using Day2.GenericRepos;
using Day2.Models;
using Day2.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Day2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string corsTxt = "";

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v2",
                    Title = "Ash Web API",
                    Description = "Ash web api for students and departments",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ash",
                        Email = "muhammad.ashraf.tahaa@gmail.com",
                    }
                });
                op.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,"xmldoc.xml"));
                op.EnableAnnotations();
                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                op.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddDbContext<ITIContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<UnitWork>();

            builder.Services.AddAuthentication(op => op.DefaultAuthenticateScheme = "AshSechema").AddJwtBearer(
                "AshSechema",
                options =>
                {
                    string secret = builder.Configuration.GetSection("SecretKey").Value;
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });

            builder.Services.AddCors(op =>
            {
                op.AddPolicy(corsTxt, builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                    );
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(corsTxt);
            app.MapControllers();

            app.Run();
        }
    }
}
