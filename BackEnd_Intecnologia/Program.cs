using BackEnd_Intecnologia.Models;
using BackEnd_Intecnologia.Services;
using DotNetify;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var apikey = builder.Configuration["ConnectionStrings:BACKConnection"];
builder.Services.AddDbContext<IntecContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("BACKConnection")));
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAssignStandServices, StandServices>();
builder.Services.AddScoped<IMessageServices, MessageServices>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
