using Ecom.Client.Core.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddRazorRuntimeCompilation().AddNToastNotifyToastr(
    new ToastrOptions
    {
        TimeOut = 5000,
        NewestOnTop = true,
        CloseButton = true,
        CloseDuration = true,
        ProgressBar = true,
        PositionClass = ToastPositions.TopRight
    });

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddScoped<IGenericHttpClient, GenericHttpClient>();

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(o =>
{
    o.Cookie.Name = "My_Auth_cookie";
    o.SlidingExpiration = true;
    o.ExpireTimeSpan = TimeSpan.FromHours(2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();