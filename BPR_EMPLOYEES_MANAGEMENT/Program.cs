

using BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Llamar al método de extensión para registrar todos los servicios
builder.Services.AddServices(builder.Configuration);

//builder.Services.AddControllers();
// Agregar Swagger y demás configuraciones
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Llamar al método de extensión que configura el uso de la app
app.AddUses();

app.Run();
