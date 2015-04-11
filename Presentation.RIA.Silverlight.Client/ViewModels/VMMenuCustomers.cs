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
//====================================================================================
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModelBase;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{

   /// <summary>
   ///    ViewModel menu customers
   /// </summary>
   public class VmMenuCustomers : ObservableObject
   {
      #region Declarations
      private ICommand _addCustomerCommand;
      private ICommand _viewCustomersCommand;
      #endregion

      #region Properties
      public string ActualState
      {
         get
         {
            return ((MainPage) App.Current.RootVisual).ActualState;
         }
         set
         {
            ((MainPage) App.Current.RootVisual).ActualState = value;
         }
      }

      public MainPage MainPage
      {
         get
         {
            return ((MainPage) App.Current.RootVisual);
         }
      }
      #endregion

      #region Command Properties
      public ICommand AddCommand
      {
         get
         {
            if (_addCustomerCommand == null) {
               _addCustomerCommand = new DelegateCommand(AddCustomerExecute, CanAddCustomerExecute);
            }
            return _addCustomerCommand;
         }
      }

      public ICommand ViewCommand
      {
         get
         {
            if (_viewCustomersCommand == null) {
               _viewCustomersCommand = new DelegateCommand(ViewCustomersExecute, CanViewCustomersExecute);
            }
            return _viewCustomersCommand;
         }
      }
      #endregion

      #region Constructors
      #endregion

      #region Command Methods
      private bool CanAddCustomerExecute()
      {
         return true;
      }

      private void AddCustomerExecute()
      {
         MainPage.addCustomer.DataContext = new VmAddCustomer();
         MainPage.GotoAddCustomer.Begin();
      }

      private bool CanViewCustomersExecute()
      {
         return true;
      }

      private void ViewCustomersExecute()
      {
         ((VmCustomerListView) MainPage.ViewListCustomers.DataContext).GetCustomers();
         MainPage.GoBackAddCustomer.Begin();
      }
      #endregion
   }

}