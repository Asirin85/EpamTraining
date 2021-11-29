namespace BusinessLogic.Exceptions
{
    using System;
    [Serializable]
    public class StudentNullException : Exception
    {
        public StudentNullException() : base() { }
        public StudentNullException(string message) : base(message) { }
        public StudentNullException(string message, Exception inner) : base(message, inner) { }

        protected StudentNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
