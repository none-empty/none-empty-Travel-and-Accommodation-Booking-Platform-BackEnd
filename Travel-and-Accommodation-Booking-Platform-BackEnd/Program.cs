using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Behaviors;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Jwt;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Services;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Middlewares;
using Travel_and_Accommodation_Booking_Platform_BackEnd.ServicesRegistrations;
using AppDbContext = Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles.AppDbContext;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    }; 
    
});
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetSection("connectionStrings")?["sqlserver"])
);

builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IGuidGenerator, GuidGenerator>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IDateGetter, DateGetter>();
builder.Services.AddAdHocPersistance();
builder.Services.AddAuthorization(options =>
    options.AddPolicy("Admin Only", policy =>
    {
        policy.RequireRole(nameof(SiteRole.Admin));
    })
);
builder.Services.AddResponseCaching();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JwtSettings:SymmetricSecurityKey"]!)),

            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true
        };
        
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var blacklist =
                    context.HttpContext.RequestServices
                        .GetRequiredService<ITokenBlacklistService>();

                var jti = context.Principal?
                    .FindFirstValue(JwtRegisteredClaimNames.Jti);

                if (jti != null &&
                    await blacklist.IsBlacklistedAsync(jti))
                {
                    context.Fail("Token is invalid at this point");
                }
            }
        };
        
    });

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Travel_and_Accommodation_Booking_Platform_BackEnd", Version = "v1" });

    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    if (File.Exists(xmlCommentsFullPath))
    {
        setup.IncludeXmlComments(xmlCommentsFullPath);
    }

    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            
    
});

builder.Services.AddRepositories();
builder.Services.AddSearchPredicatesRepos();
builder.Services.AddMapperlyMappings();

builder.Services.AddValidatorsFromAssembly(typeof(RegisterUserCommandValidator).Assembly);
builder.Services.AddMediatR(cfg => {
    
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(config["Redis:connection"]!)
);

builder.Services.AddScoped<ITokenBlacklistService, RedisTokenBlacklistService>();

builder.Services.AddFluentEmail(
    config["Email:SenderEmail"], config["Email:Sender"])
    .AddSmtpSender(config["Email:Host"],config.GetValue<int>("Email:Port"));

builder.Services.AddScoped<IEmailServiceManager, EmailServiceManager>();
var app = builder.Build();
app.MapControllers();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Travel_and_Accommodation_Booking_Platform_BackEnd V1"));
   
}

 
app.UseHttpsRedirection();
app.UseResponseCaching();

app.Run();

 