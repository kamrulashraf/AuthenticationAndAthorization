var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", config =>
    {
        config.Cookie.Name = "Testing.cookie";
        config.LoginPath = "/Home/login";
    });

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoinst =>
{
    endpoinst.MapDefaultControllerRoute();
}); 

app.Run();
