namespace DemoSite.Exceptions
{
    public class PropertyConflictException<T> : Exception
    {
        public string Property { get; }

        public PropertyConflictException(string message, string property) : base(message)
        {
            Property = property;
        }

        public PropertyConflictException(string property)
            : this($"The property {property} on entity {typeof(T)} conflicts with other instance.", property) { }
    }
}
