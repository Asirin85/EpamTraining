namespace BusinessLogic.Exceptions
{
    using System;
    [Serializable]
    public class LectureNameNullException : Exception
    {
        public LectureNameNullException() : base() { }
        public LectureNameNullException(string message) : base(message) { }
        public LectureNameNullException(string message, Exception inner) : base(message, inner) { }

        protected LectureNameNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
