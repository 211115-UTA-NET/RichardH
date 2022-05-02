using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

var builder = WebApplication.CreateBuilder(args);

string ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
{
    opt.UseSqlServer(ConnectionString);

    //opt.UseInMemoryDatabase("TodoList");
});


builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new() { Title = "ToDoApi", Version = "v1" });
 });

var app = builder.Build();

//builder.Services.AddCors(opt =>
//{
//    opt.AddDefaultPolicy(builder =>
//        builder
//            .AllowAnyMethod()
//            .AllowAnyHeader()
//            .AllowAnyOrigin()
//            .AllowCredentials());
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
