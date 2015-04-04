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
    public partial class MainPage : UserControl
    {
        public string actualState;

        public MainPage()
        {
            // Required to initialize variables
            InitializeComponent();

            this.MouseWheel += new MouseWheelEventHandler(MainPage_MouseWheel);
            actualState = "ToCustomer";

        }

        void MainPage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            switch (actualState)
            {
                case "ToCustomer":
                    {
                        if (e.Delta < 0)
                        {
                            VisualStateManager.GoToState(this, "ToBanking", true);
                            actualState = "ToBanking";
                        }
                        break;
                    }

                case "ToBanking":
                    {
                        if (e.Delta < 0)
                        {
                            VisualStateManager.GoToState(this, "ToOrders", true);
                            actualState = "ToOrders";
                        }
                        else
                        {
                            VisualStateManager.GoToState(this, "ToCustomer", true);
                            actualState = "ToCustomer";

                        }
                        break;
                    }

                case "ToOrders":
                    {
                        if (e.Delta > 0)
                        {
                            VisualStateManager.GoToState(this, "ToBanking", true);
                            actualState = "ToBanking";
                        }
                        break;
                    }
            }

        }

    }
}