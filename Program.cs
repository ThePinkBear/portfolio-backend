using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using portfolio_backend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TextDbContext>(options =>
  options.UseSqlite(builder.Configuration.GetConnectionString("db_connection") ?? throw new InvalidOperationException("Connection string 'portfolio_db_pinkbear_test' not found.")));

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
  });


builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddAuthorization(options =>
{
  var issuer = builder.Configuration["Auth0:Domain"];
  if (!string.IsNullOrEmpty(issuer))
  {
    options.AddPolicy(
    "admin:edit",
    policy => policy.Requirements.Add(
    new HasScopeRequirement("admin:edit",
     issuer))
    );
  }
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
