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
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModelBase;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{

   /// <summary>
   ///    ViewModel menu
   /// </summary>
   public class VmMenu : ObservableObject
   {
      #region Constructors
      /// <summary>
      ///    Initializes a new instance of the <see cref="VmMenu" /> class.
      /// </summary>
      public VmMenu()
      {
         _customers = new VmMenuCustomers();
         _banking = new VmMenuBanking();
         _orders = new VmMenuOrders();
      }
      #endregion

      #region Properties
      private VmMenuCustomers _customers;

      /// <summary>
      ///    Gets or sets the customers menu viewmodel.
      /// </summary>
      /// <value>
      ///    The customers.
      /// </value>
      public VmMenuCustomers Customers
      {
         get
         {
            return _customers;
         }
         set
         {
            _customers = value;
            RaisePropertyChanged("Customers");
         }
      }

      private VmMenuOrders _orders;

      /// <summary>
      ///    Gets or sets the orders menu viewmodel.
      /// </summary>
      /// <value>
      ///    The orders.
      /// </value>
      public VmMenuOrders Orders
      {
         get
         {
            return _orders;
         }
         set
         {
            _orders = value;
            RaisePropertyChanged("Orders");
         }
      }

      private VmMenuBanking _banking;

      /// <summary>
      ///    Gets or sets the banking menu viewmodel.
      /// </summary>
      /// <value>
      ///    The banking.
      /// </value>
      public VmMenuBanking Banking
      {
         get
         {
            return _banking;
         }
         set
         {
            _banking = value;
            RaisePropertyChanged("Banking");
         }
      }
      #endregion
   }

}