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
    /// ViewModel Menu orders
    /// </summary>
    public class VMMenuOrders
        : ObservableObject
    {

        #region Private fields

        private ICommand _addOrderCommand;
        private ICommand _viewOrdersCommand;

        #endregion

        #region Properties

        public string ActualState
        {
            get { return ((MainPage)App.Current.RootVisual).actualState; }
            set { ((MainPage)App.Current.RootVisual).actualState = value; }
        }

        public MainPage MainPage
        {
            get { return ((MainPage)App.Current.RootVisual); }
        }

        #endregion

        #region Command Properties

        public ICommand AddCommand
        {
            get
            {
                if (_addOrderCommand == null)
                {
                    _addOrderCommand = new DelegateCommand(AddOrderExecute, CanAddOrderExecute);
                }
                return _addOrderCommand;
            }
        }

        public ICommand ViewCommand
        {
            get
            {
                if (_viewOrdersCommand == null)
                {
                    _viewOrdersCommand = new DelegateCommand(ViewOrdersExecute, CanViewOrdersExecute);
                }
                return _viewOrdersCommand;
            }
        }

        #endregion

        #region Command Methods

        private bool CanAddOrderExecute()
        {
            return true;
        }

        private void AddOrderExecute()
        {
            VisualStateManager.GoToState(MainPage, "ToAddOrder", true);
        }

        private bool CanViewOrdersExecute()
        {
            return true;
        }

        private void ViewOrdersExecute()
        {
            VisualStateManager.GoToState(MainPage, "ToOrderList", true);
        }

        #endregion

    }
}
