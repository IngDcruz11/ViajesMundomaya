using ViajesMundoMaya.Utilidades.Configuracion;



var builder = WebApplication.CreateBuilder(args);



ConexionSQL.CadenaConexion =
	builder.Configuration.GetConnectionString("ConexionSQL")
	?? throw new InvalidOperationException(
		"No se encontró la cadena de conexión ConexionSQL."
	);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint(
			"/openapi/v1.json",
			"Viajes Mundo Maya API v1"
		);
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
