var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register exception filter types for ServiceFilter usage
builder.Services.AddScoped<laba3.Filters.HandleErrorAttribute>();
builder.Services.AddScoped<laba3.Filters.FormatExceptionFilterAttribute>();
builder.Services.AddScoped<laba3.Filters.PostIndexActionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
