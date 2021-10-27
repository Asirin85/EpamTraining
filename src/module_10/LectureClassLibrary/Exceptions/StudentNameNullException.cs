namespace BusinessLogic.Exceptions
{
    using System;
    [Serializable]
    public class StudentNameNullException : Exception
    {
        public StudentNameNullException() : base() { }
        public StudentNameNullException(string message) : base(message) { }
        public StudentNameNullException(string message, Exception inner) : base(message, inner) { }

        protected StudentNameNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
