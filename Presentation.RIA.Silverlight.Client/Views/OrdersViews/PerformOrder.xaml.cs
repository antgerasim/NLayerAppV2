using System.Windows.Controls;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Views.OrdersViews
{

   public partial class PerformOrder : UserControl
   {

      public PerformOrder()
      {
         // Required to initialize variables
         InitializeComponent();

         DataContext = new VmPerformOrder();
      }

   }

}