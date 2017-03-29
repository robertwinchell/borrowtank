using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ASOL.HireThings.Logger
{
    public class Logger
    {
        
        // the static queue to hold the mesages
        ConcurrentQueue<Message> _logMessages;
        bool isLogging = false;
        object _lock = new object();
        
        static object _objLock = new object();
        static Logger _obj = null;

        private Logger()
        {
            _logMessages = new ConcurrentQueue<Message>();
        }

        static Logger GetInstance()
        {
            lock (_objLock)
            {
                if (_obj == null)
                {
                    _obj = new Logger();
                }
            }

            return _obj;
        }

        private void Enqueue(Message msg)
        {
            _logMessages.Enqueue(msg);
            Process(); 
        }

        public static void Log(string message) {
            Logger.GetInstance().Enqueue(new Message(message));  
        }

        public static void Log(Exception ex)
        {
            Logger.GetInstance().Enqueue(new Message(ex));
        }

        private void Process()
        {
            lock (_lock)
            {
                if (isLogging)
                    return;

                isLogging = true;
            }

            BaseLogger logger = new FileLogger(ref _logMessages);
            logger.LogCompleted += OnLogCompleted;  
            ThreadStart thrStr = new ThreadStart(logger.StartLogging);
            Thread thr = new Thread(thrStr);
            thr.Start();

        }

        private void OnLogCompleted(object sender)
        {
            ((BaseLogger)sender).LogCompleted -= OnLogCompleted;

            lock (_lock)
            {
                isLogging = false;
            }
        }
    }
}