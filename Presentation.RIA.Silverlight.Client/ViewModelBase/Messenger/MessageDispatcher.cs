using System;
using System.Linq;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModelBase.Messenger
{

   /// <summary>
   ///    Dispatch messages to registered viewmodels
   /// </summary>
   public sealed class MessageDispatcher
   {
      #region Private fields
      private static readonly MessageDispatcher _current = new MessageDispatcher();
      private MultiDictionary<DispatcherMessages, Action<Object>> _internalList =
         new MultiDictionary<DispatcherMessages, Action<Object>>();
      #endregion

      #region Singleton definition
      private MessageDispatcher()
      {
      }

      public static MessageDispatcher Current
      {
         get
         {
            return _current;
         }
      }
      #endregion

      #region Public Methods
      /// <summary>
      ///    Registers a Colleague to a specific message
      /// </summary>
      /// <param name="callback">
      ///    The callback to use
      ///    when the message it seen
      /// </param>
      /// <param name="message">
      ///    The message to
      ///    register to
      /// </param>
      public void Register(Action<Object> callback, DispatcherMessages message)
      {
         _internalList.AddValue(message, callback);
      }

      /// <summary>
      ///    Unregisters the specified sender.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="message">The message.</param>
      public void Unregister(object sender, DispatcherMessages message)
      {
         if (_internalList.Where(i => i.Key == message).Count() > 0)
         {
            foreach (var callback in _internalList[message]) {
               if (callback.Target == sender) { _internalList.Remove(message); }
            }
         }
      }

      /// <summary>
      ///    Notify all colleagues that are registed to the
      ///    specific message
      /// </summary>
      /// <param name="message">The message for the notify by</param>
      /// <param name="args">The arguments for the message</param>
      public void NotifyColleagues(DispatcherMessages message, object args)
      {
         if (_internalList.ContainsKey(message))
         {
            //forward the message to all listeners
            foreach (var callback in
               _internalList[message]) {
                  callback(args);
               }
         }
      }
      #endregion
   }

}