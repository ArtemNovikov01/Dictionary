
using MetallAlloy.Models;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddControllers();
var app = builder.Build();



app.UseStaticFiles();

app.UseRouting();
app.MapControllers();


app.Map("/", async context => {
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/home.html");
});

app.Run();
