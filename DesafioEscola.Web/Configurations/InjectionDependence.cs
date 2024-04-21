
using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using DesafioEscola.Application.Mapping;
using DesafioEscola.Application.Services;
using DesafioEscola.Application.Validators;
using DesafioEscola.Data.Context;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Data.Migrations;
using DesafioEscola.Data.Repositories;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AuthenticationService = DesafioEscola.Application.Services.AuthenticationService;
using IAuthenticationService = DesafioEscola.Application.Interfaces.IAuthenticationService;

namespace DesafioEscola.Web.Configurations;

public class InjectionDependence
{
    public static void Configuration(IServiceCollection services, IConfiguration configuration)
    {


        services.AddRazorPages();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        services.AddScoped<IStudentClassRoomRepository, StudentClassRoomRepository>();

        //Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICryptService, CryptService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IClassroomService, ClassroomService>();
        services.AddScoped<IStudentClassRoomService, StudentClassRoomService>();

        //Session
        services.AddScoped<DbSession>();

        //Migration
        ConfigurationMigration(services, configuration.GetConnectionString("Principal"));

        //Authentication
        var secret = configuration.GetValue<string>("SecretJWT");
        var secretBytes = Encoding.ASCII.GetBytes(secret);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        //Mapper
        services.AddAutoMapper(assemblies: typeof(Mapping).Assembly);

        //Middleware
        services.AddScoped<MiddlewareGlobalException>();

        //Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Escola", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.  Bearer 12345abcdef",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                }
            });
        });

        //FluentValidation
        services.AddSingleton<IValidator<RegisterStudentDTO>, RegisterStudentDTOValidator>();
        services.AddSingleton<IValidator<UpdateStudentDTO>, UpdateStudentDTOValidator>();
        services.AddSingleton<IValidator<CreateClassroomDTO>, CreateClassroomDTOValidator>();
        services.AddSingleton<IValidator<ClassroomDTO>, ClassroomDTOValidator>();
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    }

    private static void ConfigurationMigration(IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(cfg => cfg
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(DesafioEscolaMigration18041945).Assembly).For.Migrations()
            )
            .AddLogging(cfg => cfg.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

            var serviceProvider = services.BuildServiceProvider();
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
    }
}
