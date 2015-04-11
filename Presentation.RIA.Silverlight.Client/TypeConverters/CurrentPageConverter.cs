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
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.TypeConverters
{

   /// <summary>
   ///    Current page value converter
   /// </summary>
   public class CurrentPageConverter : IValueConverter
   {

      /// <summary>
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </summary>
      /// <param name="value">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <param name="targetType">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <param name="parameter">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <param name="culture">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <returns>
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </returns>
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var current = (int) value;
         return ++current;
      }

      /// <summary>
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </summary>
      /// <param name="value">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <param name="targetType">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <param name="parameter">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <param name="culture">
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </param>
      /// <returns>
      ///    <see cref="System.Windows.Data.IValueConverter" />
      /// </returns>
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var current = (int) value;
         return --current;
      }

   }

}