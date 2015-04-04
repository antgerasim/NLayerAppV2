//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
			
namespace Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext.InstanceProviders
{
    using Microsoft.Practices.Unity;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services;

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Services;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;

    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.BankingModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.ERPModule.Repositories;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
    

    /// <summary>
    /// DI container accessor
    /// </summary>
    public static class Container
    {
        #region Properties

        static  IUnityContainer _currentContainer;

        /// <summary>
        /// Get the current configured container
        /// </summary>
        /// <returns>Configured container</returns>
        public static IUnityContainer Current
        {
            get
            {
                return _currentContainer;
            }
        }

        #endregion

        #region Constructor
        
        static Container()
        {
            ConfigureContainer();

            ConfigureFactories();
        }

        #endregion

        #region Methods

        static void ConfigureContainer()
        {
            /*
             * Add here the code configuration or the call to configure the container 
             * using the application configuration file
             */

            _currentContainer = new UnityContainer();
            
            
            //-> Unit of Work and repositories
            _currentContainer.RegisterType(typeof(MainBCUnitOfWork), new PerResolveLifetimeManager());
            

            _currentContainer.RegisterType<IBankAccountRepository, BankAccountRepository>();
            _currentContainer.RegisterType<ICountryRepository, CountryRepository>();
            _currentContainer.RegisterType<ICustomerRepository, CustomerRepository>();
            _currentContainer.RegisterType<IOrderRepository,OrderRepository>();
            _currentContainer.RegisterType<IProductRepository, ProductRepository>();

            //-> Adapters
            _currentContainer.RegisterType<ITypeAdapterFactory, AutomapperTypeAdapterFactory>(new ContainerControlledLifetimeManager());

            //-> Domain Services
            _currentContainer.RegisterType<IBankTransferService, BankTransferService>();

            //-> Application services
            _currentContainer.RegisterType<ISalesAppService, SalesAppService>();
            _currentContainer.RegisterType<ICustomerAppService, CustomerAppService>();
            _currentContainer.RegisterType<IBankAppService, BankAppService>();

            //-> Distributed Services
            _currentContainer.RegisterType<IBankingModuleService, BankingModuleService>();
            _currentContainer.RegisterType<IERPModuleService, ERPModuleService>();
        }


        static void ConfigureFactories()
        {
            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

            var typeAdapterFactory = _currentContainer.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);
        }

        #endregion
    }
}