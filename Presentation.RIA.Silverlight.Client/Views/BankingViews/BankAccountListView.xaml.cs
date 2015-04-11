using System.Windows.Controls;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Views.BankingViews
{

   public partial class BankAccountListView : UserControl
   {

      public BankAccountListView()
      {
         // Required to initialize variables
         InitializeComponent();

         DataContext = new VmBankAccountsList();
      }

   }

}