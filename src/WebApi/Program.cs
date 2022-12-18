using Application;
using Hangfire;
using Infrastructure;
using MediatR;
using System.Reflection;
using WebApi.Concrete;
using WebApi.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("JobConnectionString")));
builder.Services.AddHangfireServer();




// Add services to the container.



builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IGetMoviesDataJob, GetMoviesDataJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireServer(new BackgroundJobServerOptions());

GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });



//RecurringJobs.GetMovieData();
app.Run();
