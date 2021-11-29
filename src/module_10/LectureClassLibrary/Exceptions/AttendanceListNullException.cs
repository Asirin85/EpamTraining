namespace BusinessLogic.Exceptions
{
    using System;
    [Serializable]
    public class AttendanceListNullException : Exception
    {
        public AttendanceListNullException() : base() { }
        public AttendanceListNullException(string message) : base(message) { }
        public AttendanceListNullException(string message, Exception inner) : base(message, inner) { }

        protected AttendanceListNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
