using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

//using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client
{
    public partial class EditCustomerView : UserControl
    {
        public EditCustomerView()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        private void BTN_ChangePicture_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        //    //NOTE: OpenFileDialog can only used in user initiated action like the click. Indirections are not allowed.
        //    //Create a file dialog
        //    OpenFileDialog selectPictureDialog = new OpenFileDialog();

        //    //set properties for this dialog ( only once file, title and check that file exist )
        //    selectPictureDialog.Multiselect = false;

        //    if (selectPictureDialog.ShowDialog() == true)
        //    {
        //        Stream stream = selectPictureDialog.File.OpenRead();
        //        BinaryReader binaryReader = new BinaryReader(stream);
        //        byte[] buffer = binaryReader.ReadBytes((int)stream.Length);

        //        //assign selected picture
        //        ((VMEditCustomer)this.DataContext).Photo = buffer;
        //    }
        }
    }
}