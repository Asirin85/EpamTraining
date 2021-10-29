namespace BusinessLogic.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Serializable]
    public class SmtpClientDisposedException : Exception
    {
        public SmtpClientDisposedException() : base() { }
        public SmtpClientDisposedException(string message) : base(message) { }
        public SmtpClientDisposedException(string message, Exception inner) : base(message, inner) { }

        protected SmtpClientDisposedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
