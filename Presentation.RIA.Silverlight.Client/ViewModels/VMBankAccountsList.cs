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
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Collections.ObjectModel;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Helpers;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.BankingModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel transfer list
    /// </summary>
    public class VMBankAccountsList : ObservableObject
    {
        #region Members

        private ICommand _searchCommand;
        private ICommand _addCommand;

        private string _filterCompanyName;

        private ObservableCollection<BankAccountDTO> _accounts;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the filter customer.
        /// </summary>
        /// <value>
        /// The name of the filter customer.
        /// </value>
        public string FilterCustomerName
        {
            get { return _filterCompanyName; }
            set
            {
                _filterCompanyName = value;
                RaisePropertyChanged("FilterCustomerName");
            }
        }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        public ObservableCollection<BankAccountDTO> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                RaisePropertyChanged("Accounts");
            }
        }

        #endregion

        #region Command Properties

        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new DelegateCommand(SearchExecute);
                }
                return _searchCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new DelegateCommand<BankAccountDTO>(AddExecute);
                }
                return _addCommand;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMBankAccountsList"/> class.
        /// </summary>
        public VMBankAccountsList()
        {
            if (!DesignTimeHelper.IsDesignTime)
            {
                GetTransfers();
            }
        }

        #endregion

        #region Command Methods

        private void SearchExecute()
        {
            GetTransfers(FilterCustomerName);
        }

        private void AddExecute(BankAccountDTO selected)
        {
            if (selected == null) return;

            ((MainPage)App.Current.RootVisual).PerformTransfer.DataContext = new VMPerformTransfer(selected);
            VisualStateManager.GoToState(((MainPage)App.Current.RootVisual), "ToAddTransfer", true);
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Gets the transfers.
        /// </summary>
        private void GetTransfers()
        {
            GetTransfers(string.Empty);
        }


        /// <summary>
        /// Gets the transfer list.
        /// </summary>
        /// <param name="customerName">Name of the customer.</param>
        private void GetTransfers(string customerName)
        {
            try
            {
                var client = new BankingModuleServiceClient();

                client.FindBankAccountsCompleted += delegate(object sender, FindBankAccountsCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        if (String.IsNullOrWhiteSpace(customerName))
                        {
                            Accounts = e.Result;
                        }
                        else
                        {
                            var search = from a in e.Result
                                         where
                                             a.CustomerFirstName.ToLowerInvariant().Contains(customerName.ToLowerInvariant()) ||
                                             a.CustomerLastName.ToLowerInvariant().Contains(customerName.ToLowerInvariant())
                                         select a;
                            Accounts = new ObservableCollection<BankAccountDTO>(search.ToList());
                        }
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetTransfers: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindBankAccountsAsync();
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetTransfers: Error at Service:" + excep.ToString());
            }
        }

        #endregion
    }
}
