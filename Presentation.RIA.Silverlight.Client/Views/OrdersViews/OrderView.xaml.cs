using System.Windows.Controls;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Views.OrdersViews
{

   public partial class OrderView : UserControl
   {

      public OrderView()
      {
         // Required to initialize variables
         InitializeComponent();

         DataContext = new VmOrderList();
      }

   }

}