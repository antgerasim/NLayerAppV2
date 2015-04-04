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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Helpers;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.ERPModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel Customer
    /// </summary>
    public class VMCustomer : ObservableObject
    {
        #region Members

        private CustomerDTO _currentCustomer;
        private ObservableCollection<OrderListDTO> _currentCustomerOrders;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current customer.
        /// </summary>
        /// <value>
        /// The current customer.
        /// </value>
        public CustomerDTO Customer
        {
            get { return _currentCustomer; }
            set
            {
                _currentCustomer = value;
                RaisePropertyChanged("Customer");
            }
        }

        /// <summary>
        /// Gets or sets the list of customer orders.
        /// </summary>
        /// <value>
        /// The list of customer orders.
        /// </value>
        public ObservableCollection<OrderListDTO> CustomerOrders
        {
            get { return _currentCustomerOrders; }
            set
            {
                _currentCustomerOrders = value;
                RaisePropertyChanged("CustomerOrders");
            }
        }

        /// <summary>
        /// Gets the today date and time.
        /// </summary>
        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMCustomer"/> class.
        /// </summary>
        public VMCustomer()
        {

            editCommand = new DelegateCommand<object>(EditExecute);
            backCommand = new DelegateCommand<object>(BackExecute);
            _currentCustomerOrders = new ObservableCollection<OrderListDTO>();

            if (!DesignTimeHelper.IsDesignTime)
            {
                GetFirstCustomer();
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VMCustomer"/> class.
        /// </summary>
        /// <param name="current">The current customer.</param>
        public VMCustomer(CustomerDTO current)
        {

            editCommand = new DelegateCommand<object>(EditExecute);
            backCommand = new DelegateCommand<object>(BackExecute);
            _currentCustomerOrders = new ObservableCollection<OrderListDTO>();

            if (!DesignTimeHelper.IsDesignTime)
            {
                Customer = current;
                GetCustomerOrders();
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the first customer.
        /// </summary>
        private void GetFirstCustomer()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindCustomersInPageAsync(0, 10);

                client.FindCustomersInPageCompleted += delegate(object sender, FindCustomersInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        ObservableCollection<CustomerListDTO> listCustomers = e.Result;
                        if (listCustomers != null && listCustomers.Count > 0)
                        {
                            if (listCustomers.Any())
                            {
                                var id = listCustomers.First().Id;
                                GetCustomerInfoById(id);
                            }
                        }
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetFirstCustomer: Error at Service:" + e.Error.ToString());
                    }

                };
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetFirstCustomer: Error at Service:" + excep.ToString());
            }
        }

        /// <summary>
        /// Gets the customer info by id.
        /// </summary>
        /// <param name="id">The current customer id.</param>
        private void GetCustomerInfoById(Guid id)
        {
            var client = new ERPModuleServiceClient();
            client.FindCustomerAsync(id);
            client.FindCustomerCompleted += delegate(object s, FindCustomerCompletedEventArgs args)
            {
                if (args.Error == null)
                {
                    Customer = args.Result;
                    GetCustomerOrders();
                }
                else if (args.Error is FaultException<ServiceError>)
                {
                    var fault = args.Error as FaultException<ServiceError>;
                    Debug.WriteLine("GetFirstCustomer: Error at Service:" + fault.ToString());
                }
                else
                {
                    Debug.WriteLine("GetFirstCustomer: Error at Service:" + args.Error.ToString());
                }
            };
        }

        /// <summary>
        /// Gets the current customer orders.
        /// </summary>
        private void GetCustomerOrders()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindOrdersByCustomerAsync(Customer.Id);

                client.FindOrdersByCustomerCompleted += delegate(object sender, FindOrdersByCustomerCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        CustomerOrders = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetCustomerOrders: Error at Service:" + e.Error.ToString());
                    }
                };
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetCustomerOrders: Error at Service:" + excep.ToString());
            }

        }

        #endregion

        #region Commands

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }      

        #endregion

        #region Command Methods

        private void EditExecute(Object o)
        {
            if (o is CustomerDTO)
            {
                CustomerDTO current = (CustomerDTO)o;
                ((MainPage)App.Current.RootVisual).editCustomer.DataContext = new ViewModels.VMEditCustomer(current);
                ((MainPage)App.Current.RootVisual).GotoEditCustomer.Begin();
            }
            Debug.WriteLine("Edit Execute.");
        }

        private void BackExecute(Object o)
        {
            ((MainPage)App.Current.RootVisual).ViewCustomerList.Begin();
        }

        #endregion

    }
}
