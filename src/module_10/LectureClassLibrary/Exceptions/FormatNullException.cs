namespace BusinessLogic.Exceptions
{
    using System;

    [Serializable]
    public class FormatNullException : Exception
    {
        public FormatNullException() : base() { }
        public FormatNullException(string message) : base(message) { }
        public FormatNullException(string message, Exception inner) : base(message, inner) { }

        protected FormatNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
