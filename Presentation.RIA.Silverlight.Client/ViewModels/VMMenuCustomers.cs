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
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel menu customers
    /// </summary>
    public class VMMenuCustomers
        : ObservableObject
    {
        #region Declarations

        private ICommand _addCustomerCommand;
        private ICommand _viewCustomersCommand;

        #endregion

        #region Properties

        public string actualState
        {
            get { return ((MainPage)App.Current.RootVisual).actualState; }
            set { ((MainPage)App.Current.RootVisual).actualState = value; }
        }

        public MainPage mainPage
        {
            get { return ((MainPage)App.Current.RootVisual); }
        }

        #endregion

        #region Command Properties

        public ICommand AddCommand
        {
            get
            {
                if (_addCustomerCommand == null)
                {
                    _addCustomerCommand = new DelegateCommand(AddCustomerExecute, CanAddCustomerExecute);
                }
                return _addCustomerCommand;
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                if (_viewCustomersCommand == null)
                {
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
            mainPage.addCustomer.DataContext = new VMAddCustomer();
            mainPage.GotoAddCustomer.Begin();
        }

        private bool CanViewCustomersExecute()
        {
            return true;
        }

        private void ViewCustomersExecute()
        {
            ((VMCustomerListView)mainPage.viewListCustomers.DataContext).GetCustomers();
            mainPage.GoBackAddCustomer.Begin();
        }

        #endregion


    }
}
