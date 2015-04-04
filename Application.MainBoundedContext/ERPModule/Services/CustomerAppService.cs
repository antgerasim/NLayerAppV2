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


namespace Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.DTO;
    using Microsoft.Samples.NLayerApp.Application.MainBoundedContext.Resources;
    using Microsoft.Samples.NLayerApp.Application.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Logging;
    using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;

    /// <summary>
    /// The customer management service implementation.
    /// <see cref="Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerAppService"/>
    /// </summary>
    public class CustomerAppService
        : ICustomerAppService
    {
        #region Members

        readonly ICountryRepository _countryRepository;
        readonly ICustomerRepository _customerRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of Customer Management Service
        /// </summary>
        /// <param name="customerRepository">Associated CustomerRepository, intented to be resolved with DI</param>
        /// <param name="countryRepository">Associated country repository</param>
        public CustomerAppService(ICountryRepository countryRepository, //the country repository
                                  ICustomerRepository customerRepository) //the customer repository                               
        {
            if (customerRepository == null)
                throw new ArgumentNullException("customerRepository");

            if (countryRepository == null)
                throw new ArgumentNullException("countryRepository");

            _countryRepository = countryRepository;
            _customerRepository = customerRepository;
        }

        #endregion

        #region ICustomerAppService Members

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.AddNewCustomer"/>
        /// </summary>
        /// <param name="customerDTO"><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.AddNewCustomer"/></param>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.AddNewCustomer"/></returns>
        public CustomerDTO AddNewCustomer(CustomerDTO customerDTO)
        {
            //check preconditions
            if (customerDTO == null || customerDTO.CountryId == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotAddCustomerWithEmptyInformation);

            var country = _countryRepository.Get(customerDTO.CountryId);

            if (country != null)
            {
                //Create the entity and the required associated data
                var address = new Address(customerDTO.AddressCity, customerDTO.AddressZipCode, customerDTO.AddressAddressLine1, customerDTO.AddressAddressLine2);

                var customer = CustomerFactory.CreateCustomer(customerDTO.FirstName,
                                                              customerDTO.LastName,
                                                              customerDTO.Telephone,
                                                              customerDTO.Company,
                                                              country,
                                                              address);

                //save entity
                SaveCustomer(customer);

                //return the data with id and assigned default values
                return customer.ProjectedAs<CustomerDTO>();
            }
            else
                return null;
        }

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.UpdateCustomer"/>
        /// </summary>
        /// <param name="customerDTO"><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.UpdateCustomer"/></param>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.UpdateCustomer"/></returns>
        public void UpdateCustomer(CustomerDTO customerDTO)
        {
            if (customerDTO == null || customerDTO.Id == Guid.Empty)
                throw new ArgumentException(Messages.warning_CannotUpdateCustomerWithEmptyInformation);

            //get persisted item
            var persisted = _customerRepository.Get(customerDTO.Id);

            if (persisted != null) //if customer exist
            {
                //materialize from customer dto
                var current = MaterializeCustomerFromDto(customerDTO);

                //Merge changes
                _customerRepository.Merge(persisted, current);

                //commit unit of work
                _customerRepository.UnitOfWork.Commit();
            }
            else
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotUpdateNonExistingCustomer);
        }

        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.RemoveCustomer"/>
        /// </summary>
        /// <param name="customerId"><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.Seedwork.Services.ICustomerManagement.RemoveCustomer"/></param>
        public void RemoveCustomer(Guid customerId)
        {
            var customer = _customerRepository.Get(customerId);

            if (customer != null) //if customer exist
            {
                //disable customer ( "logical delete" ) 
                customer.Disable();

                //commit changes
                _customerRepository.UnitOfWork.Commit();
            }
            else //the customer not exist, cannot remove
                LoggerFactory.CreateLog().LogWarning(Messages.warning_CannotRemoveNonExistingCustomer);
        }

        /// <summary>
        ///  <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/>
        /// </summary>
        /// <param name="pageIndex"> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/></param>
        /// <param name="pageCount"> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/></param>
        /// <returns> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/></returns>
        public List<CustomerListDTO> FindCustomers(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentsForFindCustomers);

            //get customers
            var customers = _customerRepository.GetEnabled(pageIndex, pageCount);

            if (customers != null
                &&
                customers.Any())
            {
                return customers.ProjectedAsCollection<CustomerListDTO>();
            }
            else
                return null;
        }
        /// <summary>
        ///  <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/>
        /// </summary>
        /// <param name="text"> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/></param>
        /// <returns> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCustomers"/></returns>
        public List<CustomerListDTO> FindCustomers(string text)
        {
            //get the specification

            var enabledCustomers = CustomerSpecifications.EnabledCustomers();
            var filter = CustomerSpecifications.CustomerFullText(text);

            ISpecification<Customer> spec = enabledCustomers & filter;

            //Query this criteria
            var customers = _customerRepository.AllMatching(spec);

            if (customers != null
                &&
                customers.Any())
            {
                //return adapted data
                return customers.ProjectedAsCollection<CustomerListDTO>();
            }
            else // no data..
                return null;
        }
        /// <summary>
        /// <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/>
        /// </summary>
        /// <param name="customerId"><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></param>
        /// <returns><see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></returns>
        public CustomerDTO FindCustomer(Guid customerId)
        {
            //recover existing customer and map
            var customer = _customerRepository.Get(customerId);

            if (customer != null) //adapt
            {
                return customer.ProjectedAs<CustomerDTO>();
            }
            else
                return null;
        }
        /// <summary>
        ///  <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/>
        /// </summary>
        /// <param name="pageIndex"> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></param>
        /// <param name="pageCount"> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></param>
        /// <returns> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></returns>
        public List<CountryDTO> FindCountries(int pageIndex, int pageCount)
        {
            if (pageIndex < 0 || pageCount <= 0)
                throw new ArgumentException(Messages.warning_InvalidArgumentsForFindCountries);
            
            //recover countries
            var countries = _countryRepository.GetPaged(pageIndex, pageCount, c => c.CountryName, false);

            if (countries != null
                &&
                countries.Any())
            {
                return countries.ProjectedAsCollection<CountryDTO>();
            }
            else // no data.
                return null;

        }
        /// <summary>
        ///  <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/>
        /// </summary>
        /// <param name="text"> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></param>
        /// <returns> <see cref="M:Microsoft.Samples.NLayerApp.Application.MainBoundedContext.ERPModule.Services.ICustomerManagement.FindCountries"/></returns>
        public List<CountryDTO> FindCountries(string text)
        {
            //get the specification
            ISpecification<Country> specification = CountrySpecifications.CountryFullText(text);

            //Query this criteria
            var countries = _countryRepository.AllMatching(specification);

            if (countries != null
                &&
                countries.Any())
            {
                return countries.ProjectedAsCollection<CountryDTO>();
            }
            else // no data
                return null;
        }

        #endregion

        #region Private Methods

        void SaveCustomer(Customer customer)
        {
            //recover validator
            var validator = EntityValidatorFactory.CreateValidator();

            if (validator.IsValid(customer)) //if customer is valid
            {
                //add the customer into the repository
                _customerRepository.Add(customer);

                //commit the unit of work
                _customerRepository.UnitOfWork.Commit();
            }
            else //customer is not valid, throw validation errors
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages<Customer>(customer));
        }

        Customer MaterializeCustomerFromDto(CustomerDTO customerDTO)
        {
            //create the current instance with changes from customerDTO
            var address = new Address(customerDTO.AddressCity, customerDTO.AddressZipCode, customerDTO.AddressAddressLine1, customerDTO.AddressAddressLine2);

            Country country = new Country("Spain", "es-ES");
            country.ChangeCurrentIdentity(customerDTO.CountryId);

            var current = CustomerFactory.CreateCustomer(customerDTO.FirstName,
                                                         customerDTO.LastName,
                                                         customerDTO.Telephone,
                                                         customerDTO.Company,
                                                         country,
                                                         address);

            current.SetTheCountryReference(customerDTO.Id);

            //set credit
            current.ChangeTheCurrentCredit(customerDTO.CreditLimit);

            //set picture
            var picture = new Picture { RawPhoto = customerDTO.PictureRawPhoto };
            picture.ChangeCurrentIdentity(current.Id);

            current.ChangePicture(picture);

            //set identity
            current.ChangeCurrentIdentity(customerDTO.Id);


            return current;
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            //dispose all resources
            _countryRepository.Dispose();
            _customerRepository.Dispose();
        }

        #endregion
    }
}
