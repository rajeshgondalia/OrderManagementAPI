using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderManagementServices.Models;
using OrderManagementServices.OrderInterface;
using OrderManagementServices.OrderServices;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<OrderManagementContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("OrderManagementConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomer_Repository, Customer_Repository>();
builder.Services.AddScoped<IOrder_Repository, Order_Repository>();  

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
