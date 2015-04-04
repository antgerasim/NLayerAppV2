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


namespace Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging
{
    using System;

        /// <summary>
        /// Common contract for trace instrumentation. You 
        /// can implement this contrat with several frameworks.
        /// .NET Diagnostics API, EntLib, Log4Net,NLog etc.
        /// <remarks>
        /// The use of this abstraction depends on the real needs you have and the specific features  
        /// you want to use of a particular existing implementation. 
        ///  You could also eliminate this abstraction and directly use "any" implementation in your code, 
        /// Logger.Write(new LogEntry()) in EntLib, or LogManager.GetLog("logger-name") with log4net... etc.
        /// </remarks>
        /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="message">The debug message</param>
        /// <param name="args">the message argument values</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">Exception to write in debug message</param>
        void Debug(string message,Exception exception,params object[] args);

        /// <summary>
        /// Log debug message 
        /// </summary>
        /// <param name="item">The item with information to write in debug</param>
        void Debug(object item);

        /// <summary>
        /// Log FATAL error
        /// </summary>
        /// <param name="message">The message of fatal error</param>
        /// <param name="args">The argument values of message</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// log FATAL error
        /// </summary>
        /// <param name="message">The message of fatal error</param>
        /// <param name="exception">The exception to write in this fatal message</param>
        void Fatal(string message, Exception exception,params object[] args);

        /// <summary>
        /// Log message information 
        /// </summary>
        /// <param name="message">The information message to write</param>
        /// <param name="args">The arguments values</param>
        void LogInfo(string message, params object[] args);

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="message">The warning message to write</param>
        /// <param name="args">The argument values</param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="args">The arguments values</param>
        void LogError(string message, params object[] args);

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="exception">The exception associated with this error</param>
        /// <param name="args">The arguments values</param>
        void LogError(string message, Exception exception, params object[] args);
    }
}
