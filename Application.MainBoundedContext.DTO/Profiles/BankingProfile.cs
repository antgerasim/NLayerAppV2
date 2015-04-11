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
using AutoMapper;

using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;

namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO.Profiles
{

   internal class BankingProfile : Profile
   {

      protected override void Configure()
      {
         //bankAccount => BankAccountDTO
         var map = Mapper.CreateMap<BankAccount, BankAccountDto>();
         map.ForMember(dto => dto.BankAccountNumber, mc => mc.MapFrom(e => e.Iban));

         //bankAccountActivity=>bankaccountactivityDTO
         Mapper.CreateMap<BankAccountActivity, BankActivityDto>();
      }

   }

}