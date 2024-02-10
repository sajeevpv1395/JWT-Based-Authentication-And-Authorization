using JWT_Based_Authentication_And_Authorization.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
//builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
   /* var Key = "dsd3434g@@43434343Ffff67g_"*/;
    o.SaveToken = true;
 
    o.TokenValidationParameters = new TokenValidationParameters
	{
	
		ValidateIssuerSigningKey = true,	
		ValidIssuer = configuration["JWT:Issuer"],
		ValidAudience = configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Key),
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true
	};
});

builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();




builder.Services.AddControllers();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
