using ByteSizeLib;

namespace DemoSite.Configurations;

public class FilesHostingOption
{
    public required string MaxSize { get; set; }
}

public class FileHostingConfig
{
    public FileHostingConfig(FilesHostingOption option)
    {
        MaxSize = (long)ByteSize.Parse(option.MaxSize).Bytes;
    }

    // Max file size in bytes.
    public long MaxSize { get; }
}