using System.Windows;
using System.ComponentModel;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.Helpers
{
    /// <summary>
    /// Help to get the current running enviroment (Design tools, run, etc...)
    /// </summary>
    public static class DesignTimeHelper
    {
        #region Desing Helper

        /// <summary>
        /// Gets a value indicating whether this instance is in design time.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is in design time; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDesignTime
        {
            get
            {
                if (Application.Current.RootVisual != null)
                    return DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual);
                return false;
            }
        }

        #endregion
    }
}
