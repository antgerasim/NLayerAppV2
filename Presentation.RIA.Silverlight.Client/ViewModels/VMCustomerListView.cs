
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
    /// ViewModel Customer List
    /// </summary>
    public class VMCustomerListView : ObservableObject
    {

        #region Members

        private CustomerListDTO _currentCustomer;
        private ObservableCollection<CustomerListDTO> _customers;
        private ICommand _nextPageCommand;
        private ICommand _previousPageCommand;
        private int _pageIndex = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selected customer.
        /// </summary>
        /// <value>
        /// The selected customer.
        /// </value>
        public CustomerListDTO SelectedCustomer
        {
            get { return _currentCustomer; }
            set
            {
                _currentCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
            }
        }

        /// <summary>
        /// Gets or sets the customer list.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public ObservableCollection<CustomerListDTO> Customers
        {
            get { return _customers; }
            set
            {
                _customers.Clear();
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMCustomerListView"/> class.
        /// </summary>
        public VMCustomerListView()
        {
            deleteCommand = new DelegateCommand<object>(DeleteExecute);
            editCommand = new DelegateCommand<object>(EditExecute);
            viewCommand = new DelegateCommand<object>(ViewExecute);
            addCommand = new DelegateCommand<object>(AddExecute);
            searchCommand = new DelegateCommand<object>(SearchExecute);

            _customers = new ObservableCollection<CustomerListDTO>();
            this._currentCustomer = new CustomerListDTO();

            if (!DesignTimeHelper.IsDesignTime)
            {
                GetCustomers();
            }

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the customer list.
        /// </summary>
        public void GetCustomers()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindCustomersInPageCompleted += delegate(object sender, FindCustomersInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        ObservableCollection<CustomerListDTO> listCustomers = e.Result;
                        if (listCustomers != null)
                        {
                            if (listCustomers.Count > 0)
                            {
                                Customers = listCustomers;
                            }
                            if (listCustomers.Count == 0)
                            {
                                if (this.CurrentPage > 1)
                                {
                                    this.CurrentPage--;
                                }
                            }
                        }
                        else
                        {
                            if (this.CurrentPage > 1)
                            {
                                this.CurrentPage--;
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("GetCustomers: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindCustomersInPageAsync(this.CurrentPage, 4);
            }
            catch (Exception excep)
            {
                Debug.WriteLine("GetCustomers: Error at Service:" + excep.ToString());
            }
        }

        /// <summary>
        /// Search a customer by name.
        /// </summary>
        /// <param name="name">The customer name or part of them.</param>
        public void SearchCustomers(string name)
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindCustomersByFilterCompleted += delegate(object sender, FindCustomersByFilterCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        ObservableCollection<CustomerListDTO> listCustomers = e.Result;
                        if (listCustomers != null && listCustomers.Count > 0)
                        {
                            Customers = listCustomers;
                        }
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("SearchCustomers: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindCustomersByFilterAsync(name);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("SearchCustomers: Error at Service:" + excep.ToString());
            }
        }

        /// <summary>
        /// Deletes the current selected customer.
        /// </summary>
        /// <param name="customer">The current customer to delete.</param>
        public void DeleteCustomers(CustomerDTO customer)
        {
            try
            {
                //remove customer
                var client = new ERPModuleServiceClient();

                client.RemoveCustomerCompleted += delegate(object o, AsyncCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        //refresh customer list
                        this.GetCustomers();
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("DeleteCustomers: Error at Service:" + e.Error.ToString());
                    }
                };

                client.RemoveCustomerAsync(customer.Id);
            }
            catch (FaultException<ServiceError> ex)
            {
                Debug.WriteLine("DeleteCustomers: Error at Service:" + ex.ToString());
            }
        }

        #endregion

        #region Commands Definitions

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get { return searchCommand; }
            set { searchCommand = value; }
        }

        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand; }
            set { addCommand = value; }
        }

        public ICommand NextPageCommand
        {
            get
            {
                if (_nextPageCommand == null)
                {
                    _nextPageCommand = new DelegateCommand(NextPageExecute);
                }
                return _nextPageCommand;
            }
        }

        public ICommand PreviousPageCommand
        {
            get
            {
                if (_previousPageCommand == null)
                {
                    _previousPageCommand = new DelegateCommand(PreviousPageExecute);
                }
                return _previousPageCommand;
            }
        }

        #endregion

        #region Commands Actions

        private void EditExecute(Object o)
        {
            if (o is CustomerListDTO)
            {
                var current = (CustomerListDTO)o;
                {
                    ((MainPage)App.Current.RootVisual).editCustomer.DataContext = new ViewModels.VMEditCustomer(current.Id);
                    ((MainPage)App.Current.RootVisual).GotoEditCustomer.Begin();
                }
            }
            Debug.WriteLine("Edit Execute.");
        }

        private void DeleteExecute(Object o)
        {
            if (o == null) return;

            try
            {
                if (o is CustomerListDTO)
                {
                    var current = (CustomerListDTO)o;
                    var client = new ERPModuleServiceClient();
                    client.RemoveCustomerCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                    {
                        if (e.Error == null)
                        {
                            GetCustomers();
                        }
                        else if (e.Error is FaultException<ServiceError>)
                        {
                            var fault = e.Error as FaultException<ServiceError>;
                            MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                        }
                        else
                        {
                            Debug.WriteLine("DeleteExecute: Error at Service:" + e.Error.ToString());
                        }
                    };

                    client.RemoveCustomerAsync(current.Id);
                }
                Debug.WriteLine("Delete Customer.");
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("DeleteExecute: Error at Service:" + excep.ToString());
            }

        }

        private void SearchExecute(object param)
        {
            if (param is string)
            {
                this.CurrentPage = 0;
                SearchCustomers(param as string);
            }

            Debug.WriteLine("Search Customer.");
        }

        private void ViewExecute(Object o)
        {
            try
            {
                if (o is CustomerListDTO)
                {
                    var current = (CustomerListDTO)o;
                    var client = new ERPModuleServiceClient();

                    client.FindCustomerCompleted += delegate(object sender, FindCustomerCompletedEventArgs e)
                    {
                        if (e.Error == null)
                        {
                            if (e.Result != null)
                            {
                                ((MainPage)App.Current.RootVisual).viewCustomer.DataContext = new VMCustomer(e.Result);
                                ((MainPage)App.Current.RootVisual).ViewCustomer.Begin();
                            }
                        }
                        else if (e.Error is FaultException<ServiceError>)
                        {
                            var fault = e.Error as FaultException<ServiceError>;
                            MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                        }
                        else
                        {
                            Debug.WriteLine("ViewExecute: Error at Service:" + e.Error.ToString());
                        }
                    };

                    client.FindCustomerAsync(current.Id);
                }
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("ViewExecute: Error at Service:" + excep.ToString());
            }
        }

        private void AddExecute(Object o)
        {
            ((MainPage)App.Current.RootVisual).addCustomer.DataContext = new VMAddCustomer();
            ((MainPage)App.Current.RootVisual).GotoAddCustomer.Begin();

            Debug.WriteLine("Add Customer.");
        }

        #endregion

        #region Pagination Methods

        private void NextPageExecute()
        {
            this.CurrentPage++;
            this.GetCustomers();
        }

        private void PreviousPageExecute()
        {
            this.CurrentPage--;
            if (this.CurrentPage < 0) this.CurrentPage = 0;
            this.GetCustomers();
        }

        #endregion

    }

}
