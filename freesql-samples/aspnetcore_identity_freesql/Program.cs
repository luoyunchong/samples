using aspnetcore_identity_freesql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFreeSqlIdentity(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//自定义Scope 的Serivce 执行 DbContext中的OnModelCreating
app.Services.RunScopeService();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
