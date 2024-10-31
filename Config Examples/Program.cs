using Config_Examples;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<weatherapioptions>(builder.Configuration.GetSection("weatherApi"));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.Map("/config",async (HttpContext context) => {
    // method 1
    await context.Response.WriteAsync(app.Configuration["MyValue"]+"\n"); 
    // method 2 in getvalue as generic type need to pass return type.
    await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyValue")+"\n");
    // benefit of method 2, if key dont exists in appsettings.json
    // then we can set default value.
    await context.Response.WriteAsync(app.Configuration.GetValue<int>("MyIntValue", 40) + "\n");
    // above code will return 40 coz MyIntValue is not present in appsettings.json
});

app.Map("/mutiConfig",async (HttpContext context) => {
    var apisession = app.Configuration.GetSection("weatherApi");
    await context.Response.WriteAsync(apisession["key"]+"\n");
    await context.Response.WriteAsync(apisession["Secret_Key"]);
});
app.MapControllers();


app.Run();
