using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceForCollectingApplications;
using ServiceForCollectingApplications.DataAccess;
using ServiceForCollectingApplications.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ConferenceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConferenceConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IProposalService, ProposalService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
