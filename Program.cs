using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRandomizer, Randomizer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    c.DocInclusionPredicate((name, api) => true);

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    /*
     * Kazkokiu panasiu metodu turetu sugeneruot yaml
     * https://stackoverflow.com/questions/45100923/generate-yaml-swagger-using-swashbuckle
    app.UseSwaggerUI(x => { x.SwaggerEndpoint("/v1/swagger.yaml", "Komandux project API"); });
    */
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
