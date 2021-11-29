namespace BusinessLogic.Exceptions
{
    using System;

    [Serializable]
    public class ReportFormatNotSupportedException : Exception
    {
        public ReportFormatNotSupportedException() : base() { }
        public ReportFormatNotSupportedException(string message) : base(message) { }
        public ReportFormatNotSupportedException(string message, Exception inner) : base(message, inner) { }

        protected ReportFormatNotSupportedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
