using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PSP_Komanda32_API.Services;
using PSP_Komanda32_API.Services.Database;
using PSP_Komanda32_API.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddScoped<IRandomizer, Randomizer>();

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

builder.Services.AddDbContext<PoSSContext>(options => options.UseSqlite("Data Source=PoSS.db"));
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();

    //Tai dabar, kad sugeneruoti Yaml, reikia paleist programa ir webpage virsuj po uzrasu API
    //yra linkas i /swagger/v1/swagger.yaml, is cia galima gauti yaml faila.
    //O kad generuoti html, tai reikia ta yaml faila imest i cia - https://editor.swagger.io/
    //ten yra galimybiu generuot pagal yaml visokius kitus failus
    app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Komandux project API"); });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PoSSContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
