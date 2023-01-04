
using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
   
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, BubierDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}