using FilterWithExHndlrMiddleware.Commons;
using FilterWithExHndlrMiddleware.Middlewares;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ErrorResponse.GenerateErrorResponse;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Add the global exception handling middleware to the pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();
