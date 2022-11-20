namespace DemoSite.Exceptions;

public class HttpResponseException : Exception
{
    public int Code { get; }

    public HttpResponseException(string message, int code) : base(message)
    {
        Code = code;
    }

    public class Data
    {
        public required int Code { get; init; }
        public required string Message { get; init; }
    }

    public Data ToDataObject() => new Data { Code = Code, Message = Message };
}
