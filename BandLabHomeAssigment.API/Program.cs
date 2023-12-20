using BandLabHomeAssigment.Infrastructure;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BandLabHomeAssigment.Application.Commands.CreateUser;
using BandLabHomeAssigment.Domain.Repositories;
using BandLabHomeAssigment.Infrastructure.Repositories;
using BandLabHomeAssigment.Application.Commands.CreatePost;
using Azure.Storage.Blobs;
using BandLabHomeAssigment.API.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICreateUser, CreateUser>();

builder.Services.AddScoped(_ => {
    return new BlobServiceClient(builder.Configuration.GetConnectionString("BlobContainer"));
});
builder.Services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICreatePost, CreatePost>();
builder.Services.AddScoped<IPostQueries, PostQueries>();


var connString = builder.Configuration.GetConnectionString("DbConnectionString");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
