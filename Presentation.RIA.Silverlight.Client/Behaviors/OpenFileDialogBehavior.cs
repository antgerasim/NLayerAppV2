using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Behaviors
{
    [Description("Open File Dialog Box")]
    public class OpenFileDialogBehavior : TargetedTriggerAction<Button>
    {
        #region Dependency Properties

        public static readonly DependencyProperty FileDialogDialogResultCommandProperty =
            DependencyProperty.Register("FileDialogDialogResultCommandProperty",
            typeof(object), typeof(OpenFileDialogBehavior), null);

        /// <summary>
        /// Gets or sets the file dialog dialog result command.
        /// </summary>
        /// <value>
        /// The file dialog dialog result command.
        /// </value>
        public object FileDialogDialogResultCommand
        {
            get
            {
                return (object)base.GetValue(FileDialogDialogResultCommandProperty);
            }
            set
            {
                base.SetValue(FileDialogDialogResultCommandProperty, value);
            }
        }


        public static readonly DependencyProperty DialogFilterProperty =
           DependencyProperty.Register("DialogFilter", typeof(string),
           typeof(OpenFileDialogBehavior), null);

        /// <summary>
        /// Gets or sets the dialog filter.
        /// </summary>
        /// <value>
        /// The dialog filter.
        /// </value>
        public string DialogFilter
        {
            get
            {
                if (GetValue(DialogFilterProperty) == null)
                {
                    return "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
                + "PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                }
                return (string)base.GetValue(DialogFilterProperty);
            }
            set
            {
                base.SetValue(DialogFilterProperty, value);
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            try
            {
                //Create a file dialog
                var selectPictureDialog = new OpenFileDialog();

                //set properties for this dialog ( only once file, title and check that file exist )
                selectPictureDialog.Filter = DialogFilter;
                selectPictureDialog.Multiselect = false;

                if (selectPictureDialog.ShowDialog() == true)
                {
                    byte[] buffer;
                    using(var stream  = selectPictureDialog.File.OpenRead())
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                    }
                   
                    //assign selected picture
                    FileDialogDialogResultCommand = buffer;
                }
            }
            catch (SecurityException excep)
            {
                Debug.WriteLine("OpenFileDialogBehavior: Security Error:" + excep.ToString());
            }
            
        }

        #endregion
    }

}
