//===================================================================================
// Microsoft Developer and Platform Evangelism
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

using Microsoft.Samples.NLayerApp.Domain.Seedwork;

namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg
{

   /// <summary>
   ///    The country entity
   /// </summary>
   public class Country : Entity
   {
      #region Properties
      /// <summary>
      ///    Get or set the Country Name
      /// </summary>
      public string CountryName { get; private set; }

      /// <summary>
      ///    Get or set the Country ISO Code
      /// </summary>
      public string CountryIsoCode { get; private set; }
      #endregion

      #region Constructor

      //required by EF
      private Country()
      {
      }

      public Country(string countryName, string countryIsoCode)
      {
         if (String.IsNullOrWhiteSpace(countryName)) { throw new ArgumentNullException("countryName"); }

         if (String.IsNullOrWhiteSpace(countryIsoCode)) { throw new ArgumentNullException("countryIsoCode"); }

         this.CountryName = countryName;
         this.CountryIsoCode = countryIsoCode;
      }
      #endregion
   }

}