using Microsoft.EntityFrameworkCore;
using GrowthPath.FeedbackAPI.Data;
using GrowthPath.FeedbackAPI.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Web.Http.Cors;


namespace GrowthPath.FeedbackAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FeedbackDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();


            builder.Services.AddControllers();

       //     builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       //.AddJwtBearer(options =>
       //{
       //    options.TokenValidationParameters = new TokenValidationParameters
       //    {
       //        ValidateIssuer = true,
       //        ValidateAudience = true,
       //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
       //        ValidAudience = builder.Configuration["Jwt:Audience"],
       //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       //    };
       //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
           // app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
