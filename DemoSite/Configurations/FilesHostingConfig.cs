using ByteSizeLib;

namespace DemoSite.Configurations;

public class FilesHostingOption
{
    public required string MaxSize { get; set; }
    public required string AvatarMaxSize { get; set; }
}

public class FilesHostingConfig
{
    public FilesHostingConfig(FilesHostingOption option, DataPathConfig dataPathConfig)
    {
        MaxSize = (long)ByteSize.Parse(option.MaxSize).Bytes;
        AvatarMaxSize = (long)ByteSize.Parse(option.AvatarMaxSize).Bytes;
        Directory = dataPathConfig.FilesHostingDirectory;
    }

    // Max file size in bytes.
    public long MaxSize { get; }

    public long AvatarMaxSize { get; }

    public DirectoryInfo Directory { get; }
}