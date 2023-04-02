using Q2.Hubs;
using Q2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Products/Index", ""));
builder.Services.AddSession();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddSqlServer<PRN_Spr23_B1Context>(builder.Configuration.GetConnectionString("SqlServerConnection"));
builder.Services.AddScoped<PRN_Spr23_B1Context>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/", () => "Hello World!");
app.MapHub<EmployeeHub>("/employeeHub");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapRazorPages();
app.Run();