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

using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Adapter;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Adapter;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator;
using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.MainBoundedContext.Tests
{

   [TestClass]
   public class AssemblyTestsInitialize
   {

      /// <summary>
      ///    Initialize all factories for tests
      /// </summary>
      /// <param name="context">The MS TEST context</param>
      [AssemblyInitialize()]
      public static void InitializeFactories(TestContext context)
      {

         LoggerFactory.SetCurrent(new TraceSourceLogFactory());

         EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());

         var dto = new CountryDto();
         // this is only to force  current domain to load de .DTO assembly and all profiles

         var adapterfactory = new AutomapperTypeAdapterFactory();
         TypeAdapterFactory.SetCurrent(adapterfactory);
      }

   }

}