using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client
{

   public partial class App : Application
   {

      public App()
      {
         this.Startup += this.Application_Startup;
         this.Exit += this.Application_Exit;
         this.UnhandledException += this.Application_UnhandledException;

         InitializeComponent();
      }

      private void Application_Startup(object sender, StartupEventArgs e)
      {
         this.RootVisual = new MainPage();

      }

      private void Application_Exit(object sender, EventArgs e)
      {
      }

      private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
      {
         // If the app is running outside of the debugger then report the exception using
         // the browser's exception mechanism. On IE this will display as a yellow alert 
         // icon in the status bar and Firefox will display a script error.
         if (!Debugger.IsAttached)
         {

            // NOTE: This will allow the application to continue running after an exception has been thrown
            // but not handled. 
            // For production applications this error handling should be replaced with something that will 
            // report the error to the website and stop the application.
            e.Handled = true;
            Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDom(e); });
         }
      }

      private void ReportErrorToDom(ApplicationUnhandledExceptionEventArgs e)
      {
         try
         {
            var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
            errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

            HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
         }
         catch (Exception) {
         }
      }

   }

}