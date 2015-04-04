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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Helpers;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModelBase.Messenger;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.ERPModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel perform order
    /// </summary>
    public class VMPerformOrder : ObservableObject
    {
        #region Private fields

        private ICommand _saveCommand;
        private ICommand _addCommand;

        private OrderDTO _currentOrder;
        private ObservableCollection<CustomerListDTO> _customers;
        private CustomerListDTO _selectedCustomer;

        private OrderLineDTO _selectedOrderLine = new OrderLineDTO() { Amount = 1 };
        private ObservableCollection<ProductDTO> _products;
        private ProductDTO _selectedProduct;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the new order.
        /// </summary>
        /// <value>
        /// The new order.
        /// </value>
        public OrderDTO Order
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                RaisePropertyChanged("Order");
                RaisePropertyChanged("OrderLines");
                RaisePropertyChanged("ShippingName");
                RaisePropertyChanged("DeliveryDate");
                RaisePropertyChanged("ShippingCity");
                RaisePropertyChanged("ShippingAddress");
                RaisePropertyChanged("ShippingZip");
            }
        }

        /// <summary>
        /// Gets or sets the selected customer.
        /// </summary>
        /// <value>
        /// The selected customer.
        /// </value>
        public CustomerListDTO SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
            }
        }

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customer list.
        /// </value>
        public ObservableCollection<CustomerListDTO> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        /// <summary>
        /// Gets or sets the name of the shipping.
        /// </summary>
        /// <value>
        /// The name of the shipping.
        /// </value>
        public string ShippingName
        {
            get { return _currentOrder.ShippingName; }
            set
            {
                _currentOrder.ShippingName = value;
                RaisePropertyChanged("ShippingName");
            }
        }


        /// <summary>
        /// Gets or sets the order lines.
        /// </summary>
        /// <value>
        /// The order lines.
        /// </value>
        public ObservableCollection<OrderLineDTO> OrderLines
        {
            get { return _currentOrder.OrderLines; }
            set
            {
                _currentOrder.OrderLines = value;
                RaisePropertyChanged("OrderLines");
            }
        }

        /// <summary>
        /// Gets or sets the selected order line.
        /// </summary>
        /// <value>
        /// The selected order line.
        /// </value>
        public OrderLineDTO SelectedOrderLine
        {
            get { return _selectedOrderLine; }
            set
            {
                _selectedOrderLine = value;
                RaisePropertyChanged("SelectedOrderLine");
                RaisePropertyChanged("Discount");
                RaisePropertyChanged("Amount");
            }
        }

        /// <summary>
        /// Gets or sets the discount to the current order line.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount
        {
            get { return _selectedOrderLine.Discount; }
            set
            {
                _selectedOrderLine.Discount = value;
                RaisePropertyChanged("Discount");
            }
        }

        /// <summary>
        /// The public property Amount stores...
        /// </summary>
        public int Amount
        {
            get { return _selectedOrderLine.Amount; }
            set
            {
                _selectedOrderLine.Amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        /// <summary>
        /// The public property Products stores...
        /// </summary>
        public ObservableCollection<ProductDTO> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                RaisePropertyChanged("Products");
            }
        }

        /// <summary>
        /// The new product to be add
        /// </summary>
        public ProductDTO SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }

        /// <summary>
        /// Gets or sets the delivery date.
        /// </summary>
        /// <value>
        /// The delivery date.
        /// </value>
        public DateTime? DeliveryDate
        {
            get { return _currentOrder.DeliveryDate; }
            set
            {
                _currentOrder.DeliveryDate = value;
                RaisePropertyChanged("DeliveryDate");
            }
        }

        /// <summary>
        /// Gets or sets the shipping city.
        /// </summary>
        /// <value>
        /// The shipping city.
        /// </value>
        public string ShippingCity
        {
            get { return _currentOrder.ShippingCity; }
            set
            {
                _currentOrder.ShippingCity = value;
                RaisePropertyChanged("ShippingCity");
            }
        }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        /// <value>
        /// The shipping address.
        /// </value>
        public string ShippingAddress
        {
            get { return _currentOrder.ShippingAddress; }
            set
            {
                _currentOrder.ShippingAddress = value;
                RaisePropertyChanged("ShippingAddress");
            }
        }

        /// <summary>
        /// Gets or sets the shipping zip.
        /// </summary>
        /// <value>
        /// The shipping zip.
        /// </value>
        public string ShippingZip
        {
            get { return _currentOrder.ShippingZipCode; }
            set
            {
                _currentOrder.ShippingZipCode = value;
                RaisePropertyChanged("ShippingZip");
            }
        }
        #endregion

        #region Command Properties

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand(SaveExecute);
                }
                return _saveCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new DelegateCommand(AddExecute);
                }
                return _addCommand;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMPerformOrder"/> class.
        /// </summary>
        public VMPerformOrder()
        {
            if (!DesignTimeHelper.IsDesignTime)
            {
                this._currentOrder = new OrderDTO();
                GetCustomers();
                GetProducts();
            }
        }

        #endregion

        #region Command Methods

        private void SaveExecute()
        {
            if (SelectedCustomer == null) return;

            try
            {
                var client = new ERPModuleServiceClient();

                client.AddNewOrderCompleted += delegate(object sender, AddNewOrderCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (e.Result == null)
                        {
                            MessageBox.Show("Order exceeds the maximum allowed amount", "Unable to perform the order", MessageBoxButton.OK);
                        }
                        else
                        {
                            Order = new OrderDTO();
                            SelectedCustomer = null;
                            VisualStateManager.GoToState(((MainPage)App.Current.RootVisual), "ToOrderList", true);
                            MessageDispatcher.NotifyColleagues(DispatcherMessages.RefreshOrders,null);
                        }
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

                Order.CustomerId = SelectedCustomer.Id;
                Order.OrderDate = DateTime.Now;

                client.AddNewOrderAsync(Order);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetOrders: Error at Service:" + excep.ToString());
            }
        }

        private void AddExecute()
        {
            var newOrder = new OrderLineDTO();
            newOrder.ProductId = SelectedProduct.Id;
            newOrder.ProductTitle = SelectedProduct.Title;
            newOrder.UnitPrice = SelectedProduct.UnitPrice;
            newOrder.Amount = Amount;
            newOrder.Discount = Discount;

            if (OrderLines == null) OrderLines = new ObservableCollection<OrderLineDTO>();
            OrderLines.Add(newOrder);

            SelectedOrderLine = new OrderLineDTO() { Amount = 1 };
            SelectedProduct = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the customer list.
        /// </summary>
        private void GetCustomers()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindCustomersInPageCompleted += delegate(object sender, FindCustomersInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Customers = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetCustomers: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindCustomersInPageAsync(0, 100);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetCustomers: Error at Service:" + excep.ToString());
            }
        }

        /// <summary>
        /// Gets the product list.
        /// </summary>
        private void GetProducts()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindProductsInPageCompleted += delegate(object sender, FindProductsInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Products = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetProducts: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindProductsInPageAsync(0, 100);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetProducts: Error at Service:" + excep.ToString());
            }
        }

        #endregion
    }
}
