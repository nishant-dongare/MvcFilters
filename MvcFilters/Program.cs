using MvcFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<ExceptionFilter>(); // Add this line to register the filter
	//options.Filters.Add<CustomAuthorizationFilter>();
	//options.Filters.Add<CustomResourceFilter>();
	options.Filters.Add<CustomActionFilter>();
	//options.Filters.Add<CustomResultFilter>();
});

// Add this line to register the filter as a service
builder.Services.AddScoped<ExceptionFilter>();
//builder.Services.AddScoped<CustomAuthorizationFilter>();
//builder.Services.AddScoped<CustomResourceFilter>();
builder.Services.AddScoped<CustomActionFilter>();
//builder.Services.AddScoped<CustomResultFilter>();

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
