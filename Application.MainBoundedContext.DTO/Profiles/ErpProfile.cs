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

namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO.Profiles
{
    using AutoMapper;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;

    class ErpProfile
        :Profile
    {
        protected override void Configure()
        {
            //book => book dto
            Mapper.CreateMap<Book, BookDTO>();

            //country => countrydto
            Mapper.CreateMap<Country, CountryDTO>();

            //customer => customerlistdto
            Mapper.CreateMap<Customer, CustomerListDTO>();

            //customer => customerdto
            var customerMappingExpression = Mapper.CreateMap<Customer, CustomerDTO>();

            //order => orderlistdto
            var orderListMappingExpression = Mapper.CreateMap<Order, OrderListDTO>();
            orderListMappingExpression.ForMember(dto => dto.TotalOrder, mc => mc.MapFrom(e => e.GetOrderTotal()));
            orderListMappingExpression.ForMember(dto => dto.ShippingAddress, mc => mc.MapFrom(e => e.ShippingInformation.ShippingAddress));
            orderListMappingExpression.ForMember(dto => dto.ShippingCity, mc => mc.MapFrom(e => e.ShippingInformation.ShippingCity));
            orderListMappingExpression.ForMember(dto => dto.ShippingName, mc => mc.MapFrom(e => e.ShippingInformation.ShippingName));
            orderListMappingExpression.ForMember(dto => dto.ShippingZipCode, mc => mc.MapFrom(e => e.ShippingInformation.ShippingZipCode));



            //order => orderdto
            var orderMappingExpression = Mapper.CreateMap<Order, OrderDTO>();

            orderMappingExpression.ForMember(dto => dto.ShippingAddress, (map) => map.MapFrom(o => o.ShippingInformation.ShippingAddress));
            orderMappingExpression.ForMember(dto => dto.ShippingCity, (map) => map.MapFrom(o => o.ShippingInformation.ShippingCity));
            orderMappingExpression.ForMember(dto => dto.ShippingName, (map) => map.MapFrom(o => o.ShippingInformation.ShippingName));
            orderMappingExpression.ForMember(dto => dto.ShippingZipCode, (map) => map.MapFrom(o => o.ShippingInformation.ShippingZipCode));

            //orderline => orderlinedto
            var lineMapperExpression = Mapper.CreateMap<OrderLine, OrderLineDTO>();
            lineMapperExpression.ForMember(dto => dto.Discount, mc => mc.MapFrom(o => o.Discount * 100));

            //product => productdto
            Mapper.CreateMap<Product, ProductDTO>();

            //software => softwaredto
            Mapper.CreateMap<Software, SoftwareDTO>();
        }
    }
}
