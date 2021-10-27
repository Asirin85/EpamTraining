namespace RestApi.ErrorHandling
{
    using System;

    public class ErrorResponseModel
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public ErrorResponseModel(Exception exception)
        {
            Type = exception.GetType().Name;
            Message = exception.Message;
        }
    }
}
