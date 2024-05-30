using Infrastructure.DependencyInjection;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("WebUI");

app.UseHttpsRedirection();

app.UseAuthorization();

// MIDDLEWARE:
app.Use(async (context, next) =>
{
    try
    {
        var reqContent = new StringBuilder();
        reqContent.AppendLine("REQUEST:");
        reqContent.AppendLine($"METHOD: {context.Request.Method.ToUpper()}");
        reqContent.AppendLine($"PATH: {context.Request.Path}");
        reqContent.AppendLine("USED HEADERS:");
        foreach (var (headerKey, headerValue) in context.Request.Headers)
        {
            reqContent.AppendLine($"{headerKey}: {headerValue}");
        }

        reqContent.AppendLine("BODY:");
        context.Request.EnableBuffering();
        using (var requestReader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var content = await requestReader.ReadToEndAsync();
            reqContent.AppendLine(content);
            context.Request.Body.Position = 0;
        }
        Console.Write(reqContent.ToString());

        await next(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }
});

app.MapControllers();

app.Run();
