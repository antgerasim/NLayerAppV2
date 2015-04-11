using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client
{

   public partial class MainPage : UserControl
   {

      public string ActualState;

      public MainPage()
      {
         // Required to initialize variables
         InitializeComponent();

         this.MouseWheel += new MouseWheelEventHandler(MainPage_MouseWheel);
         ActualState = "ToCustomer";

      }

      private void MainPage_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         switch (ActualState)
         {
            case "ToCustomer":
            {
               if (e.Delta < 0)
               {
                  VisualStateManager.GoToState(this, "ToBanking", true);
                  ActualState = "ToBanking";
               }
               break;
            }

            case "ToBanking":
            {
               if (e.Delta < 0)
               {
                  VisualStateManager.GoToState(this, "ToOrders", true);
                  ActualState = "ToOrders";
               }
               else
               {
                  VisualStateManager.GoToState(this, "ToCustomer", true);
                  ActualState = "ToCustomer";

               }
               break;
            }

            case "ToOrders":
            {
               if (e.Delta > 0)
               {
                  VisualStateManager.GoToState(this, "ToBanking", true);
                  ActualState = "ToBanking";
               }
               break;
            }
         }

      }

   }

}