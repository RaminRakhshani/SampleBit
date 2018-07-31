using Bit.Core;
using Bit.Core.Contracts;
using Bit.Core.Implementations;
using Bit.Data;
using Bit.Data.Contracts;
using Bit.Data.EntityFrameworkCore.Implementations;
using Bit.Model.Implementations;
using Bit.OData.ActionFilters;
using Bit.OData.Contracts;
using Bit.Owin.Implementations;
using Bit.OwinCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Wallet.Data;

[assembly: ODataModule("Wallet")]

namespace Wallet.Api
{
    public class AppStartup : AutofacAspNetCoreAppStartup
    {
        public AppStartup(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DefaultAppModulesProvider.Current = new WalletAppModulesProvider();

            return base.ConfigureServices(services);
        }
    }

    public class WalletAppModulesProvider : IAppModule, IAppModulesProvider
    {
        public IEnumerable<IAppModule> GetAppModules()
        {
            yield return this;
        }

        public virtual void ConfigureDependencies(IServiceCollection services, IDependencyManager dependencyManager)
        {
            AssemblyContainer.Current.Init();

            dependencyManager.RegisterMinimalDependencies();

            dependencyManager.RegisterDefaultLogger(typeof(DebugLogStore).GetTypeInfo(), typeof(ConsoleLogStore).GetTypeInfo());

            dependencyManager.RegisterDefaultAspNetCoreApp();

            /*services.AddResponseCompression();
            dependencyManager.RegisterAspNetCoreMiddlewareUsing(aspNetCoreApp =>
            {
                aspNetCoreApp.UseResponseCompression();
            });*/

            dependencyManager.RegisterMinimalAspNetCoreMiddlewares();

            dependencyManager.RegisterAspNetCoreSingleSignOnClient();

            dependencyManager.RegisterMetadata();

            dependencyManager.RegisterDefaultWebApiAndODataConfiguration();

            dependencyManager.RegisterWebApiMiddleware(webApiDependencyManager =>
            {
                webApiDependencyManager.RegisterWebApiMiddlewareUsingDefaultConfiguration();

                webApiDependencyManager.RegisterGlobalWebApiActionFiltersUsing(httpConfiguration =>
                {
                    httpConfiguration.Filters.Add(new System.Web.Http.AuthorizeAttribute());
                });

                webApiDependencyManager.RegisterGlobalWebApiCustomizerUsing(httpConfiguration =>
                {
                    httpConfiguration.EnableSwagger(c =>
                    {
                        c.SingleApiVersion($"v{DefaultAppEnvironmentsProvider.Current.GetActiveAppEnvironment().AppInfo.Version}", $"Swagger-Api");
                        c.ApplyDefaultApiConfig(httpConfiguration);
                    }).EnableBitSwaggerUi();
                });
            });

            dependencyManager.RegisterODataMiddleware(odataDependencyManager =>
            {
                odataDependencyManager.RegisterGlobalWebApiActionFiltersUsing(httpConfiguration =>
                {
                    httpConfiguration.Filters.Add(new DefaultODataAuthorizeAttribute());
                });

                odataDependencyManager.RegisterGlobalWebApiCustomizerUsing(httpConfiguration =>
                {
                    httpConfiguration.EnableSwagger(c =>
                    {
                        c.SingleApiVersion($"v{DefaultAppEnvironmentsProvider.Current.GetActiveAppEnvironment().AppInfo.Version}", $"Swagger-Api");
                        c.ApplyDefaultODataConfig(httpConfiguration);
                    }).EnableBitSwaggerUi();
                });

                odataDependencyManager.RegisterWebApiODataMiddlewareUsingDefaultConfiguration();
            });

            dependencyManager.Register<IDbConnectionProvider, DefaultDbConnectionProvider<SqlConnection>>();
            dependencyManager.RegisterEfCoreDbContext<WalletDbContext, SqlServerDbContextObjectsProvider>();

            dependencyManager.RegisterRepository(typeof(EfCoreRepository<>).GetTypeInfo());

            dependencyManager.RegisterDtoEntityMapper();
            dependencyManager.RegisterDtoEntityMapperConfiguration<DefaultDtoEntityMapperConfiguration>();

            //dependencyManager.RegisterSingleSignOnServer<SampleAppUserService, SampleAppClientProvider>();

            dependencyManager.RegisterIndexPageMiddlewareUsingDefaultConfiguration();
        }
    }
}
