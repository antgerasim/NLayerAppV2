using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.Samples.NLayerApp.Presentation.Silverlight.Client.ViewModelBase.Messenger
{
    /// <summary>
    /// Dispatch messages to registered viewmodels
    /// </summary>
    public sealed class MessageDispatcher
    {
        #region Private fields

        private static readonly MessageDispatcher _current = new MessageDispatcher();
        MultiDictionary<DispatcherMessages, Action<Object>> internalList = new MultiDictionary<DispatcherMessages, Action<Object>>();

        #endregion

        #region Singleton definition

        private MessageDispatcher() { }

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
        /// Registers a Colleague to a specific message
        /// </summary>
        /// <param name="callback">The callback to use 
        /// when the message it seen</param>
        /// <param name="message">The message to 
        /// register to</param>
        public void Register(Action<Object> callback,
            DispatcherMessages message)
        {
            internalList.AddValue(message, callback);
        }

        /// <summary>
        /// Unregisters the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="message">The message.</param>
        public void Unregister(object sender, DispatcherMessages message)
        {
            if (internalList.Where(i => i.Key == message).Count() > 0)
            {
                foreach (Action<object> callback in internalList[message])
                {
                    if (callback.Target == sender)
                    {
                        internalList.Remove(message);
                    }
                }
            }
        }


        /// <summary>
        /// Notify all colleagues that are registed to the 
        /// specific message
        /// </summary>
        /// <param name="message">The message for the notify by</param>
        /// <param name="args">The arguments for the message</param>
        public void NotifyColleagues(DispatcherMessages message,
            object args)
        {
            if (internalList.ContainsKey(message))
            {
                //forward the message to all listeners
                foreach (Action<object> callback in
                    internalList[message])
                    callback(args);
            }
        }
        #endregion
    }

}
