//===================================================================================
// Microsoft Developer and Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================


namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.Resources;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;

    /// <summary>
    /// Aggregate root for Customer Aggregate.
    /// </summary>
    public class Customer
        :Entity,IValidatableObject
    {

        #region Members

        bool _IsEnabled;

        #endregion

        #region Properties

        
        /// <summary>
        /// Get or set the Given name of this customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Get or set the surname of this customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get or set the full name of this customer
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", this.LastName, this.FirstName);
            }
            set { } 

        }

        /// <summary>
        /// Get or set the telephone 
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Get or set the company name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Get or set the address of this customer
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// Get or set the current credit limit for this customer
        /// </summary>
        public decimal CreditLimit { get; private set; }

        /// <summary>
        /// Get or set if this customer is enabled
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            private set
            {
                _IsEnabled = value;
            }
        }


        /// <summary>
        /// Get or set associated country identifier
        /// </summary>
        public Guid CountryId { get; private set; }

        /// <summary>
        /// Get the current country for this customer
        /// </summary>
        public virtual Country Country { get; private set; }

        /// <summary>
        /// Get or set associated photo for this customer
        /// </summary>
        public virtual Picture Picture { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Disable customer
        /// </summary>
        public void Disable()
        {
            if ( IsEnabled)
                this._IsEnabled = false;
        }

        /// <summary>
        /// Enable customer
        /// </summary>
        public void Enable()
        {
            if( !IsEnabled)
                this._IsEnabled = true;
        }

        /// <summary>
        /// Associate existing country to this customer
        /// </summary>
        /// <param name="country"></param>
        public void SetTheCountryForThisCustomer(Country country)
        {
            if (country == null
                ||
                country.IsTransient())
            {
                throw new ArgumentException(Messages.exception_CannotAssociateTransientOrNullCountry);
            }

            //fix relation
            this.CountryId = country.Id;

            this.Country = country;
        }

        /// <summary>
        /// Set the country reference for this customer
        /// </summary>
        /// <param name="countryId"></param>
        public void SetTheCountryReference(Guid countryId)
        {
            if (countryId != Guid.Empty)
            {
                //fix relation
                this.CountryId = countryId;

                this.Country = null;
            }
        }

        /// <summary>
        /// Change the customer credit limit
        /// </summary>
        /// <param name="newCredit">the new credit limit</param>
        public void ChangeTheCurrentCredit(decimal newCredit)
        {
            if ( IsEnabled )
                this.CreditLimit = newCredit;
        }

        /// <summary>
        /// change the picture for this customer
        /// </summary>
        /// <param name="picture">the new picture for this customer</param>
        public void ChangePicture(Picture picture)
        {
            if (picture != null &&
                !picture.IsTransient())
            {
                this.Picture = picture;
            }
        }

        #endregion

        #region IValidatableObject Members

        /// <summary>
        /// <see cref="M:System.ComponentModel.DataAnnotations.IValidatableObject.Validate"/>
        /// </summary>
        /// <param name="validationContext"><see cref="M:System.ComponentModel.DataAnnotations.IValidatableObject.Validate"/></param>
        /// <returns><see cref="M:System.ComponentModel.DataAnnotations.IValidatableObject.Validate"/></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check first name property
            if (String.IsNullOrWhiteSpace(this.FirstName))
            {
                validationResults.Add(new ValidationResult(Messages.validation_CustomerFirstNameCannotBeNull, 
                                                           new string[] { "FirstName" }));
            }

            //-->Check last name property
            if (String.IsNullOrWhiteSpace(this.LastName))
            {
                validationResults.Add(new ValidationResult(Messages.validation_CustomerLastNameCannotBeBull,
                                                           new string[] { "LastName" }));
            }

            //-->Check Country identifier
            if (this.CountryId == Guid.Empty)
                validationResults.Add(new ValidationResult(Messages.validation_CustomerCountryIdCannotBeEmpty, 
                                                          new string[] { "CountryId" }));


            return validationResults;
        }

        #endregion
    }
}
