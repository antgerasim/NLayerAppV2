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

using System;
using System.Collections.Generic;

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services;
using Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext.InstanceProviders;
using Microsoft.Samples.NLayerApp.DistributedServices.Seedwork.ErrorHandlers;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext
{

   [ApplicationErrorHandler()] // manage all unhandled exceptions
   [UnityInstanceProviderServiceBehavior()]
   //create instance and inject dependencies using unity container
   public class ErpModuleService : IErpModuleService
   {
      #region Constructor
      /// <summary>
      ///    Create a new instance ERP Module Service
      /// </summary>
      /// <param name="customerAppService">The customer app service dependency</param>
      /// <param name="salesAppService">The sales app service dependency</param>
      public ErpModuleService(ICustomerAppService customerAppService, ISalesAppService salesAppService)
      {
         if (customerAppService == null) { throw new ArgumentNullException("customerAppService"); }

         if (salesAppService == null) { throw new ArgumentNullException("salesAppService"); }

         _customerAppService = customerAppService;
         _salesAppService = salesAppService;
      }
      #endregion

      #region IDisposable Members
      /// <summary>
      ///    <see cref="M:System.IDisposable.Dispose" />
      /// </summary>
      public void Dispose()
      {
         //dispose all resources
         _salesAppService.Dispose();
         _customerAppService.Dispose();
      }
      #endregion

      #region Members
      private readonly ICustomerAppService _customerAppService;
      private readonly ISalesAppService _salesAppService;
      #endregion

      #region IERPModuleService Members
      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="customer">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public CustomerDto AddNewCustomer(CustomerDto customer)
      {
         return _customerAppService.AddNewCustomer(customer);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="customer">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      public void UpdateCustomer(CustomerDto customer)
      {
         _customerAppService.UpdateCustomer(customer);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="customer">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      public void RemoveCustomer(Guid customerId)
      {
         _customerAppService.RemoveCustomer(customerId);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="pageIndex">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <param name="pageCount">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<CustomerListDto> FindCustomersInPage(int pageIndex, int pageCount)
      {
         return _customerAppService.FindCustomers(pageIndex, pageCount);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="filter">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<CustomerListDto> FindCustomersByFilter(string filter)
      {
         return _customerAppService.FindCustomers(filter);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="customerId">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public CustomerDto FindCustomer(Guid customerId)
      {
         return _customerAppService.FindCustomer(customerId);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="pageIndex">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <param name="pageCount">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<CountryDto> FindCountriesInPage(int pageIndex, int pageCount)
      {
         return _customerAppService.FindCountries(pageIndex, pageCount);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="filter">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<CountryDto> FindCountriesByFilter(string filter)
      {
         return _customerAppService.FindCountries(filter);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="pageIndex">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <param name="pageCount">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<OrderListDto> FindOrdersInPage(int pageIndex, int pageCount)
      {
         return _salesAppService.FindOrders(pageIndex, pageCount);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="from">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <param name="to">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<OrderListDto> FindOrdersByFilter(DateTime from, DateTime to)
      {
         return _salesAppService.FindOrders(from, to);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="customerId">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<OrderListDto> FindOrdersByCustomer(Guid customerId)
      {
         return _salesAppService.FindOrders(customerId);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="order">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public OrderDto AddNewOrder(OrderDto order)
      {
         return _salesAppService.AddNewOrder(order);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="software">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public SoftwareDto AddNewSoftware(SoftwareDto software)
      {
         return _salesAppService.AddNewSoftware(software);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="book">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public BookDto AddNewBook(BookDto book)
      {
         return _salesAppService.AddNewBook(book);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="pageIndex">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <param name="pageCount">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<ProductDto> FindProductsInPage(int pageIndex, int pageCount)
      {
         return _salesAppService.FindProducts(pageIndex, pageCount);
      }

      /// <summary>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </summary>
      /// <param name="filter">
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </param>
      /// <returns>
      ///    <see
      ///       cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.BankingModule.Services.IMainBoundedContextService" />
      /// </returns>
      public List<ProductDto> FindProductsByFilter(string filter)
      {
         return _salesAppService.FindProducts(filter);
      }
      #endregion
   }

}