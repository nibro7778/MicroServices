using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MicroService.RavenDb
{
    public static class ServiceExtension
    {

        /// <summary>
        /// Adds a Raven <see cref="IDocumentStore"/> singleton to the dependency injection services. The <see cref="DocumentStore"/> is configured using the settings in appsettings.json. The settings will be stored in an injectable <see cref="DbSettings"/> instance.
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="configuration">The configuration to load the <see cref="DbSettings"/> from.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRavenDocumentStore(this IServiceCollection svc, IConfiguration configuration)
        {
            // Get the settings from the configuration.
            svc.Configure<DbSettings>(configuration);
            var provider = svc.BuildServiceProvider();
            var settings = provider.GetRequiredService<IOptions<DbSettings>>().Value;
            var onBeforeStore = provider.GetService<IOnBeforeStore>();

            return AddRavenDocumentStore(svc, settings, onBeforeStore);
        }

        /// <summary>
        /// Adds a Raven <see cref="IDocumentStore"/> singleton to the dependency injection services. The <see cref="DocumentStore"/> will be configured using the specified <paramref name="dbSettings"/>
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="dbSettings">The settings for the document store.</param>
        /// <param name="onBeforeStores"></param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddRavenDocumentStore(this IServiceCollection svc, DbSettings dbSettings,
            IOnBeforeStore onBeforeStore)
        {
            var docStore = new DocumentStore
            {
                Urls = new[] { dbSettings.Url },
                Database = dbSettings.DatabaseName
            };

            // Configure the certificate if we have one in app settings.
            if (!string.IsNullOrEmpty(dbSettings.CertName))
            {
                var provider = svc.BuildServiceProvider();
                var host = provider.GetRequiredService<IHostingEnvironment>();
                var certFilePath = Path.Combine(host.ContentRootPath, dbSettings.CertName);
                docStore.Certificate = new X509Certificate2(certFilePath, dbSettings.CertPassword);
            }

            if (onBeforeStore != null)
                docStore.OnBeforeStore += onBeforeStore.Handle;


            docStore.Initialize();
            svc.AddSingleton<IDocumentStore>(docStore);
            return svc;
        }

        /// <summary>
        /// Gets the document store inside the dependency injection services and configures a new <see cref="IAsyncDocumentSession"/> to be created on each request.
        /// </summary>
        /// <returns>The dependency injection services.</returns>
        public static IServiceCollection AddRavenAsyncDocumentSession(this IServiceCollection svc)
        {
            return svc.AddScoped<IAsyncDocumentSession>(provider =>
            {
                IDocumentStore db;
                try
                {
                    db = provider.GetRequiredService<IDocumentStore>();
                }
                catch (InvalidOperationException notFoundError)
                {
                    throw new InvalidOperationException("Couldn't find the Raven IDocumentStore. To fix this, in Startup call .AddRavenDocStore(...) before calling AddAsyncDocumentSession(...)", notFoundError);
                }

                return db.OpenAsyncSession();
            });
        }

        /// <summary>
        /// Gets the document store inside the dependency injection services and configures a new <see cref="IDocumentSession"/> to be created on each request.
        /// </summary>
        /// <returns>The dependency injection services.</returns>
        public static IServiceCollection AddRavenDocumentSession(this IServiceCollection svc)
        {
            return svc.AddScoped<IDocumentSession>(provider =>
            {
                IDocumentStore db;
                try
                {
                    db = provider.GetRequiredService<IDocumentStore>();
                }
                catch (InvalidOperationException notFoundError)
                {
                    throw new InvalidOperationException("Couldn't find the Raven IDocumentStore. To fix this, in Startup call .AddRavenDocStore(...) before calling AddDocumentSession(...)", notFoundError);
                }

                return db.OpenSession();
            });
        }

        public static IServiceCollection EnableRavenDb(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection("RavenDb"));
            services.AddRavenDocumentStore(configuration)
                .AddRavenAsyncDocumentSession()
                .AddRavenDocumentSession();

            return services;
        }
    }
}
