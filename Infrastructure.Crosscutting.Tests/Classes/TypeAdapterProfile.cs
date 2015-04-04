
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

namespace Infrastructure.Crosscutting.Tests.Classes
{
    using AutoMapper;

    class TypeAdapterProfile
        :Profile
    {
        protected override void  Configure()
        {
            var map = Mapper.CreateMap<Customer, CustomerDTO>();
            map.ForMember(dto => dto.CustomerId, mc => mc.MapFrom(e => e.Id));
            map.ForMember(dto => dto.FullName, mc => mc.MapFrom(e => string.Format("{0},{1}",e.LastName,e.FirstName)));
        }
    }
}
