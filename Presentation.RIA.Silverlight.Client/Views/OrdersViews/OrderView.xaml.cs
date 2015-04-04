using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModels;


namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client
{
	public partial class OrderView : UserControl
	{
		public OrderView()
		{
			// Required to initialize variables
			InitializeComponent();

		    DataContext = new VMOrderList();
		}
	}
}