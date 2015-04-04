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
using System.ComponentModel;
using System.ServiceModel;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Helpers;
using Microsoft.Samples.NLayerApp.Presentation.Silverlight.ServiceAgents.Proxies.BankingModule;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels
{
    /// <summary>
    /// ViewModel perform transfer
    /// </summary>
    public class VMPerformTransfer : ObservableObject
    {
        #region Members

        private ICommand _closeCommand;
        private ICommand _transferCommand;
        private ICommand _lockBankAccountCommand;
        private ICommand _refreshActivities;

        private decimal _bankTransferAmount;

        private ObservableCollection<BankAccountDTO> _bankAccounts;
        private ObservableCollection<BankActivityDTO> _bankActivities;
        private BankAccountDTO _bankAccountSource;
        private BankAccountDTO _bankAccountDestination;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the destination bank accounts.
        /// </summary>
        /// <value>
        /// The bank accounts list.
        /// </value>
        public ObservableCollection<BankAccountDTO> BankAccounts
        {
            get { return _bankAccounts; }
            set
            {
                _bankAccounts = value;
                RaisePropertyChanged("BankAccounts");
            }
        }

        /// <summary>
        /// Gets or sets the destination bank accounts.
        /// </summary>
        /// <value>
        /// The bank accounts list.
        /// </value>
        public ObservableCollection<BankActivityDTO> BankActivities
        {
            get { return _bankActivities; }
            set
            {
                _bankActivities = value;
                RaisePropertyChanged("BankActivities");
            }
        }

        /// <summary>
        /// Gets or sets the bank account source.
        /// </summary>
        /// <value>
        /// The bank account source.
        /// </value>
        public BankAccountDTO BankAccountSource
        {
            get { return _bankAccountSource; }
            set
            {
                _bankAccountSource = value;
                RaisePropertyChanged("BankAccountSource");
            }
        }

        /// <summary>
        /// Gets or sets the bank account destination.
        /// </summary>
        /// <value>
        /// The bank account destination.
        /// </value>
        public BankAccountDTO BankAccountDestination
        {
            get { return _bankAccountDestination; }
            set
            {
                _bankAccountDestination = value;
                RaisePropertyChanged("BankAccountDestination");
            }
        }

        /// <summary>
        /// Gets or sets the transfer amount.
        /// </summary>
        /// <value>
        /// The transfer amount.
        /// </value>
        public decimal BankTransferAmount
        {
            get { return _bankTransferAmount; }
            set
            {
                _bankTransferAmount = value;
                RaisePropertyChanged("BankTransferAmount");
            }
        }

        #endregion

        #region Command Properties

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new DelegateCommand(CloseExecute);
                }
                return _closeCommand;
            }
        }

        public ICommand TransferCommand
        {
            get
            {
                if (_transferCommand == null)
                {
                    _transferCommand = new DelegateCommand(TransferExecute);
                }
                return _transferCommand;
            }
        }

        public ICommand LockBankAccountCommand
        {
            get
            {
                if (_lockBankAccountCommand == null)
                {
                    _lockBankAccountCommand = new DelegateCommand(LockAccountExecute);
                }
                return _lockBankAccountCommand;
            }
        }

        public ICommand RefreshActivitiesCommand
        {
            get
            {
                if (_refreshActivities == null)
                {
                    _refreshActivities = new DelegateCommand(RefreshActivitiesExecute);
                }
                return _refreshActivities;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VMPerformTransfer"/> class.
        /// </summary>
        public VMPerformTransfer()
        {
            if (!DesignTimeHelper.IsDesignTime)
                GetBankAccounts();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VMPerformTransfer"/> class.
        /// </summary>
        /// <param name="source">The source account to perform a transfer from.</param>
        public VMPerformTransfer(BankAccountDTO source)
        {
            if (!DesignTimeHelper.IsDesignTime)
            {
                GetBankAccounts();
                BankAccountSource = source;
                RefreshBankAccountsStatus();
            }
        }

        #endregion

        #region Command Methods

        private void CloseExecute()
        {
            VisualStateManager.GoToState(((MainPage)App.Current.RootVisual), "ToTransfers", true);
        }

        private void RefreshActivitiesExecute()
        {
            RefreshBankAccountsStatus();
        }

        private void TransferExecute()
        {
            PerformTransfer();
        }

        private void LockAccountExecute()
        {
            LockAccount();
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Gets the bank accounts.
        /// </summary>
        public void GetBankAccounts()
        {
            try
            {
                var client = new BankingModuleServiceClient();

                client.FindBankAccountsCompleted += delegate(object sender, FindBankAccountsCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        BankAccounts = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("GetBankAccounts: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindBankAccountsAsync();
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("GetBankAccounts: Error at Service:" + excep.ToString());
            }
        }

        /// <summary>
        /// Locks the selected account.
        /// </summary>
        private void LockAccount()
        {
            if (this.BankAccountSource != null)
            {
                try
                {
                    var client = new BankingModuleServiceClient();

                    client.LockBankAccountCompleted += delegate(object sender, LockBankAccountCompletedEventArgs e)
                    {
                        if (e.Error == null)
                        {
                            RefreshBankAccountsStatus();
                        }
                        else if (e.Error is FaultException<ServiceError>)
                        {
                            var fault = e.Error as FaultException<ServiceError>;
                            MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                        }
                        else
                        {
                            Debug.WriteLine("LockAccount: Error at Service:" + e.Error.ToString());
                        }
                    };

                    client.LockBankAccountAsync(BankAccountSource.Id);
                }
                catch (FaultException<ServiceError> excep)
                {
                    Debug.WriteLine("LockAccount: Error at Service:" + excep.ToString());
                }
            }
        }

        /// <summary>
        /// Performs the transfer from the source account to de destination account.
        /// </summary>
        private void PerformTransfer()
        {
            if (BankAccountDestination != null && BankAccountSource != null)
            {
                if (!BankAccountDestination.Locked && !BankAccountSource.Locked)
                {
                    try
                    {
                        var client = new BankingModuleServiceClient();

                        client.PerformTransferCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                                                               {
                                                                   if (e.Error == null)
                                                                   {
                                                                       BankAccountDestination = null;
                                                                       BankTransferAmount = 0;
                                                                       RefreshBankAccountsStatus();
                                                                   }
                                                                   else if (e.Error is FaultException<ServiceError>)
                                                                   {
                                                                       var fault = e.Error as FaultException<ServiceError>;
                                                                       MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                                                                   }
                                                                   else
                                                                   {
                                                                       Debug.WriteLine("PerformTransfer: Error at Service:" + e.Error.ToString());
                                                                   }
                                                               };

                        client.PerformTransferAsync(BankAccountSource, BankAccountDestination, BankTransferAmount);
                    }
                    catch (FaultException<ServiceError> excep)
                    {
                        Debug.WriteLine("PerformTransfer: Error at Service:" + excep.ToString());
                    }
                }
            }
        }


        /// <summary>
        /// Refreshes the bank accounts status.
        /// </summary>
        private void RefreshBankAccountsStatus()
        {
            try
            {
                var client = new BankingModuleServiceClient();

                client.FindBankAccountActivitiesCompleted += delegate(object sender, FindBankAccountActivitiesCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        BankActivities = e.Result;
                    }
                    else if (e.Error is FaultException<ServiceError>)
                    {
                        var fault = e.Error as FaultException<ServiceError>;
                        MessageBox.Show(fault.Detail.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                    else
                    {
                        Debug.WriteLine("RefreshBankAccountsStatus: Error at Service:" + e.Error.ToString());
                    }
                };

                client.FindBankAccountActivitiesAsync(BankAccountSource.Id);
            }
            catch (FaultException<ServiceError> excep)
            {
                Debug.WriteLine("RefreshBankAccountsStatus: Error at Service:" + excep.ToString());
            }
        }

        #endregion
    }
}
