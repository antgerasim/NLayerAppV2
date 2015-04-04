using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client
{
	public partial class CustomerListView : UserControl
	{
		public CustomerListView()
		{
			// Required to initialize variables
			InitializeComponent();
            Loaded += new RoutedEventHandler(CustomerListView_Loaded);            
		}

        void CustomerListView_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModels.VMCustomerListView();
        }
	}
}