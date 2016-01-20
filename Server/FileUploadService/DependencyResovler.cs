using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Repository;
using TAP.FileService.Domain.Repository.Impl;
using TAP.FileService.Domain.Service;
using TAP.FileService.Domain.Service.Impl;
using TAP.FileService.Infrastructure;
using TAP.FileService.Infrastructure.Impl.EF;

namespace TAP.FileService
{
    internal class DependencyResovler
    {
        private static IUnityContainer container;

        static DependencyResovler()
        {
            if (container == null)
            {
                container = new UnityContainer();
                BootStrap();
            }
        }

        public static IUnityContainer GobalContainer()
        {
            return container;
        }

        private static void BootStrap()
        {
            container.RegisterType<IUnitOfWork, EFUnitOfWork>(new PerResolveLifetimeManager());
            container.RegisterType(typeof(IRepository<,>), typeof(EFRepository<,>), new PerResolveLifetimeManager());
            container.RegisterType<IFileMetadataRepository, FileMetadataRepository>();
            container.RegisterType<IFileMediaRepository, FileMediaRepository>();
            container.RegisterType<IFileMediaService, FileMediaService>();
        }
    }
}