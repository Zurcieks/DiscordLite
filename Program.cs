using System.Text;
using DiscordLite.Features.Authentication.Login;
using DiscordLite.Features.Authentication.Logout;
using DiscordLite.Features.Authentication.Profile;
using DiscordLite.Features.Authentication.Refresh;
using DiscordLite.Features.Authentication.Register;
using DiscordLite.Infrastructure;
using DiscordLite.Infrastructure.OpenApi;
using DiscordLite.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidation();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.SectionName));

var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions!.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
    });

 

builder.Services.AddAuthorization();

var app = builder.Build();



 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapRegisterEndpoint();
app.MapLoginEndpoint();
app.MapGetProfileEndpoint();
app.MapRefreshTokenEndpoint();
app.MapLogoutEndpoint();
 
app.Run();

 