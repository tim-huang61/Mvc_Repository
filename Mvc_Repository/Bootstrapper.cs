using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Repository.Models;
using Mvc_Repository.Models.Interfaces;
using Mvc_Repository.Models.Repositories;
using Mvc_Repository.Services;
using Mvc_Repository.Services.Interfaces;
using Unity;
using Unity.AspNet.Mvc;

namespace Mvc_Repository.Web
{
    public class Bootstrapper
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
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // Repository
            container.RegisterType<IRepository<Category>, GenericRepository<Category>>();
            container.RegisterType<IRepository<Product>, GenericRepository<Product>>();

            // Service
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IProductService, ProductService>();
        }
    }
}