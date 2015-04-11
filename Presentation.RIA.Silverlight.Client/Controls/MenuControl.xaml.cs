using System.Windows;
using System.Windows.Controls;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Controls
{

   public partial class MenuControl : UserControl
   {

      public MenuControl()
      {
         // Required to initialize variables
         InitializeComponent();
         this.DataContext = new VmMenu();
      }

      public string ActualState
      {
         get
         {
            return ((MainPage) App.Current.RootVisual).ActualState;
         }
         set
         {
            ((MainPage) App.Current.RootVisual).ActualState = value;
         }
      }
      public MainPage MainPage
      {
         get
         {
            return ((MainPage) App.Current.RootVisual);
         }
      }

      private void BTN_Menu_Click(object sender, RoutedEventArgs e)
      {
         var control = (Button) sender;
         switch (control.Name)
         {
            case "BTN_Customer":
            {
               VisualStateManager.GoToState(MainPage, "ToCustomer", true);
               ActualState = "ToBanking";

               break;
            }

            case "BTN_Orders":
            {
               VisualStateManager.GoToState(MainPage, "ToOrders", true);
               ActualState = "ToOrders";
               break;
            }

            case "BTN_Banking":
            {
               VisualStateManager.GoToState(MainPage, "ToBanking", true);
               ActualState = "ToBanking";
               break;
            }
         }
      }

   }

}