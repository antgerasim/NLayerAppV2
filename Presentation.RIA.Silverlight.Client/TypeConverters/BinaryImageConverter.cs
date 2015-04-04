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
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Converters
{
    /// <summary>
    /// Customer binary image ocnverter
    /// </summary>
    public class BinaryImageConverter
            : IValueConverter
    {
        #region IValueConverter Methods

        /// <summary>
        /// <see cref="System.Windows.Data.IValueConverter"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <param name="targetType"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <param name="parameter"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <param name="culture"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <returns><see cref="System.Windows.Data.IValueConverter"/></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] buffer = value as byte[];
            if (buffer != null && buffer.Length > 0)
            {
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.SetSource(new MemoryStream(buffer));

                return bmpImage;
            }
            else
            {
                var stream = App.GetResourceStream(new Uri("Resources/Images/Unknown.png", UriKind.Relative)).Stream;
                BitmapImage img = new BitmapImage();
                img.SetSource(stream);
                return img;
            }
        }
        /// <summary>
        /// <see cref="System.Windows.Data.IValueConverter"/>
        /// </summary>
        /// <param name="value"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <param name="targetType"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <param name="parameter"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <param name="culture"><see cref="System.Windows.Data.IValueConverter"/></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}