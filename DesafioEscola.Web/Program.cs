using DesafioEscola.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

InjectionDependence.Configuration(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<MiddlewareGlobalException>();

app.Run();
