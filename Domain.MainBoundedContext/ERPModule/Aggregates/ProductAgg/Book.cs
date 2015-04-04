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


using System;
namespace Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.ERPModule.Aggregates.ProductAgg
{
    /// <summary>
    /// The book product
    /// </summary>
    public class Book
        : Product
    {
        #region Properties

        /// <summary>
        /// Get or set the publisher of this book
        /// </summary>
        public string Publisher { get; private set; }

        /// <summary>
        /// Get or set related ISBN
        /// </summary>
        public string ISBN { get; private set; }

        #endregion

        #region Constructor

        //required by ef
        private Book() { }

        public Book(string title, string description,string publisher,string isbn)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("title");

            if (String.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");

            if (String.IsNullOrWhiteSpace(publisher))
                throw new ArgumentNullException("publisher");

            if (String.IsNullOrWhiteSpace(isbn))
                throw new ArgumentNullException("isbn");

            this.Title = title;
            this.Description = description;
            this.Publisher = publisher;
            this.ISBN = isbn;
        }

        #endregion
    }
}
