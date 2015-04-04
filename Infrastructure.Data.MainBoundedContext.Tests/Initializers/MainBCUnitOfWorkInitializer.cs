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


namespace Infrastructure.Data.MainBoundedContext.Tests.Initializers
{
    using System;
    using System.Data.Entity;

    using Microsoft.Samples.NLayerApp.Domain.Seedwork;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CountryAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.CustomerAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.OrderAgg;
    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg;
    using Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork;

    /// <summary>
    /// The default initializer for testing this unit of work. Yoy can
    /// learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// <remarks>
    /// In this initialize data the Guid is not sequential Guid but 
    /// in your code you need use sequential guid to avoid index fragmentation
    /// </remarks>
    /// </summary>
    public class MainBCUnitOfWorkInitializer
        : DropCreateDatabaseAlways<MainBCUnitOfWork>
    {
        protected override void Seed(MainBCUnitOfWork unitOfWork)
        {
            /*
             * Countries agg
             */

            var spainCountry = new Country("Spain", "es-ES");
            spainCountry.ChangeCurrentIdentity(new Guid("32BB805F-40A4-4C37-AA96-B7945C8C385C"));

            var usaCountry = new Country("EEUU", "en-US");
            usaCountry.ChangeCurrentIdentity(new Guid("C3C82D06-6A07-41FB-B7EA-903EC456BFC5"));

            unitOfWork.Countries.Add(spainCountry);
            unitOfWork.Countries.Add(usaCountry);

            /*
             * Customers agg
             */

            var customerJhon = CustomerFactory.CreateCustomer("Jhon", "Jhon", "+34617", "company", spainCountry, new Address("Madrid", "280181", "Paseo de La finca", ""));
            customerJhon.ChangeCurrentIdentity(new Guid("43A38AC8-EAA9-4DF0-981F-2685882C7C45"));


            var customerMay = CustomerFactory.CreateCustomer("May", "Garcia", "+34617", "company", usaCountry, new Address("Seatle", "3332", "Alaskan Way", ""));
            customerMay.ChangeCurrentIdentity(new Guid("0CD6618A-9C8E-4D79-9C6B-4AA69CF18AE6"));


            unitOfWork.Customers.Add(customerJhon);
            unitOfWork.Customers.Add(customerMay);


            /*
             * Product agg
             */
            var book = new Book("The book title", "Any book description", "Krassis Press", "ABC");

            book.ChangeUnitPrice(40M);
            book.IncrementStock(2);

            book.ChangeCurrentIdentity(new Guid("44668EBF-7B54-4431-8D61-C1298DB50857"));

            var software = new Software("the new SO", "the software description", "XXXX0000--111");

            software.ChangeUnitPrice(100M);
            software.IncrementStock(3);
            software.ChangeCurrentIdentity(new Guid("D7E5C537-6A0C-4E19-B41E-3653F4998085"));

            unitOfWork.Products.Add(book);
            unitOfWork.Products.Add(software);

            /*
             * Orders agg
             */

            var orderA = OrderFactory.CreateOrder(customerJhon, "shipping name", "shipping city", "shipping address", "shipping zip code");

            orderA.ChangeCurrentIdentity(new Guid("3135513C-63FD-43E6-9697-6C6E5D8CE55B"));
            orderA.OrderDate = DateTime.Now;

            orderA.AddNewOrderLine(book.Id, 1, 40, 0);

            var orderB = OrderFactory.CreateOrder(customerMay, "shipping name", "shipping city", "shipping address", "shipping zip code");

            orderB.GenerateNewIdentity();
            orderB.OrderDate = DateTime.Now;

            orderB.AddNewOrderLine(software.Id, 3, 12, 0);

            unitOfWork.Orders.Add(orderA);
            unitOfWork.Orders.Add(orderB);

            /*
             * Bank Account agg
             */

            var bankAccountNumberJhon = new BankAccountNumber("1111", "2222", "3333333333", "01");
            BankAccount bankAccountJhon = BankAccountFactory.CreateBankAccount(customerJhon, bankAccountNumberJhon);
            bankAccountJhon.ChangeCurrentIdentity(new Guid("0343C0B0-7C40-444A-B044-B463F36A1A1F"));
            bankAccountJhon.DepositMoney(1000, "Open BankAccount");

            var bankAccountNumberMay = new BankAccountNumber("4444", "5555", "3333333333", "02");
            BankAccount bankAccountMay = BankAccountFactory.CreateBankAccount(customerMay, bankAccountNumberMay);
            bankAccountMay.GenerateNewIdentity();
            bankAccountJhon.DepositMoney(2000, "Open BankAccount");

            unitOfWork.BankAccounts.Add(bankAccountJhon);
            unitOfWork.BankAccounts.Add(bankAccountMay);

        }
    }
}
