using Org.BouncyCastle.Crypto.Tls;

namespace DemoSite.Models.DTO
{
    public class FileWithContent
    {
        public required string ContentType { get; init; }
        public required byte[] Content { get; init; }
    }
}
