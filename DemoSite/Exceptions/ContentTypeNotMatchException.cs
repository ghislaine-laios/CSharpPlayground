namespace DemoSite.Exceptions
{
    public class ContentTypeNotMatchException: Exception
    {
        public ContentTypeNotMatchException(string message): base(message) { }
    }
}
