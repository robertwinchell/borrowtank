using System;
using System.Text;

namespace ASOL.HireThings.Logger
{
    public class Message
    {
        private Exception _exception;
        private string _message;
        private DateTime _datetime;

        public Message(Exception ex) 
        {
            _exception = ex;
            _datetime = DateTime.Now;
        }

        public Message(string message)
        {
            _message = message;
            _datetime = DateTime.Now;
        }

        //public string Message { get; }
     //   public Exception Exception { get; set; }

        public override string ToString()
        {

            StringBuilder message = new StringBuilder();      
            if(_message!=null)      
                message.AppendLine(_datetime.ToString() + "  || Message: " + _message);            
            else
                message.AppendLine(_datetime.ToString() + "  || Message: " + _exception.ToString());            
            return message.ToString();  
        }
    }
}