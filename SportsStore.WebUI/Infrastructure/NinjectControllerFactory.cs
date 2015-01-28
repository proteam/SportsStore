using System;
using Ninject;
using System.Web.Routing;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    namespace SportsStore.WebUI.Infrastructure
    {
        // реализация пользовательской фабрики контроллеров,
        // наследуясь от фабрики используемой по умолчанию
        public class NinjectControllerFactory : DefaultControllerFactory
        {
            private IKernel ninjectKernel;
            public NinjectControllerFactory()
            {
                // создание контейнера
                ninjectKernel = new StandardKernel();
                AddBindings();
            }
            protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
            {
                // получение объекта контроллера из контейнера
                // используя его тип
                return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
            }
            private void AddBindings()
            {
                // конфигурирование контейнера
                ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            }
        }
    }
}