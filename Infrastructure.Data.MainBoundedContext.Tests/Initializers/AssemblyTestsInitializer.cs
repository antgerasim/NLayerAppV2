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
			
namespace Infrastructure.Data.MainBoundedContext.Tests.Initializers
{
    using System.Data.Entity;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssemblyTestsInitialize
    {
        /// <summary>
        /// In this beta, the unit of work initializer is MainBCUnitOfWorkInitializer
        /// </summary>
        /// <param name="context">The MS TEST context</param>
        [AssemblyInitialize()]
        public static void RebuildUnitOfWork(TestContext context)
        {
            //Set default initializer for MainBCUnitOfWork
            Database.SetInitializer<MainBCUnitOfWork>(new MainBCUnitOfWorkInitializer());
        }
    }
}
