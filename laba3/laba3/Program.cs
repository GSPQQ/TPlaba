var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<laba3.Filters.HandleErrorAttribute>();
builder.Services.AddScoped<laba3.Filters.FormatExceptionFilterAttribute>();
builder.Services.AddScoped<laba3.Filters.PostIndexActionFilter>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Simple}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
