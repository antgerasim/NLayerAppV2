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
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.DistributedServices.Seedwork.ErrorHandlers;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext
{

   /// <summary>
   ///    WCF SERVICE FACADE FOR ERP MODULE
   /// </summary>
   [ServiceContract]
   public interface IErpModuleService : IDisposable
   {

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      CustomerDto AddNewCustomer(CustomerDto customer);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      void UpdateCustomer(CustomerDto customer);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      void RemoveCustomer(Guid customer);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<CustomerListDto> FindCustomersInPage(int pageIndex, int pageCount);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      CustomerDto FindCustomer(Guid customerId);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<CustomerListDto> FindCustomersByFilter(string filter);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<CountryDto> FindCountriesInPage(int pageIndex, int pageCount);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<CountryDto> FindCountriesByFilter(string filter);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<OrderListDto> FindOrdersInPage(int pageIndex, int pageCount);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<OrderListDto> FindOrdersByFilter(DateTime from, DateTime to);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<OrderListDto> FindOrdersByCustomer(Guid customerId);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<ProductDto> FindProductsInPage(int pageIndex, int pageCount);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      List<ProductDto> FindProductsByFilter(string filter);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      OrderDto AddNewOrder(OrderDto order);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      SoftwareDto AddNewSoftware(SoftwareDto software);

      [OperationContract()]
      [FaultContract(typeof (ApplicationServiceError))]
      BookDto AddNewBook(BookDto book);

   }

}