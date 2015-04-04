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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Controls
{
    /// <summary>
    /// Overriden Grid type for integrated Mouse Plane Proyection
    /// </summary>
    public class FloatingGrid : Grid
    {

        #region Private Fields

        /// <summary>
        /// Store the Grid's Plane Proyection 
        /// </summary>
        private PlaneProjection _planeP;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor with events subscriptions
        /// </summary>
        public FloatingGrid()
        {
            MouseMove += (FloatingGrid_MouseMove);
            MouseEnter += (FloatingGrid_MouseEnter);
            MouseLeave += (FloatingGrid_MouseLeave);
            Loaded += (FloatingGrid_Loaded);
        }

        #endregion

        #region Properties

        [Category("Common Properties")]
        public double MaximunAngle
        {
            get { return (double)GetValue(MaximunAngleProperty); }
            set { SetValue(MaximunAngleProperty, value); }
        }

        public static readonly DependencyProperty MaximunAngleProperty =
            DependencyProperty.Register("MaximunAngle", typeof(double), typeof(FloatingGrid), new PropertyMetadata(10.0));

        #endregion

        #region Methods

        /// <summary>
        /// Animate Plane Proyection to a determinate position
        /// </summary>
        /// <param name="x">X Angle to move</param>
        /// <param name="y">Y angle to move</param>
        private void AnimateTo(double x, double y)
        {
            var duration = new Duration(TimeSpan.FromSeconds(0.44));

            var xAnimation = new DoubleAnimation();
            var yAnimation = new DoubleAnimation();

            xAnimation.Duration = duration;
            yAnimation.Duration = duration;

            xAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            yAnimation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

            var sb = new Storyboard { Duration = duration };

            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            Storyboard.SetTarget(xAnimation, _planeP);
            Storyboard.SetTarget(yAnimation, _planeP);

            var xPath = new PropertyPath(PlaneProjection.RotationXProperty);
            var yPath = new PropertyPath(PlaneProjection.RotationYProperty);

            Storyboard.SetTargetProperty(xAnimation, xPath);
            Storyboard.SetTargetProperty(yAnimation, yPath);

            xAnimation.To = x;
            yAnimation.To = y;

            sb.Begin();

        }

        /// <summary>
        /// Determine the current angle to animate
        /// </summary>
        /// <param name="e">Gets the current mouse position</param>
        /// <returns>The X and Y Angles</returns>
        private Point GetCurrentAngles(MouseEventArgs e)
        {
            if (double.IsNaN(ActualWidth) || ActualWidth == 0) return new Point(0, 0);
            var actualPos = e.GetPosition(this);
            var centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);
            if (MaximunAngle <= 0) MaximunAngle = 10;
            var y = (actualPos.X - centerPoint.X) / centerPoint.X * MaximunAngle;
            var x = (actualPos.Y - centerPoint.Y) / centerPoint.Y * MaximunAngle;
            return new Point(x, y);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the loaded control event
        /// </summary>
        /// <param name="sender">Current instance of control</param>
        /// <param name="e">Event Args</param>
        void FloatingGrid_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLayout();
            _planeP = new PlaneProjection();
            Projection = _planeP;
        }

        /// <summary>
        /// Handles the Mouse Leave control event
        /// </summary>
        /// <param name="sender">Current instance of control</param>
        /// <param name="e">Event Args</param>
        void FloatingGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimateTo(0, 0);
        }

        /// <summary>
        /// Handles the Mouse Enter control event
        /// </summary>
        /// <param name="sender">Current instance of control</param>
        /// <param name="e">Event Args</param>
        void FloatingGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var pos = GetCurrentAngles(e);
            AnimateTo(pos.X, pos.Y);
        }

        /// <summary>
        /// Handles the Mouse Move control event
        /// </summary>
        /// <param name="sender">Current instance of control</param>
        /// <param name="e">Event Args</param>
        void FloatingGrid_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = GetCurrentAngles(e);
            AnimateTo(pos.X, pos.Y);
        }

        #endregion

    }
}
