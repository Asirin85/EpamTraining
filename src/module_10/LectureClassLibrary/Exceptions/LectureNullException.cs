namespace BusinessLogic.Exceptions
{
    using System;

    [Serializable]
    public class LectureNullException : Exception
    {
        public LectureNullException() : base() { }
        public LectureNullException(string message) : base(message) { }
        public LectureNullException(string message, Exception inner) : base(message, inner) { }

        protected LectureNullException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
