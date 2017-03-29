using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOL.HireThings.Logger
{
    public abstract class BaseLogger
    {
        protected ConcurrentQueue<Message> _queue;

        public BaseLogger(ref ConcurrentQueue<Message> queue)
        {
            _queue = queue;
        }

        public abstract void StartLogging();
        public delegate void LogCompleteHandler(object sender);
        public event LogCompleteHandler LogCompleted;

        protected void OnLogCompleted()
        {
            LogCompleted(this); 
        }
    }
}