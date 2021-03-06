﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Ninject;

namespace SportsStore.WebUI.Infrastructure
{
    using Domain.Abstract;
    using Domain.Entities;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelparam)
        {
            kernel = kernelparam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running Shoes", Price = 95 }
            });

            kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
        }
    }
}