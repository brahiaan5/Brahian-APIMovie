using API.Movies.DAL;
using API.Movies.MoviesMapper;
using API.Movies.Repository;
using API.Movies.Repository.IRepository;
using API.Movies.Services;
using API.Movies.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddAutoMapper(x => x.AddProfile<Mappers>());

// Dependency Injection for Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMovieService, MovieService>();

// Dependency Injection for Repositories
builder.Services.AddScoped<ICaregoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
