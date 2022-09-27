using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;  
using System.Reflection;

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AngularDotNetContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred creating the DB.");
        }
    }
}

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up the Host!");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DapperContext>(); 
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>(); 
builder.Services.AddControllers();
builder.Services.AddDbContext<AngularDotNetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EFConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "angular-dotnet",
        Description = "An ASP.NET Core Web API + Angular",
        TermsOfService = new Uri("https://example.com/terms"),
    });

    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
    options.IncludeXmlComments(Path.Combine(assemblyFolder, fileName));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    CreateDbIfNotExists(app);
    app.UseSwagger();
    app.UseSwaggerUI();
} 
else {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();


