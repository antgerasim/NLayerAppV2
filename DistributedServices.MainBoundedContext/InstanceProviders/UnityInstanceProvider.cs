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

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using Microsoft.Practices.Unity;

namespace Microsoft.Samples.NLayerApp.DistributedServices.MainBoundedContext.InstanceProviders
{

   /// <summary>
   ///    The unity instance provider. This class provides
   ///    an extensibility point for creating instances of wcf
   ///    service.
   ///    <remarks>
   ///       The goal is to inject dependencies from the inception point
   ///    </remarks>
   /// </summary>
   public class UnityInstanceProvider : IInstanceProvider
   {
      #region Constructor
      /// <summary>
      ///    Create a new instance of unity instance provider
      /// </summary>
      /// <param name="serviceType">The service where we apply the instance provider</param>
      public UnityInstanceProvider(Type serviceType)
      {
         if (serviceType == null) { throw new ArgumentNullException("serviceType"); }

         _serviceType = serviceType;
         _container = Container.Current;
      }
      #endregion

      #region Members
      private Type _serviceType;
      private IUnityContainer _container;
      #endregion

      #region IInstance Provider Members
      /// <summary>
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </summary>
      /// <param name="instanceContext">
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </param>
      /// <param name="message">
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </param>
      /// <returns>
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </returns>
      public object GetInstance(InstanceContext instanceContext, Message message)
      {
         //This is the only call to UNITY container in the whole solution
         return _container.Resolve(_serviceType);
      }

      /// <summary>
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </summary>
      /// <param name="instanceContext">
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </param>
      /// <returns>
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </returns>
      public object GetInstance(InstanceContext instanceContext)
      {
         return GetInstance(instanceContext, null);
      }

      /// <summary>
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </summary>
      /// <param name="instanceContext">
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </param>
      /// <param name="instance">
      ///    <see cref="System.ServiceModel.Dispatcher.IInstanceProvider" />
      /// </param>
      public void ReleaseInstance(InstanceContext instanceContext, object instance)
      {
         if (instance is IDisposable) { ((IDisposable) instance).Dispose(); }
      }
      #endregion
   }

}