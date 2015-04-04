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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Helpers;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModelBase.Messenger;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.ERPModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel order list
    /// </summary>
    public class VMOrderList : ObservableObject
    {
        #region Private fields

        private ICommand _filterCommand;
        private DateTime? _filterFrom;
        private DateTime? _filterTo;
        private ObservableCollection<OrderListDTO> _orders;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filter from start date.
        /// </summary>
        /// <value>
        /// The filter from.
        /// </value>
        public DateTime? FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                RaisePropertyChanged("FilterFrom");
            }
        }

        /// <summary>
        /// Gets or sets the filter to end date.
        /// </summary>
        /// <value>
        /// The filter to.
        /// </value>
        public DateTime? FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                RaisePropertyChanged("FilterTo");
            }
        }

        /// <summary>
        /// Gets or sets the list of orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public ObservableCollection<OrderListDTO> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                RaisePropertyChanged("Orders");
            }
        }

        #endregion

        #region Command Properties

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new DelegateCommand(FilterExecute);
                }
                return _filterCommand;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMOrderList"/> class.
        /// </summary>
        public VMOrderList()
        {
            if (!DesignTimeHelper.IsDesignTime)
            {
                this.GetOrders();
                base.MessageDispatcher.Register(m => GetOrders(), DispatcherMessages.RefreshOrders);
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="VMOrderList"/> is reclaimed by garbage collection.
        /// </summary>
        ~VMOrderList()
        {
            base.MessageDispatcher.Unregister(this, DispatcherMessages.RefreshOrders);
        }

        #endregion

        #region Command Methods

        private void FilterExecute()
        {
            GetOrdersByFilter();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the orders list.
        /// </summary>
        private void GetOrders()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindOrdersInPageCompleted += delegate(object sender, FindOrdersInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Orders = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetOrders: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindOrdersInPageAsync(0, 100);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetOrders: Error at Service:" + excep.ToString());
            }
        }

        /// <summary>
        /// Gets the orders by date filter.
        /// </summary>
        private void GetOrdersByFilter()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindOrdersByFilterCompleted += delegate(object sender, FindOrdersByFilterCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Orders = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetOrders: Error at Service:" + e.Error.ToString());
                    }
                };
                if (FilterFrom.HasValue && FilterTo.HasValue)
                    client.FindOrdersByFilterAsync(FilterFrom.Value, FilterTo.Value);
                else
                    GetOrders();
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetOrders: Error at Service:" + excep.ToString());
            }
        }

        #endregion
    }
}
