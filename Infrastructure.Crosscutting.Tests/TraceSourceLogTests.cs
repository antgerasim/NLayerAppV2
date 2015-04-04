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
			

namespace Infrastructure.Crosscutting.Tests
{
    using System;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Dynamic;

    [TestClass]
    public partial class TraceSourceLogTest
    {
        #region Class Initialize

        [ClassInitialize()]
        public static void ClassInitialze(TestContext context)
        {
            // Initialize default log factory
            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
        }

        #endregion

        [TestMethod()]
        public void LogInfo()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.LogInfo("{0}","the info message"); 
        }
        [TestMethod()]
        public void LogWarning()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.LogWarning("{0}","the warning message");
        }
        [TestMethod()]
        public void LogError()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.LogError("{0}", "the error message"); 
        }
            
        [TestMethod()]
        public void LogErrorWithException()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.LogError("{0}", new ArgumentNullException("param"), "the error message");
        }
        [TestMethod()]
        public void LogDebugWithObject()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.Debug(new object());
        }
        [TestMethod()]
        public void LogDebugWithMessage()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.Debug("{0}","the debug mesage");
        }
        [TestMethod()]
        public void LogDebugWithException()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.Debug("{0}", new ArgumentNullException("param"),"the debug mesage");
        }
        [TestMethod()]
        public void LogFatalWithMessage()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.Fatal("{0}", "the debug mesage");
        }
        [TestMethod()]
        public void LogFatalWithException()
        {
            //Arrange
            ILogger log = LoggerFactory.CreateLog();

            //Act
            log.Fatal("{0}", new ArgumentNullException("param"), "the debug mesage");
        }

        
    }
}
