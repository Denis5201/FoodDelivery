using FoodDelivery.Models;
using FoodDelivery.Services.Authorization;
using FoodDelivery.Services.Exceptions;
using FoodDelivery.Services.Implementation;
using FoodDelivery.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IDishRatingService, DishRatingService>();
builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddHostedService<BackgroundTokenCleaningService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthorizationHandler, StillWorkingTokenHandler>();

//Auth JwtBearer
builder.Services.AddAuthorization(options =>
    options.AddPolicy("StillWorkingToken", policy => 
        policy.AddRequirements(new StillWorkingTokenRequirement())
        .RequireAuthenticatedUser()));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtConfigs.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtConfigs.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = JwtConfigs.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
