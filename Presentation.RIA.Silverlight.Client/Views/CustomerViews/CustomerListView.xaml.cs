using System.Windows;
using System.Windows.Controls;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Views.CustomerViews
{

   public partial class CustomerListView : UserControl
   {

      public CustomerListView()
      {
         // Required to initialize variables
         InitializeComponent();
         Loaded += new RoutedEventHandler(CustomerListView_Loaded);
      }

      private void CustomerListView_Loaded(object sender, RoutedEventArgs e)
      {
         DataContext = new VmCustomerListView();
      }

   }

}