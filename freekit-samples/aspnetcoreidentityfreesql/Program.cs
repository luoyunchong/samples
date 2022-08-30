
using aspnetcoreidentityfreesql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddFreeSqlIdentity(builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();

var app = builder.Build();

app.Services.RunScopeService();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
