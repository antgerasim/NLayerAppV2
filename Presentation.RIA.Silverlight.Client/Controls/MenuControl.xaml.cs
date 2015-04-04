using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Controls
{
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            // Required to initialize variables
            InitializeComponent();
            this.DataContext = new ViewModels.VMMenu();
        }

        public string actualState
        {
            get { return ((MainPage)App.Current.RootVisual).actualState; }
            set { ((MainPage)App.Current.RootVisual).actualState = value; }
        }

        public MainPage mainPage
        {
            get { return ((MainPage)App.Current.RootVisual); }
        }



        private void BTN_Menu_Click(object sender, RoutedEventArgs e)
        {
            Button control = (Button)sender;
            switch (control.Name)
            {
                case "BTN_Customer":
                    {
                        VisualStateManager.GoToState(mainPage, "ToCustomer", true);
                        actualState = "ToBanking";

                        break;
                    }

                case "BTN_Orders":
                    {
                        VisualStateManager.GoToState(mainPage, "ToOrders", true);
                        actualState = "ToOrders";
                        break;
                    }

                case "BTN_Banking":
                    {
                        VisualStateManager.GoToState(mainPage, "ToBanking", true);
                        actualState = "ToBanking";
                        break;
                    }
            }
        }
    }
}