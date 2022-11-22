namespace DemoSite.Exceptions;

public class HttpResponseException : Exception
{
    public HttpResponseException(string message, int code) : base(message)
    {
        Code = code;
    }

    public int Code { get; }

    public Data ToDataObject()
    {
        return new Data { Code = Code, Message = Message };
    }

    public class Data
    {
        public required int Code { get; init; }
        public required string Message { get; init; }
    }
}