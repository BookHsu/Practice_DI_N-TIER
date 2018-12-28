using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MVC_Repository_Prictice.Models;
using MVC_Repository_Prictice.Models.DbContextFactory;
using MVC_Repository_Prictice.Models.Interface;
using MVC_Repository_Prictice.Models.Repositiry;
using MVC_Repository_Prictice.Service;
using MVC_Repository_Prictice.Service.Interface;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace MVC_Repository_Prictice.Web
{
    public class UnityBootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {

            var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);
            return container;

        }

        public static void RegisterTypes(IUnityContainer container)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;
            container.RegisterType<IDbContextFactory, DbContextFactory>(
                new PerRequestLifetimeManager(),
                new InjectionConstructor(connectionString));
            //REPOSITORY
            container.RegisterType<IRepository<Categories>, GenericRepository<Categories>>();
            container.RegisterType<IRepository<Products>, GenericRepository<Products>>();
            //service 
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IProductService, ProductService>();



        }
    }
}