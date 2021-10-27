namespace BusinessLogic.Exceptions
{
    using System;
    [Serializable]
    public class LecturerNullException : Exception
    {
        public LecturerNullException() : base() { }
        public LecturerNullException(string message) : base(message) { }
        public LecturerNullException(string message, Exception inner) : base(message, inner) { }

        protected LecturerNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
