using CollegeHub.Data;
using CollegeHub.Extensions;
using CollegeHub.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(options => options.UseSqlite(builder.Configuration["SQLiteDatabase:Path"]));

builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy(Role.Student.ToString(), policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("role", Role.Student.ToString());
    });
    options.AddPolicy(Role.Teacher.ToString(), policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("role", Role.Teacher.ToString());
    });
    options.AddPolicy(Role.Adm.ToString(), policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("role", Role.Adm.ToString());
    });
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenSettings:Key"]))
    };
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:5213", "https://localhost:7238",
                                            "http://localhost:8080", "http://127.0.0.1:8080")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) => {
    var error = httpContext.Features?.Get<IExceptionHandlerFeature>()?.Error;
    return ErrorExtensions.GetErrorMessage(error);
});

app.Run();
