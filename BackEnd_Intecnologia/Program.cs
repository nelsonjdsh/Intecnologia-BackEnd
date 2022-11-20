using BackEnd_Intecnologia.Helpers;
using BackEnd_Intecnologia.Models;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var misReglasCors = "ReglasCors";
builder.Services.AddCors(option =>
	option.AddPolicy(name: misReglasCors,
		builder =>
		{
			builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
		}
	)
);

builder.Services.AddScoped<JWTService>();
builder.Configuration.AddJsonFile("appsettings.json");
var secretkey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretkey);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd_Intecnologia", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var apikey = builder.Configuration["ConnectionStrings:BACKConnection"];
builder.Services.AddDbContext<IntecContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("BACKConnection")));
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IStandServices, StandServices>();
builder.Services.AddScoped<IMessageServices, MessageServices>();
builder.Services.AddScoped<IActivityServices, ActivityServices>();


var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
	app.UseSwaggerUI();
//}
app.UseCors(misReglasCors);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
