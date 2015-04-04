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

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.DistributedServices.Seedwork.ErrorHandlers;

    /// <summary>
    /// WCF SERVICE FACADE FOR ERP MODULE
    /// </summary>
    [ServiceContract]
    public interface IERPModuleService
        :IDisposable
    {
        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        CustomerDTO AddNewCustomer(CustomerDTO customer);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void UpdateCustomer(CustomerDTO customer);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        void RemoveCustomer(Guid customer);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<CustomerListDTO> FindCustomersInPage(int pageIndex, int pageCount);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        CustomerDTO FindCustomer(Guid customerId);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<CustomerListDTO> FindCustomersByFilter(string filter);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<CountryDTO> FindCountriesInPage(int pageIndex, int pageCount);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<CountryDTO> FindCountriesByFilter(string filter);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<OrderListDTO> FindOrdersInPage(int pageIndex, int pageCount);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<OrderListDTO> FindOrdersByFilter(DateTime from, DateTime to);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<OrderListDTO> FindOrdersByCustomer(Guid customerId);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<ProductDTO> FindProductsInPage(int pageIndex, int pageCount);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        List<ProductDTO> FindProductsByFilter(string filter);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        OrderDTO AddNewOrder(OrderDTO order);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        SoftwareDTO AddNewSoftware(SoftwareDTO software);

        [OperationContract()]
        [FaultContract(typeof(ApplicationServiceError))]
        BookDTO AddNewBook(BookDTO book);
    }
}
