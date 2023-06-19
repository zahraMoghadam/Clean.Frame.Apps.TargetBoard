using Clean.Frame.Apps.TargetBoard.Configurations;
using Clean.Frame.Apps.TargetBoard.Services.NativeInjectorBootStrapper;
using Clean.Frame.Apps.TargetBoard.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(x => x.ResourcesPath = "Resources");

//----- Database -----
builder.Services.AddCustomizedDatabase(builder.Configuration);

//---Services Repository--
NativeInjectorBootStrapper.RegisterServices(builder.Services);

//----- AutoMapper -----
builder.Services.AddAutoMapperSetup();

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
