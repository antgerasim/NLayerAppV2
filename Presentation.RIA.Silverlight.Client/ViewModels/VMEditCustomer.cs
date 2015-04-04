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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.ERPModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel Edit Customer
    /// </summary>
    public class VMEditCustomer : ObservableObject
    {
        #region Members

        private ICommand _cancelCommand;
        private ICommand _saveCommand;

        private CustomerDTO _currentCustomer;
        private ObservableCollection<CountryDTO> _countries;
        private CountryDTO _selectedCountry;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current customer.
        /// </summary>
        /// <value>
        /// The customer.
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
        /// Gets or sets the avaliable countries.
        /// </summary>
        /// <value>
        /// The country list.
        /// </value>
        public ObservableCollection<CountryDTO> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                RaisePropertyChanged("Countries");
            }
        }


        public CountryDTO SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                if (_selectedCountry != null)
                {
                    _currentCustomer.CountryId = _selectedCountry.Id;
                    _currentCustomer.CountryCountryName = _selectedCountry.CountryName;
                }              
                RaisePropertyChanged("SelectedCountry");
            }
        }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName
        {
            get { return _currentCustomer.Company; }
            set
            {
                _currentCustomer.Company = value;
                RaisePropertyChanged("CompanyName");
            }
        }

        /// <summary>
        /// Gets the name of the contact.
        /// </summary>
        /// <value>
        /// The name of the contact.
        /// </value>
        public string ContactName
        {
            get { return string.Format("{0} {1}",_currentCustomer.FirstName, _currentCustomer.LastName); }
        }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public byte[] Photo
        {
            get
            {
                return _currentCustomer.PictureRawPhoto;
            }
            set
            {
                _currentCustomer.PictureRawPhoto = value;
                RaisePropertyChanged("Photo");
            }
        }

        #endregion

        #region Command Properties

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new DelegateCommand(CancelExecute);
                }
                return _cancelCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand<string>(SaveExecute, CanSaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMEditCustomer"/> class.
        /// </summary>
        public VMEditCustomer()
        {
            _currentCustomer = new CustomerDTO();
            GetCountries();
        }

        public VMEditCustomer(CustomerDTO current)
        {
            Customer = current;
            GetCountries();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VMEditCustomer"/> class.
        /// </summary>
        /// <param name="customerCode">The customer code of the current user.</param>
        public VMEditCustomer(Guid customerCode)
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindCustomerCompleted += delegate(object sender, FindCustomerCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (e.Result != null)
                        {
                            Customer = e.Result;
                        }
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("VMEditCustomer: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindCustomerAsync(customerCode);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("VMEditCustomer: Error at Service:" + excep.ToString());
            }           
        }

        #endregion

        #region Command Methods

        private void CancelExecute()
        {
            try
            {
                var client = new ERPModuleServiceClient();

                client.FindCustomerCompleted += delegate(object sender, FindCustomerCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (e.Result != null)
                        {
                            Customer = e.Result;
                        }
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("CancelExecute: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindCustomerAsync(Customer.Id);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("CancelExecute: Error at Service:" + excep.ToString());
            }

            ((MainPage)App.Current.RootVisual).GoBackEditCustomer.Begin();
        }

        private void SaveExecute(string isValidData)
        {
            try
            {
                var client = new ERPModuleServiceClient();
                client.UpdateCustomerCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        ((VMCustomer)((MainPage)App.Current.RootVisual).viewCustomer.DataContext).Customer = Customer;
                        ((VMCustomerListView)((MainPage)App.Current.RootVisual).viewListCustomers.DataContext).GetCustomers();
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("SaveExecute: Error at Service:" + e.Error.ToString());
                    }

                    ((MainPage)App.Current.RootVisual).GoBackEditCustomer.Begin();
                };

                client.UpdateCustomerAsync(Customer);

            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("SaveExecute: Error at Service:" + excep.ToString());
            }
        }

        private bool CanSaveExecute(string isValidData)
        {
            return (this.Customer != null);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the country list.
        /// </summary>
        private void GetCountries()
        {
            try
            {
                var client = new ERPModuleServiceClient();
                client.FindCountriesInPageCompleted += delegate(object sender, FindCountriesInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Countries = e.Result;
                        SelectedCountry = Countries.Where(c=> c.Id == _currentCustomer.CountryId).FirstOrDefault();
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetCountries: Error at Service:" + e.Error.ToString());
                    }

                };
                client.FindCountriesInPageAsync(0, 100);

            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetCountries: Error at Service:" + excep.ToString());
            }
        }

        #endregion
    }
}
