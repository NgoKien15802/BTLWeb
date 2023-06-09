﻿using codeBTL.Models;
using codeBTL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// tắt chức năng tự trả về lỗi để sử dụng do customize
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // tắt bỏ 
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddDbContext<DtddContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DtddContext")));
builder.Services.AddSession();
builder.Services.AddScoped<IBrandMenuRepository, BrandMenuRepository>();
builder.Services.AddScoped<ITypeMenuRepository, TypeMenuRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
   name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
