using System.Text;
using System.Text.Json;
using AuthBlog.Data;
using AuthBlog.Repositories;
using AuthBlog.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
    // Configurando mensagem de erro padrão para não autorizado (mensagem customizada)
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var errorMessage = new { error = "User Unauthorized", details = "You must provide a valid token to access this resource" };
            var jsonErroMessage = JsonSerializer.Serialize(errorMessage);
            return context.Response.WriteAsync(jsonErroMessage);
        }
    };
});

// Configuração de autorização/permisão
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adimin", policy => policy.RequireClaim("role", "admin"));
    options.AddPolicy("user", policy => policy.RequireClaim("role", "user"));
    options.AddPolicy("adminOrUser", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("role", "admin") || context.User.HasClaim("role", "user")));
    options.AddPolicy("editor", policy => policy.RequireClaim("role", "editor"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
