namespace DemoSite.Exceptions
{
    public class FileTooLargeException: Exception
    {
        public FileTooLargeException(string message = "File is too large."): base(message) { }
    }
}
