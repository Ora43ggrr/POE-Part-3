using POE_Part_3.Services; // Import services namespace

var builder = WebApplication.CreateBuilder(args); // Creates the web application builder

// Add services to the container.
builder.Services.AddControllersWithViews(); // Add MVC services
builder.Services.AddSingleton<DataService>(); // Register data service as singleton
builder.Services.AddSession(options => // Add session services
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Make cookie accessible only by server
    options.Cookie.IsEssential = true; // Mark cookie as essential
    options.Cookie.Name = "CMCS.Session"; // Session cookie name
});

var app = builder.Build(); // Builds the application

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) // Checks if not in development environment
{
    app.UseExceptionHandler("/Home/Error"); // Uses custom error handler
    app.UseHsts(); // Enables HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS
app.UseStaticFiles(); // Enable static file serving
app.UseRouting(); // Adds routing middleware
app.UseSession(); // Enable session middleware
app.UseAuthorization(); // Adds authorization middleware

app.MapControllerRoute( // Configures default route
    name: "default", // Route name
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Runs the application