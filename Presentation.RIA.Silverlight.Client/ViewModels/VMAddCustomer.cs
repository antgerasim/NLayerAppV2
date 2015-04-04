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
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.ERPModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel Add Customer
    /// </summary>
    public class VMAddCustomer : ObservableObject
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
        /// Gets or sets the customer.
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
                _currentCustomer.CountryId = _selectedCountry.Id;
                _currentCustomer.CountryCountryName = _selectedCountry.CountryName;
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
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get { return _currentCustomer.FirstName; }
            set
            {
                _currentCustomer.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get { return _currentCustomer.LastName; }
            set
            {
                _currentCustomer.LastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address
        {
            get { return _currentCustomer.AddressAddressLine1; }
            set
            {
                _currentCustomer.AddressAddressLine1 = value;
                RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City
        {
            get { return _currentCustomer.AddressCity; }
            set
            {
                _currentCustomer.AddressCity = value;
                RaisePropertyChanged("City");
            }
        }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip
        {
            get { return _currentCustomer.AddressZipCode; }
            set
            {
                _currentCustomer.AddressZipCode = value;
                RaisePropertyChanged("Zip");
            }
        }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        public string Telephone
        {
            get { return _currentCustomer.Telephone; }
            set
            {
                _currentCustomer.Telephone = value;
                RaisePropertyChanged("Telephone");
            }
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
                    _saveCommand = new DelegateCommand(SaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMAddCustomer"/> class.
        /// </summary>
        public VMAddCustomer()
        {
            //create default
            CreateDefault();

            //get countries
            GetCountries();
        }

        

        #endregion

        #region Command Methods

        private void CancelExecute()
        {
            ((MainPage)App.Current.RootVisual).GoBackAddCustomer.Begin();
        }

        private void SaveExecute()
        {
            if (SelectedCountry == null) return;
            try
            {
                var client = new ERPModuleServiceClient();
                client.AddNewCustomerCompleted += delegate(object sender, AddNewCustomerCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        ((VMCustomerListView)((MainPage)App.Current.RootVisual).viewListCustomers.DataContext).GetCustomers();
                        ((MainPage)App.Current.RootVisual).GoBackAddCustomer.Begin();
                        Customer = null;
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
                    
                };
                client.AddNewCustomerAsync(_currentCustomer);

            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("SaveExecute: Error at Service:" + excep.ToString());
            }
        }

        #endregion

        #region Private Methods

        void CreateDefault()
        {
            //create default customer information
            _currentCustomer = new CustomerDTO();
            CompanyName = "Company Name Here...";
        }

        /// <summary>
        /// Gets the country list.
        /// </summary>
        void GetCountries()
        {
            try
            {
                var client = new ERPModuleServiceClient();
                client.FindCountriesInPageCompleted += delegate(object sender, FindCountriesInPageCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        Countries = e.Result;
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
