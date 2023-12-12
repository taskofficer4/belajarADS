using Actris.Abstraction;
using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using Actris.Infrastructure.EntityFramework.Queries;
using Actris.Infrastructure.EntityFramework.Repositories;
using Actris.Infrastructure.Services;
using Actris.Web.Providers; 
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using System.Runtime.Caching;

namespace Actris.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterInstance(new MemoryCache("system"));

            container.RegisterType<ActrisContext, DbContextMapper>();
            container.RegisterType<IConnectionProvider, ConnectionStringProvider>();
            container.RegisterFactory<HttpContextBase>((_) =>
            {
                return new HttpContextWrapper(HttpContext.Current);
            });


            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IActSourceRepository, ActSourceRepository>();
            container.RegisterType<IActSourceService, ActSourceService>();

            container.RegisterType<ICaPriorityRepository, CaPriorityRepository>();
            container.RegisterType<ICaPriorityService, CaPriorityService>();

            container.RegisterType<IImpactAnalysisRepository, ImpactAnalysisRepository>();
            container.RegisterType<IImpactAnalysisService, ImpactAnalysisService>();

            container.RegisterType<ICaOverdueImpactRepository, CaOverdueImpactRepository>();
            container.RegisterType<ICaOverdueImpactService, CaOverdueImpactService>();

            container.RegisterType<IActionTrackingReportRepository, ActionTrackingReportRepository>();
            container.RegisterType<IActionTrackingReportService, ActionTrackingReportService>();

            container.RegisterType<ILookupService, LookupService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}