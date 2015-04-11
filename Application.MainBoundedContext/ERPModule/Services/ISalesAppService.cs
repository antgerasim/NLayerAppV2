using System;
using System.Collections.Generic;

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;

namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services
{

   /// <summary>
   ///    This is the contract that the application will interact to perform various operations for "sales management".
   ///    The responsability of this contract is to orchestrate operations, check security, cache,
   ///    adapt entities to DTO etc,
   /// </summary>
   public interface ISalesAppService : IDisposable
   {

      /// <summary>
      ///    Find orders in specific page
      /// </summary>
      /// <param name="pageIndex">The page index</param>
      /// <param name="pageCount">The number of elements in each page</param>
      /// <returns>A collection of orders representation</returns>
      List<OrderListDto> FindOrders(int pageIndex, int pageCount);

      /// <summary>
      ///    Find order in date range
      /// </summary>
      /// <param name="dateFrom">The date from</param>
      /// <param name="dateTo">The date to</param>
      /// <returns>A collection of orders representation</returns>
      List<OrderListDto> FindOrders(DateTime? dateFrom, DateTime? dateTo);

      /// <summary>
      ///    Find orders by customer identifier
      /// </summary>
      /// <param name="customerId">The customer identifier</param>
      /// <returns>A collection of orders representation</returns>
      List<OrderListDto> FindOrders(Guid customerId);

      /// <summary>
      ///    Find products in specific page
      /// </summary>
      /// <param name="pageIndex">The page index</param>
      /// <param name="pageCount">The number of elements in each page</param>
      /// <returns></returns>
      List<ProductDto> FindProducts(int pageIndex, int pageCount);

      /// <summary>
      ///    Find products
      /// </summary>
      /// <param name="text">The text to find in title or product description</param>
      /// <returns>The products with title or description that contains <paramref name="text" /></returns>
      List<ProductDto> FindProducts(string text);

      /// <summary>
      ///    Add new order
      /// </summary>
      /// <param name="order">The order representation to add</param>
      OrderDto AddNewOrder(OrderDto order);

      /// <summary>
      ///    Add new softare
      /// </summary>
      /// <param name="softwareDto">The software representation to add</param>
      SoftwareDto AddNewSoftware(SoftwareDto softwareDto);

      /// <summary>
      ///    Add new book
      /// </summary>
      /// <param name="bookDto">The book representation to add</param>
      BookDto AddNewBook(BookDto bookDto);

   }

}