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


namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.Resources;
    using Microsoft.Samples.NLayerApp.Application.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;

    /// <summary>
    /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
    /// </summary>
    public class SalesAppService
        : ISalesAppService
    {
        #region Members

        readonly IOrderRepository _orderRepository;
        readonly IProductRepository _productRepository;
        readonly ICustomerRepository _customerRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of sales management service
        /// </summary>
        /// <param name="orderRepository">The associated order repository</param>
        /// <param name="productRepository">The associated product repository</param>
        /// <param name="customerRepository">The associated customer repository</param>
        public SalesAppService(IProductRepository productRepository,//associated product repository
                               IOrderRepository orderRepository,//associated order repository
                               ICustomerRepository customerRepository) //the associated customer repository
        {
            if (orderRepository == null)
                throw new ArgumentNullException("orderRepository");

            if (productRepository == null)
                throw new ArgumentNullException("productRepository");

            if (customerRepository == null)
                throw new ArgumentNullException("customerRepository");

            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;

        }
        #endregion

        #region ISalesAppService Members

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></returns>
        public List<OrderListDTO> FindOrders(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindOrders);

            //recover orders in paged fashion
            var orders = _orderRepository.GetPaged<DateTime>(pageIndex, pageCount, o => o.OrderDate, false);

            if (orders != null
                &&
                orders.Any())
            {
                return orders.ProjectedAsCollection<OrderListDTO>();
            }
            else // no data
                return null;
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="dateFrom"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <param name="dateTo"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></returns>
        public List<OrderListDTO> FindOrders(DateTime? dateFrom, DateTime? dateTo)
        {
            //create the specification ( how to filter orders from dates..)
            var spec = OrdersSpecifications.OrderFromDateRange(dateFrom, dateTo);

            //recover orders
            var orders = _orderRepository.AllMatching(spec);

            if (orders != null
               &&
               orders.Any())
            {
                return orders.ProjectedAsCollection<OrderListDTO>();
            }
            else //no data
                return null;

        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="customerId"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></returns>
        public List<OrderListDTO> FindOrders(Guid customerId)
        {
            var orders = _orderRepository.GetFiltered(o => o.CustomerId == customerId);

            if (orders != null
               &&
               orders.Any())
            {
                return orders.ProjectedAsCollection<OrderListDTO>();
            }
            else //no data..
                return null;

        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="pageIndex"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <param name="pageCount"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></returns>
        public List<ProductDTO> FindProducts(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentForFindProducts);

            //recover products
            var products = _productRepository.GetPaged<string>(pageIndex, pageCount, p => p.Title, false);

            if (products != null
                &&
                products.Any())
            {
                return products.ProjectedAsCollection<ProductDTO>();
            }
            else // no data
                return null;
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="text"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></returns>
        public List<ProductDTO> FindProducts(string text)
        {
            //create the specification ( howto find products for any string ) 
            var spec = ProductSpecifications.ProductFullText(text);

            //recover products
            var products = _productRepository.AllMatching(spec);

            //adapt results
            return products.ProjectedAsCollection<ProductDTO>();
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="orderDto"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        public OrderDTO AddNewOrder(OrderDTO orderDto)
        {
            //if orderdto data is not valid
            if (orderDto == null || orderDto.CustomerId == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddOrderWithNullInformation);
            
            var customer = _customerRepository.Get(orderDto.CustomerId);

            if (customer != null)
            {
                //Create a new order entity
                var newOrder = CreateNewOrder(orderDto, customer);

                if (newOrder.IsCreditValidForOrder()) //if total order is less than credit 
                {
                    //save order
                    SaveOrder(newOrder);

                    return newOrder.ProjectedAs<OrderDTO>();
                }
                else //total order is greater than credit
                {
                    LoggerFactory.CreateLog().LogInfo(Messages.info_OrderTotalIsGreaterCustomerCredit);
                    return null;
                }
            }
            else
            {
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotCreateOrderForNonExistingCustomer);
                return null;
            }
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="softwareDTO"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        public SoftwareDTO AddNewSoftware(SoftwareDTO softwareDTO)
        {
            if (softwareDTO == null)
                throw new ArgumentException(Messages.warning_CannotAddSoftwareWithNullInformation);

            //Create the softare entity
            var newSoftware = new Software(softwareDTO.Title, softwareDTO.Description,softwareDTO.LicenseCode);

            //set unit price and stock
            newSoftware.ChangeUnitPrice(softwareDTO.UnitPrice);
            newSoftware.IncrementStock(softwareDTO.AmountInStock);

            //Assign the poid
            newSoftware.GenerateNewIdentity();

            //save software
            SaveProduct(newSoftware);

            //return software dto
            return newSoftware.ProjectedAs<SoftwareDTO>();
        }
        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/>
        /// </summary>
        /// <param name="bookDTO"><see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ISalesAppService"/></param>
        public BookDTO AddNewBook(BookDTO bookDTO)
        {
            if (bookDTO == null)
                throw new ArgumentNullException(Messages.warning_CannotAddSoftwareWithNullInformation);

            //Create the book entity
            var newBook = new Book(bookDTO.Title, bookDTO.Description,bookDTO.Publisher,bookDTO.ISBN);
            
            //set stock and unit price
            newBook.IncrementStock(bookDTO.AmountInStock);
            newBook.ChangeUnitPrice(bookDTO.UnitPrice);

            //Assign the poid
            newBook.GenerateNewIdentity();

            //save software
            SaveProduct(newBook);

            //return software dto
            return newBook.ProjectedAs<BookDTO>();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose all resources
            _orderRepository.Dispose();
            _productRepository.Dispose();
            _customerRepository.Dispose();
        }

        #endregion

        #region Private Methods

        void SaveOrder(Order order)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(order))//if entity is valid save. 
            {
                //add order and commit changes
                _orderRepository.Add(order);
                _orderRepository.UnitOfWork.Commit();
            }
            else // if not valid throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(order));
        }
        Order CreateNewOrder(OrderDTO dto, Customer associatedCustomer)
        {
            //Create a new order entity from factory
            Order newOrder = OrderFactory.CreateOrder(associatedCustomer,
                                                     dto.ShippingName,
                                                     dto.ShippingCity,
                                                     dto.ShippingAddress,
                                                     dto.ShippingZipCode);

            //if have lines..add
            if (dto.OrderLines != null)
            {
                foreach (var line in dto.OrderLines) //add order lines
                    newOrder.AddNewOrderLine(line.ProductId, line.Amount, line.UnitPrice, line.Discount / 100);
            }

            return newOrder;
        }
        void SaveProduct(Product product)
        {
            var entityValidator = EntityValidatorFactory.CreateValidator();

            if (entityValidator.IsValid(product)) // if is valid
            {
                _productRepository.Add(product);
                _productRepository.UnitOfWork.Commit();
            }
            else //if not valid, throw validation errors
                throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(product));
        }

        #endregion
    }
}
