using System.Text;
using Authzed.Api.V1;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SCIM;
using Newtonsoft.Json;
using SpiceDB.SCIM.Provider.Provider;

namespace SpiceDB.SCIM.Provider.Extensions;

public static class ApplicationServices
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services.BuildServiceProvider();
        var environment = services.GetService<IHostEnvironment>();
        var configuration = services.GetService<IConfiguration>();

        void ConfigureMvcNewtonsoftJsonOptions(MvcNewtonsoftJsonOptions options) =>
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

        void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        builder.Services.AddGrpcClient<PermissionsService.PermissionsServiceClient>(ConfigureSpiceDbGrpcClient)
            .ConfigureChannel(ConfigureSpiceDbChannel);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddAuthentication(ConfigureAuthenticationOptions).AddJwtBearer(ConfigureJwtBearerOptions);
        builder.Services.AddControllers().AddNewtonsoftJson(ConfigureMvcNewtonsoftJsonOptions);
        services = builder.Services.BuildServiceProvider();
        IMonitor monitoringBehavior = new ConsoleMonitor();
        var permissionServiceClient = services.GetService<PermissionsService.PermissionsServiceClient>();
        IProvider providerBehavior = new SpiceDbProvider(permissionServiceClient);


        builder.Services.AddSingleton(providerBehavior);
        builder.Services.AddSingleton(monitoringBehavior);
        return;

        void ConfigureJwtBearerOptions(JwtBearerOptions options)
        {
            if (environment.IsDevelopment())
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = configuration["Token:TokenIssuer"],
                        ValidAudience = configuration["Token:TokenAudience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:IssuerSigningKey"]))
                    };
            }
            else
            {
                options.Authority = configuration["Token:TokenIssuer"];
                options.Audience = configuration["Token:TokenAudience"];
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context => { return Task.CompletedTask; },
                    OnAuthenticationFailed = AuthenticationFailed
                };
            }
        }

        void ConfigureSpiceDbGrpcClient(IServiceProvider provider, GrpcClientFactoryOptions options)
        {
            options.Address =
                new Uri(configuration["SpiceDB:GrpcAddress"] ?? throw new InvalidOperationException());
        }

        void ConfigureSpiceDbChannel(IServiceProvider provider, GrpcChannelOptions options)
        {
            var credentials = CallCredentials.FromInterceptor((context, metadata) =>
            {
                string token = configuration["SpiceDB:Token"] ?? throw new InvalidOperationException();
                metadata.Add("Authorization", $"Bearer {token}");
                return Task.CompletedTask;
            });

            // TODO: - we're using Insecure credentials only for this demo so that we don't need to setup TLS
            options.UnsafeUseInsecureChannelCallCredentials = true;
            options.Credentials = ChannelCredentials.Create(ChannelCredentials.SecureSsl, credentials);
        }
    }

    private static Task AuthenticationFailed(AuthenticationFailedContext arg)
    {
        // For debugging purposes only!
        string authenticationExceptionMessage = $"{{AuthenticationFailed: '{arg.Exception.Message}'}}";

        arg.Response.ContentLength = authenticationExceptionMessage.Length;
        arg.Response.Body.WriteAsync(
            Encoding.UTF8.GetBytes(authenticationExceptionMessage),
            0,
            authenticationExceptionMessage.Length);

        return Task.FromException(arg.Exception);
    }
}
