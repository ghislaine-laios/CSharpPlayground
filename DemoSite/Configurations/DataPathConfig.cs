using System.Reflection;

namespace DemoSite.Configurations;

public class DataPathOptions
{
    public string? FilesHosting { get; set; }
}

public class DataPathConfig
{
    private const string FileHostingSubDir = "Uploads";

    public DataPathConfig(DataPathOptions dataPathOptions, IWebHostEnvironment environment,
        ApplicationInfoConfig applicationInfo)
    {
        if (environment.IsDevelopment())
        {
            var binPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
            Directory.SetCurrentDirectory(binPath);
        }

        if (!string.IsNullOrEmpty(dataPathOptions.FilesHosting))
        {
            FilesHostingPath = dataPathOptions.FilesHosting;
        }
        else
        {
            // If not specified, use the default path.
            if (OperatingSystem.IsWindows())
                FilesHostingPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    applicationInfo.ApplicationName, FileHostingSubDir);
            else if (OperatingSystem.IsLinux())
                FilesHostingPath = Path.Join("/srv", applicationInfo.ApplicationName, FileHostingSubDir);
            else throw new NotImplementedException();
        }

        FilesHostingDirectory = Directory.CreateDirectory(FilesHostingPath);
    }

    public string FilesHostingPath { get; }
    public DirectoryInfo FilesHostingDirectory { get; }
}